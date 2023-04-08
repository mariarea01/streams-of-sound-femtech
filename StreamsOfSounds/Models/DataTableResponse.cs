using System;
using DataTables;

namespace StreamsOfSound.Models
{
    public class DataTableResponse
    {
        public int Draw { get; set; }
        public long OpportunitiesTotal { get; set; }
        public int OpportunitiesFiltered { get; set; }
        public object[]? Data { get; set; }
        public string? Error { get; set; }
    }
}

