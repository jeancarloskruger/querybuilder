using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class ReaderFilterColumns : BaseColumn
    {
        public string FieldValue { get; set; }
        public string JoinTable { get; set; }
        public string JoinTableColumn { get; set; }
        public string FieldValueFirst { get; set; }
        public string FieldValueSecond { get; set; }
        public ReaderQuery SubQuery { get; set; }
    }

}
