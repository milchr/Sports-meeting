using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsMeeting.Shared.Dto
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemTo { get; set; }
        public int TotalItemCount { get; set; }

        public PageResult(List<T> items, int totalCount, int quantity, int page)
        {
            Items = items;
            TotalItemCount = totalCount;
            ItemFrom = quantity * (page - 1) + 1;
            ItemTo = ItemFrom + quantity - 1;
            TotalPages = (int)Math.Ceiling(totalCount /(double) quantity);
        }

    }
}
