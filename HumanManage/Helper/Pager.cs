using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanManage.Helpers
{
    public class Pager<T> : List<T>
    {

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }

        public Pager(IQueryable<T> source, int pageIndex)
        {
            PageIndex = pageIndex - 1;
            CurrentPage = pageIndex;
            PageSize = 10;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            this.AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
        }

        public Pager(IQueryable<T> source, int pageIndex,int pageSize)
        {
            PageIndex = pageIndex - 1;
            CurrentPage = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            this.AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex + 1 < TotalPages);
            }
        }

        public List<int> PageList
        {
            get
            {
                List<int> list = new List<int>();
                if (TotalPages < 10)
                {
                    for (int i = 1; i <= TotalPages; i++)
                    {
                        list.Add(i);
                    }
                }
                else
                {
                    if (CurrentPage <= 5)
                    {
                        for (int i = 1; i <= 10; i++)
                        {
                            list.Add(i);
                        }
                    }
                    else if (CurrentPage > 5 && CurrentPage <= TotalPages - 5)
                    {
                        for (int i = CurrentPage - 5; i <= CurrentPage; i++)
                        {
                            list.Add(i);
                        }
                        for (int i = CurrentPage + 1; i < CurrentPage + 5; i++)
                        {
                            list.Add(i);
                        }
                    }
                    else
                    {
                        for (int m = TotalPages - 9; m < CurrentPage; m++)
                        {
                            list.Add(m);
                        }
                        for (int m = CurrentPage; m <= TotalPages; m++)
                        {
                            list.Add(m);
                        }
                        
                    }
                }
                return list;
            }
        }
    }
}
