using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    public class Arguments
    {
        private readonly string[] args;

        public Arguments(string[] args)
        {
            this.args = args;
        }

        public string GetArgument(string name) =>
            args[Array.IndexOf(args, name) + 1];

        public string FileJson => GetArgument("-fileJson");
    }
}
