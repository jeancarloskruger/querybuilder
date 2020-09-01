using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class BaseQuery
    {
        public string TableName { get; set; }
        public List<string> SelectColumns { get; set; }
        public List<TableJoin> TableJoins { get; set; }

    }
}
