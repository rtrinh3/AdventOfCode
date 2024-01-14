using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocCommon
{
    public static class Constants
    {
        public const StringSplitOptions TrimAndDiscard = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        public static string[] SplitLines(string input)
        {
            return input.ReplaceLineEndings("\n").Split('\n', TrimAndDiscard);
        }
    }
}
