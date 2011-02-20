﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CoffeeScript.Compiler.Util;

namespace CoffeeScript.Compiler.Tests
{
    [TestFixture]
    public class when_output_is_unspecified_and_source_exists : CompilerTestBase
    {
        private string Compile(string path)
        {
            new Compiler().Compile(new CompilerOptions
            {
                Compile = true,
                Path = path
            });

            var dest = Path.Combine(path, "valid.js");
            return dest;
        }



        [Test]
        public void result_should_be_in_place()
        {
            var sourceDir = ExampleScripts.Valid.Relative;
            var dest = Compile(sourceDir);
            Assert.IsTrue(File.Exists(dest),"File " + dest + " should be compiled in place");
            AssertOutputMirrorsSource(sourceDirectory: sourceDir.AsDirectory(), outputDirectory: sourceDir.AsDirectory());
        }


        [Test]
        public void relative_path_should_also_work()
        {
            var sourceDir = ExampleScripts.Valid.Relative;
            var dest = Compile(sourceDir);
            Assert.IsTrue(File.Exists(dest), "Relative path should work.");
            AssertOutputMirrorsSource(sourceDirectory: sourceDir.AsDirectory(), outputDirectory: sourceDir.AsDirectory());
        }

        [TearDown]
        public void Cleanup()
        {
            var dest = Path.ChangeExtension(ExampleScripts.Valid.SingleScript.ToString(), ".js");
            File.Delete(dest);
        }

    }
}
