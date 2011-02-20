#region usings

using System;
using System.Linq;
using System.IO;
using CoffeeScript.Compiler.Util;
using NUnit.Framework;


#endregion

namespace CoffeeScript.Compiler.Tests
{
    [TestFixture]
    public class when_building_directory : CompilerTestBase
    {
        public static string[] OutputFolderParameters
        {
            get { return new[]
                             {
                                 OutputDir.FullName, "", null, " "
                             }; }
        }

        [Test]
        public void source_directory_is_mirrored_in_output([ValueSource("OutputFolderParameters")] string outputFolder)
        {
            var sourceDir = ExampleScripts.Valid.Hierarchy;


            var compilerOptions = new CompilerOptions
                                      {
                                          Compile = true,
                                          OutputDir = outputFolder,
                                          Path = sourceDir.ToString()
                                      };

            new Compiler().Compile(compilerOptions);

            AssertOutputMirrorsSource(compilerOptions.Path.AsDirectory(), compilerOptions.OutputDir.AsDirectory());
        }
    }
}