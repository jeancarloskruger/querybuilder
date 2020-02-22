﻿using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class BasicFilterColumn : BaseColumn
    {
        public string FieldValue { get; set; }

        public BasicFilterColumn(ReaderFilterColumns filter)
            : base(filter.Operator, filter.FieldName, filter.TypeFilter)
        {
            FieldValue = filter.FieldValue;
        }


        public override string FilterCommand()
        {
            return $"{FieldName} {Operator} ({FieldValue})";
        }
    }
}
