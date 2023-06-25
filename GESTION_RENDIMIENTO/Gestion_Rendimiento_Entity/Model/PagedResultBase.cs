using System;
using System.Collections.Generic;
using System.Linq;

namespace Gestion_Rendimiento_Entity.Model
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public ICollection<T> Results { get; set; }

        public PagedResult()
        {
            Results = new HashSet<T>();
        }
    }

    public class PagedResultFor<T, U> : PagedResult<T>
        where U : class
        where T : class
    {
        public PagedResultFor(PagedResult<U> pagedResult, IEnumerable<T> rows)
        {
            CurrentPage = pagedResult.CurrentPage;
            PageCount = pagedResult.PageCount;
            PageSize = pagedResult.PageSize;
            RowCount = pagedResult.RowCount;

            if (rows != null)
            { Results = rows.ToList(); }
        }

        public PagedResult<T> Result => this;
    }
}
