using Cake.Common;
using Cake.Common.Solution;
using Cake.Common.IO;
using Cake.Common.Tools.MSBuild;
using Cake.Common.Tools.NuGet;
using Cake.Core;
using Cake.Common.Diagnostics;
using Code.Cake;
using Cake.Common.Tools.NuGet.Pack;
using System.Linq;
using Cake.Core.Diagnostics;
using Cake.Common.Tools.NuGet.Restore;
using System;
using Cake.Common.Tools.NuGet.Push;
using SimpleGitVersion;
using Cake.Common.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Cake.Core.IO;
using Cake.Common.Build.AppVeyor;
using Cake.Common.Build;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;

namespace CodeCake
{
    public partial class Build : CodeCakeHost
    {
        public Build()
        {
            Cake.Log.Verbosity = Verbosity.Diagnostic;

            StandardGlobalInfo globalInfo = CreateStandardGlobalInfo()
                                                .AddDotnet()
                                                .SetCIBuildTag();

            Setup( context =>
            {
                context.Information( "Executed BEFORE the first task." );
            } );

            Teardown( context =>
            {
                context.Information( "Executed AFTER the last task." );
            } );

            TaskSetup( setupContext =>
            {
                setupContext.Information( $"TaskSetup for Task: {setupContext.Task.Name}" );
            } );

            TaskTeardown( teardownContext =>
            {
                teardownContext.Information( $"TaskTeardown for Task: {teardownContext.Task.Name}" );
            } );

            Task( "Check-Repository" )
                .Does( () =>
                {
                    globalInfo.TerminateIfShouldStop();
                } );

            Task( "Clean" )
                .IsDependentOn( "Check-Repository" )
                .Does( () =>
                {
                    globalInfo.GetDotnetSolution().Clean();
                    Cake.CleanDirectories( globalInfo.ReleasesFolder.ToString() );
                    Cake.DeleteFiles( "Tests/**/TestResult*.xml" );
                } );

            Task( "Build" )
                .IsDependentOn( "Clean" )
                .IsDependentOn( "Check-Repository" )
                .Does( () =>
                {
                    globalInfo.GetDotnetSolution().Build();
                } );

            Task( "Create-NuGet-Packages" )
                .WithCriteria( () => globalInfo.IsValid )
                .IsDependentOn( "Build" )
                .Does( () =>
                {
                    globalInfo.GetDotnetSolution().Pack();
                } );

            Task( "Push-NuGet-Packages" )
                .IsDependentOn( "Create-NuGet-Packages" )
                .WithCriteria( () => globalInfo.IsValid )
               .Does( async () =>
                {
                    await globalInfo.PushArtifactsAsync();
                } );

            Task( "Default" ).IsDependentOn( "Push-NuGet-Packages" );

        }
    }
}
