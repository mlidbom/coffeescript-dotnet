#region usings

using System;
using System.Collections.Generic;
using System.IO;
using CoffeeScript.Compiler.Util;
using NUnit.Framework;
using System.Linq;

#endregion

namespace CoffeeScript.Compiler.Tests
{
    public class CompilerTestBase
    {
        public static readonly DirectoryInfo BinDir = Environment.CurrentDirectory.AsDirectory();
        public static readonly DirectoryInfo BaseDir = BinDir.Parent.Parent;
        public static readonly DirectoryInfo OutputDir = BaseDir.SubDir("OutputDir");

        public static readonly string NonExistingPath = @"Q:\nonsense\nonsense\nonsense\nonsense";

        protected CompilerTestBase(IEnumerable<DirectoryInfo> additionalOutputDirectories = null)
        {
            OutputDirectories = new[] { OutputDir, ExampleScripts.Base };
            if (additionalOutputDirectories != null)
            {
                OutputDirectories = OutputDirectories.Concat(additionalOutputDirectories);
            }
        }

        [SetUp]
        public void CleanOutputDirectoriesFromJsFiles()
        {
            OutputDirectories.ForEach(dir => dir.Glob("*.js").ForEach(f => f.Delete()));
        }

        protected IEnumerable<DirectoryInfo> OutputDirectories { get; private set; }

        protected static void AssertTargetMirrorsSource(DirectoryInfo sourceDirectory, DirectoryInfo targetDirectory)
        {
            var sourceFiles = sourceDirectory.Glob("*.coffee");
            var expectedTargetFiles = sourceFiles
                .Select(src => Path.ChangeExtension(src.FullName, "js"))
                .Select(src => src.Replace(sourceDirectory.ToString(), OutputDir.ToString()))
                .OrderBy(f => f)
                .ToArray();

            var actualTargetFiles = targetDirectory.Glob("*.js*").Select(f => f.FullName).OrderBy(f => f).ToArray();

            Console.WriteLine("Expecting:");
            expectedTargetFiles.ForEach(Console.WriteLine);

            Console.WriteLine("Got:");
            actualTargetFiles.ForEach(Console.WriteLine);

            Assert.That(actualTargetFiles, Is.EqualTo(expectedTargetFiles));
        }
    }
}
