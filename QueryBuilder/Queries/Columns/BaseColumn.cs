using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
   public class BaseColumn
    {
        public BaseColumn() { }
        public string Operator { get; set; }
        public string FieldName { get; set; }
        public string TypeFilter { get; set; }

        public BaseColumn(string aOperator, string fieldName, string typeFilter)
        {
            Operator = aOperator;
            FieldName = fieldName;
            TypeFilter = typeFilter;
        }
        public virtual string FilterCommand()
        {
            return "1=1";
        }
    }
}
