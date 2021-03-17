namespace ImprovedSieve.Core.Models
{
    public class SieveOptions
    {
        public int DefaultPageSize { get; set; }

        public int MaxPageSize { get; set; }

        public bool ThrowExceptions { get; set; }

        public static SieveOptions Defaults()
        {
            return new SieveOptions
            {
                DefaultPageSize = 0,
                MaxPageSize = 0,
                ThrowExceptions = false,
            };
        }
    }
}