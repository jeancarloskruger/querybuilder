using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class BasicBetweenFilterColumn : BaseColumn
    {
        public string FieldValueFirst { get; set; }
        public string FieldValueSecond { get; set; }

        public BasicBetweenFilterColumn(ReaderFilterColumns filter)
            : base(filter.Operator, filter.FieldName, filter.TypeFilter, filter.TypeColumn)
        {
            FieldValueFirst = filter.FieldValueFirst;
            FieldValueSecond = filter.FieldValueSecond;
        }

        public override string FilterCommand()
        {
            return $"{FieldName} {Operator} {StringHelper.ConvertFieldValueToSpeficType(TypeColumn, FieldValueFirst)} AND {StringHelper.ConvertFieldValueToSpeficType(TypeColumn, FieldValueSecond)}";
        }
    }
}
