using System;
using System.Collections.Generic;
using System.Linq;

namespace PastryFlow.Application.Common;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;

    public PagedResult<TResult> Map<TResult>(Func<T, TResult> mapper)
    {
        return new PagedResult<TResult>
        {
            Items = Items.Select(mapper).ToList(),
            TotalCount = TotalCount,
            PageNumber = PageNumber,
            PageSize = PageSize
        };
    }
}
