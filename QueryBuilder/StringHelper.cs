using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public static class StringHelper
    {
        public static string ConvertFieldValueToSpeficType(string type, string value)
        {
            switch (type)
            {
                case "string":
                    return $"'{value}'";
                default:
                    return value;
            }
        }
    }
}
