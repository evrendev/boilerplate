using System;
using System.Collections.Generic;

namespace EvrenDev.Application.Interfaces.Result
{
    public class PaginatedResult<T> : Result
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }
        public List<T> Data { get; set; }

        internal PaginatedResult(bool error, 
            List<T> data = default, 
            List<string> messages = null, 
            long count = 0, 
            int page = 1, 
            int pageSize = 25)
        {
            Data = data;
            Page = page;
            Error = error;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }
        public static PaginatedResult<T> Failure(List<string> messages)
        {
            return new PaginatedResult<T>(error: true, data: default, messages: messages);
        }

        public static PaginatedResult<T> Success(List<T> data, long count, int page, int pageSize)
        {

            return new PaginatedResult<T>(error: false, data: data, messages: null, count: count, page: page, pageSize: pageSize);
        }
        public int Page { get; set; }

        public int TotalPages { get; set; }

        public long TotalCount { get; set; }

        public bool HasPreviousPage => Page > 1;

        public bool HasNextPage => Page < TotalPages;
    }
}
