using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class SubQueryFilterColumn : BaseColumn
    {
        public SubQueryFilterColumn(ReaderFilterColumns filter)
            : base(filter.Operator, filter.FieldName, filter.TypeFilter)
        {
            SubQuery = new QueryFactory(filter.SubQuery);
        }

        public QueryFactory SubQuery { get; set; }
        public override string FilterCommand()
        {
            return $"{FieldName} {Operator} ({SubQuery.MakeQuery()})";
        }
    }
}
