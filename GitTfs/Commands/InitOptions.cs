using System;
using System.ComponentModel;
using NDesk.Options;
using Sep.Git.Tfs.Util;
using System.IO;

namespace Sep.Git.Tfs.Commands
{
    [StructureMapSingleton]
    public class InitOptions
    {
        private const string default_autocrlf = "false";

        public InitOptions() { GitInitAutoCrlf = default_autocrlf; }

        public OptionSet OptionSet
        {
            get
            {
                return new OptionSet
                {
                    { "template=", "Passed to git-init",
                        v => GitInitTemplate = v },
                    { "shared:", "Passed to git-init",
                        v => GitInitShared = v == null ? (object)true : (object)v },
                    { "autocrlf=", "Normalize line endings (default: " + default_autocrlf + ")",
                        v => GitInitAutoCrlf = ValidateCrlfValue(v) },
                    { "ignorecase=", "Ignore case in file paths (default: system default)",
                        v => GitInitIgnoreCase = ValidateIgnoreCaseValue(v) },
                    {"bare", "Clone the TFS repository in a bare git repository", v => IsBare = v != null},
                    {"workspace=", "Set tfs workspace to a specific folder (a shorter path is better!)", v => WorkspacePath = v},
                    {"gitignore=", "Path toward the .gitignore file which be committed and used to ignore files", v => GitIgnorePath = v},
                    {"skip-branches-file", "Path to file containing a list of TFS branches that should be skipped.", v => BranchesToSkip = string.Join("|", File.ReadAllLines(v))}
                };
            }
        }

        string ValidateCrlfValue(string v)
        {
            string[] valid = { "false", "true", "auto" };
            if (!Array.Exists(valid, s => v == s))
                throw new OptionException("error: autocrlf value must be one of true, false or auto", "autocrlf");
            return v;
        }

        string ValidateIgnoreCaseValue(string v)
        {
            string[] valid = { "false", "true" };
            if (!Array.Exists(valid, s => v == s))
                throw new OptionException("error: ignorecase value must be true or false", "ignorecase");
            return v;
        }


        public bool IsBare { get; set; }
        public string WorkspacePath { get; set; }
        public string GitInitTemplate { get; set; }
        public object GitInitShared { get; set; }
        public string GitInitAutoCrlf { get; set; }
        public string GitInitIgnoreCase { get; set; }
        public string GitIgnorePath { get; set; }
        public string BranchesToSkip { get; set; }
    }
}
