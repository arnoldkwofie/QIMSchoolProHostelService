using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Qface.Application.Shared.Common.Models
{
    
        public class PaginatedList<T>
        {
            public List<T> Data { get; set; }
            public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
            public int PageSize { get; }
            public int TotalCount { get; }
            public bool HasPrevious => CurrentPage > 1;
            public bool HasNext => CurrentPage < TotalPages;
            public int From => ((CurrentPage - 1) * PageSize) + 1;
            public int To => (From + PageSize) - 1;
            public int CurrentPage { get; }

            public PaginatedList(List<T> data,
                             int totalCount,
                             int currentPage,
                             int pageSize)
            {
                Data = data;
                CurrentPage = currentPage;
                PageSize = pageSize;
                TotalCount = totalCount;
            }

            public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
            {
                var count = await source.CountAsync();
                var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

                return new PaginatedList<T>(items, count, pageIndex, pageSize);
            }

            public PaginatedList<TDest> Map<TDest>(IMapper mapper)
            {
                List<TDest> data = mapper.Map<List<TDest>>(Data);
                return new PaginatedList<TDest>(data: data,
                                          totalCount: TotalCount,
                                          currentPage: CurrentPage,
                                          pageSize: PageSize);

            }
        }

    
}
