using CoffeeScript.Compiler.Util;

namespace CoffeeScript.Compiler
{
    public class CompilerOptions
    {
        private string _outputDir;
        public bool Compile { get; set; }
        public bool Print { get; set; }
        public bool Help { get; set; }
        public bool Bare { get; set; }
        public string OutputDir
        {
            get
            {
                return _outputDir.IsNullOrWhiteSpace() ? Path : _outputDir;
            }
            set { _outputDir = value; }
        }


        public bool Watch { get; set; }
        public string Path { get; set; }
    }
}