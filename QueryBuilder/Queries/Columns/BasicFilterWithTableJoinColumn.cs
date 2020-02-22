using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class BasicFilterWithTableJoinColumn : BaseColumn
    {
        public string JoinTable { get; set; }
        public string JoinTableColumn { get; set; }

        public BasicFilterWithTableJoinColumn(ReaderFilterColumns filter)
            : base(filter.Operator, filter.FieldName, filter.TypeFilter)
        {
            JoinTable = filter.JoinTable;
            JoinTableColumn = filter.JoinTableColumn;
        }

        public override string FilterCommand()
        {
            return $"{FieldName} {Operator} ({JoinTable}.{JoinTableColumn})";
        }
    }
}
