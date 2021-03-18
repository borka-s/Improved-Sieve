namespace ImprovedSieve.Core.Models
{
    public class SieveOptions
    {
        public int DefaultPageSize { get; set; }

        public int MaxPageSize { get; set; }

        public bool ThrowExceptions { get; set; }

        public bool IgnoreSortingNulls { get; set; }

        public static SieveOptions Defaults()
        {
            return new SieveOptions
            {
                DefaultPageSize = 10,
                MaxPageSize = 100,
                ThrowExceptions = false,
                IgnoreSortingNulls = false,
            };
        }
    }
}