using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class TableJoin
    {
        public string TableName { get; set; }
        public string TypeJoin { get; set; }
        public string RerefenceTableFieldName { get; set; }
        public string FieldName { get; set; }
    }

}
