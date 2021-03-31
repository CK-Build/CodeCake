using Cake.Common.IO;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cake.Common.IO
{
    public static class CakeWorkaroundExtensions
    {
        [CakeAliasCategory( "Copy" )]
        [CakeMethodAlias]
        public static void CopyFile( this ICakeContext context, string filePath, string targetFilePath )
            => FileAliases.CopyFile( context, new FilePath( filePath ), new FilePath( targetFilePath ) );

        [CakeAliasCategory( "Copy" )]
        [CakeMethodAlias]
        public static void CopyFileToDirectory( this ICakeContext context, string filePath, string targetDirectoryPath )
            => FileAliases.CopyFileToDirectory(context, new FilePath( filePath ), new DirectoryPath( targetDirectoryPath ) );

        [CakeAliasCategory( "Delete" )]
        [CakeMethodAlias]
        public static void DeleteFile( this ICakeContext context, string filePath )
            => FileAliases.DeleteFile(context, new FilePath( filePath ) );

        [CakeAliasCategory( "Delete" )]
        [CakeMethodAlias]
        public static void DeleteFiles( this ICakeContext context, string pattern )
            => FileAliases.DeleteFiles(context, new GlobPattern( pattern ) );

        [CakeAliasCategory( "Path" )]
        [CakeMethodAlias]
        public static FilePath ExpandEnvironmentVariables( this ICakeContext context, string filePath )
            => FileAliases.ExpandEnvironmentVariables(context, new FilePath( filePath ) );

        [CakeAliasCategory( "Exists" )]
        [CakeMethodAlias]
        public static bool FileExists( this ICakeContext context, string filePath )
            => FileAliases.FileExists( context, new FilePath( filePath ) );

        [CakeAliasCategory( "Path" )]
        [CakeMethodAlias]
        public static FilePath MakeAbsolute( this ICakeContext context, string filePath )
            => FileAliases.MakeAbsolute( context, new FilePath( filePath ) );

        [CakeAliasCategory( "Move" )]
        [CakeMethodAlias]
        public static void MoveFile( this ICakeContext context, string filePath, string targetFilePath )
            => FileAliases.MoveFile(context, new FilePath( filePath ), new FilePath( targetFilePath ) );

        // Directory

        [CakeAliasCategory( "Clean" )]
        [CakeMethodAlias]
        public static void CleanDirectories( this ICakeContext context, string pattern )
            => DirectoryAliases.CleanDirectories( context, new GlobPattern( pattern ) );

        [CakeAliasCategory( "Clean" )]
        [CakeMethodAlias]
        public static void CleanDirectories( this ICakeContext context, string pattern, Func<IFileSystemInfo, bool> predicate )
            => DirectoryAliases.CleanDirectories(context, new GlobPattern( pattern ), predicate );

        [CakeAliasCategory( "Clean" )]
        [CakeMethodAlias]
        public static void CleanDirectory( this ICakeContext context, string path, Func<IFileSystemInfo, bool> predicate )
            => DirectoryAliases.CleanDirectory(context, new DirectoryPath( path ), predicate );

        [CakeAliasCategory( "Clean" )]
        [CakeMethodAlias]
        public static void CleanDirectory( this ICakeContext context, string path )
            => DirectoryAliases.CleanDirectory(context, new DirectoryPath( path ) );

        [CakeAliasCategory( "Copy" )]
        [CakeMethodAlias]
        public static void CopyDirectory( this ICakeContext context, string source, string destination )
            => DirectoryAliases.CopyDirectory( context, new DirectoryPath( source ), new DirectoryPath( destination ) );

        [CakeAliasCategory( "Exists" )]
        [CakeMethodAlias]
        public static bool DirectoryExists( this ICakeContext context, string path )
            => DirectoryAliases.DirectoryExists( context, new DirectoryPath( path ) );

        [CakeAliasCategory( "Exists" )]
        [CakeMethodAlias]
        public static void EnsureDirectoryExists( this ICakeContext context, string path )
            => DirectoryAliases.EnsureDirectoryExists( context, new DirectoryPath( path ) );

        [CakeAliasCategory( "Move" )]
        [CakeMethodAlias]
        public static void MoveDirectory( this ICakeContext context, string directoryPath, string targetDirectoryPath )
            => DirectoryAliases.MoveDirectory(context, new DirectoryPath( directoryPath ), new DirectoryPath( targetDirectoryPath ) );

    }
}
