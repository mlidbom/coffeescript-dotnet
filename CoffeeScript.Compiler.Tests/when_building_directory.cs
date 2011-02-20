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
        [Test]
        public void source_directory_is_mirrored_in_output()
        {
            var sourceDir = ExampleScripts.Valid.Hierarchy;


            new Compiler().Compile(new CompilerOptions
                                       {
                                           Compile = true,
                                           OutputDir = OutputDir.ToString(),
                                           Path = sourceDir.ToString()
                                       });

            AssertOutputMirrorsSource(sourceDir, OutputDir);
        }
    }
}