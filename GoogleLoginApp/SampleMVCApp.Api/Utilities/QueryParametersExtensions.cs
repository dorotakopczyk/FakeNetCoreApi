using System;
using System.Linq;
using SampleMVCApp.Api.Models;

namespace SampleMVCApp.Api.Utilities
{
    public static class QueryParametersExtensions
    {
        public static bool HasPrevious(this QueryParameters queryParameters)
        {
            return (queryParameters.Page > 1);
        }

        public static bool HasNext(this QueryParameters queryParameters, int totalCount)
        {
            return (queryParameters.Page < (int)GetTotalPages(queryParameters, totalCount));
        }

        public static double GetTotalPages(this QueryParameters queryParameters, int totalCount)
        {
            return Math.Ceiling(totalCount / (double)queryParameters.PageCount);
        }

        public static bool HasQuery(this QueryParameters queryParameters)
        {
            return !String.IsNullOrEmpty(queryParameters.Query);
        }

        public static bool IsDescending(this QueryParameters queryParameters)
        {
            if (!String.IsNullOrEmpty(queryParameters.OrderBy))
            {
                var x = queryParameters.OrderBy.Split(' ');//.Last().ToLowerInvariant().StartsWith("desc");
                #pragma warning disable RECS0063 // Warns when a culture-aware 'StartsWith' call is used by default.
                return Enumerable.Last(x.ToList()).ToLowerInvariant().StartsWith("desc");
                #pragma warning restore RECS0063 // Warns when a culture-aware 'StartsWith' call is used by default.
            }
            return false;
        }
    }
}
