using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons;

public class Pagination<T>
{
    public int TotalItemsCount { get; set; }
    public int PageSize { get; set; }
    public int TotalPagesCount
    {
        get
        {
            var temp = TotalItemsCount / PageSize;
            if (TotalItemsCount % PageSize == 0)
            {
                temp += 1;
            }
            return temp;
        }
    }/// <summary>
    /// Page number start from 1.
    /// </summary>
    public int PageIndex { get; set; }
    public bool Next => PageIndex + 1 < TotalPagesCount;
    public bool Previous => PageIndex > 1;
    public ICollection<T> Items { get; set; }
}