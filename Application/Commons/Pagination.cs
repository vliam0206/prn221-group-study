using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons;

public class Pagination<T> {
    public int TotalItemsCount { get; set; }
    public int PageSize { get; set; }
    public int TotalPagesCount {
        get {
            var temp = TotalItemsCount / PageSize;
            if (TotalItemsCount % PageSize == 0) {
                return temp;
            }
            return temp + 1;
        }
    }
    public int PageIndex { get; set; }

    /// <summary>
    /// page number start from 0
    /// </summary>
    public bool HasNext => PageIndex + 1 < TotalPagesCount;
    public bool HasPrevious => PageIndex > 0;
    public ICollection<T> Items { get; set; }
}