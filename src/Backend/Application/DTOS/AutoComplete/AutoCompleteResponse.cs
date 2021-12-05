using System;

namespace EvrenDev.Application.DTOS.AutoComplete
{
    public class AutoCompleteResponse
    {
        public Guid Id{ get; set; }

        public string Selector { get; set; }

        public string Value { get; set; }
    }
}
