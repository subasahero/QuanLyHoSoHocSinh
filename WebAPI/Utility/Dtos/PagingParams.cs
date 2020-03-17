using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Dtos
{
    public class PagingParams
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; }
        public string Keyword { get; set; }
        public string SortValue { get; set; }
        public string Sortkey { get; set; }
        public string SearchValue { get; set; }
        public string SearchKey { get; set; }
        public string LevelIdValue { get; set; }
        public string gradeId { get; set; }
    }
}
