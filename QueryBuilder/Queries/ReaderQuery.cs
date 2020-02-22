using System;
using System.Collections.Generic;
using System.Text;

namespace QueryBuilder
{
   public class ReaderQuery : BaseQuery
    {
        public List<ReaderFilterColumns> FilterColumns { get; set; }
    }
}
