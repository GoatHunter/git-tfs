using System.ComponentModel;
using NDesk.Options;
using Sep.Git.Tfs.Util;
using System.IO;

namespace Sep.Git.Tfs.Commands
{
    using System;

    [StructureMapSingleton]
    public class RemoteOptions
    {
        public OptionSet OptionSet
        {
            get
            {
                return new OptionSet
                {
                    { "ignore-regex=", "A regex of files to ignore",
                        v => IgnoreRegex = v },
                    { "ignore-regex-file=", "A file containing regex of files to ignore",
                        v => IgnoreRegex = string.Join("|",File.ReadAllLines(v)) },
                    { "except-regex=", "A regex of exceptions to '--ignore-regex'",
                        v => ExceptRegex = v},
                    { "u|username=", "TFS username",
                        v => Username = v },
                    { "p|password=", "TFS password",
                        v => Password = v },
                };
            }
        }

        public string IgnoreRegex { get; set; }

        public string ExceptRegex { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
