﻿using System.Collections.Generic;
using System.Linq;

namespace Stryker.Core.Options.Options
{
    public class DiffIgnoreFilePatternsOption : BaseStrykerOption<IEnumerable<FilePattern>>
    {
        public DiffIgnoreFilePatternsOption(IEnumerable<string> diffIgnores)
        {
            var diffIgnoreFilePatterns = new List<FilePattern>();
            if (diffIgnores != null)
            {
                foreach (var pattern in diffIgnores)
                {
                    diffIgnoreFilePatterns.Add(FilePattern.Parse(FilePathUtils.NormalizePathSeparators(pattern)));
                }

                Value = diffIgnoreFilePatterns;
            }
        }

        public override StrykerOption Type => StrykerOption.DiffIgnoreFilePatterns;

        public override string HelpText => @"Allows to specify an array of C# files which should be ignored if present in the diff.
             Any non-excluded files will trigger all mutants to be tested because we cannot determine what mutants are affected by these files. 
            This feature is only recommended when you are sure these files will not affect results, or when you are prepared to sacrifice accuracy for perfomance.
            
            Use glob syntax for wildcards: https://en.wikipedia.org/wiki/Glob_(programming)
            Example: ['**/*Assets.json','**/favicon.ico']";

        public override IEnumerable<FilePattern> DefaultValue => Enumerable.Empty<FilePattern>();
    }
}