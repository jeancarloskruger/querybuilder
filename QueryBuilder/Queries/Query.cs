using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
    public class Query : BaseQuery
    {
        public List<BaseColumn> FilterColumns { get; set; }
    }
}
