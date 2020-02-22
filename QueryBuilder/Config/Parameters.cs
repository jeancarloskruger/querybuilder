using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder.Config
{
    public static class Parameters
    {
        public static readonly List<string> BasicOperators = new List<string> { "=", ">", "<", ">=", "<=", "<>", "IN", "LIKE", "NOT LIKE" };
        public static readonly List<string> SubQueryOperators = new List<string> { "= ALL", "> ALL", "< ALL", ">= ALL", "<= ALL", "<> ALL",
            "= ANY", "> ANY", "< ANY", ">= ANY", "<= ANY", "<> ANY",
             "= SOME", "> SOME", "< SOME", ">= SOME", "<= SOME", "<> SOME","IN"};
    }
}
