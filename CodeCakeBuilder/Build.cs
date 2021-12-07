
using Cake.Common.IO;
using Cake.Core;
using Cake.Core.Diagnostics;

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
                context.Log.Information( "Executed BEFORE the first task." );
            } );

            Teardown( context =>
            {
                context.Log.Information( "Executed AFTER the last task." );
            } );

            TaskSetup( setupContext =>
            {
                setupContext.Log.Information( $"TaskSetup for Task: {setupContext.Task.Name}" );
            } );

            TaskTeardown( teardownContext =>
            {
                teardownContext.Log.Information( $"TaskTeardown for Task: {teardownContext.Task.Name}" );
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
