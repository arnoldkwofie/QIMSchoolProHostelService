using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Application.Shared.Common.Models
{
    public class PaginatedCommand
    {

        public int PageSize { get; set; } = 10;
        [JsonProperty("currentPage")]

        public int PageNumber { get; set; } = 1;

        [JsonProperty("start")]
        public int Take { get; set; } = 0;
        public string OrderBy { get; set; }
        public int Skip { get; set; }

        public string Filter { get; set; }

        public string Search { get; set; }
        public string OtherJson { get; set; }

    }
    public class PaginatedQuery
    {
        public IQueryCollection FormQuery { get; }

        public PaginatedQuery(IQueryCollection formQuery)
        {
            FormQuery = formQuery;
        }

        private readonly int _pageSize = 10;
        private readonly int _pageNumber = 1;
        private readonly int _start = 0;

        public int PageSize => string.IsNullOrEmpty(FormQuery["pageSize"].ToString()) ? _pageSize : Convert.ToInt32(FormQuery["pageSize"].ToString() ?? $"{_pageSize}");

        public int PageNumber => string.IsNullOrEmpty(FormQuery["pageNumber"].ToString())
                    ? _pageNumber
                    : Convert.ToInt32(FormQuery["pageNumber"].ToString() ?? $"{_pageNumber}");


        public int Take => string.IsNullOrEmpty(FormQuery["start"].ToString())
                    ? _start
                    : Convert.ToInt32(FormQuery["start"].ToString() ?? $"{_start}");

        public string OrderBy => FormQuery["orderBy"].ToString();
        public string Filter => FormQuery["filter"].ToString();
        public string Search => FormQuery["search"].ToString().ToLower();

    }
    public class QueryBase
    {
        public IQueryCollection FormQuery { get; }

        public QueryBase(IQueryCollection formQuery)
        {
            FormQuery = formQuery;
        }



    }
    public class PaginatedQueryValidator : AbstractValidator<PaginatedQuery>
    {
        public PaginatedQueryValidator()
        {
            RuleFor(a => a.PageNumber)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be more than 1 and cannot be {PropertyValue}");

            RuleFor(a => a.PageSize)
                .GreaterThan(2);


            RuleFor(a => a.Filter)
                // .Length(3,1000)

                .Must(a => string.IsNullOrEmpty(a) || a.Contains(":"))
                .WithMessage("{PropertyName} must contain : Eg 1) key:filterValue  2) key:filterValue contains NB: The contains is the filter type, by default is Equal");

        }
    }
}
