using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Antlr4.Runtime.Tree;

namespace ImprovedSieve.Core.Models
{
    public class SieveModel
    {
        private readonly ParserBase _filterParser;
        private readonly ParserBase _sortByParser;

        private string _filters;

        [DataMember]
        public string Filters
        {
            get => _filters;
            set
            {
                _filters = value;
                FilterTree = _filterParser.Parse(value);
            }
        }

        private string _sorts;

        [DataMember]
        public string Sorts
        {
            get => _sorts;
            set
            {
                _sorts = value;
                SortTree = _sortByParser.Parse(value);
            }
        }

        [DataMember]
        [Range(1, int.MaxValue)]
        public int? Page { get; set; }

        [DataMember]
        [Range(1, int.MaxValue)]
        public int? PageSize { get; set; }

        public IParseTree FilterTree { get; private set; }

        public IParseTree SortTree { get; private set; }

        public SieveModel()
        {
            _filterParser = new FilterParser();
            _sortByParser = new SortByParser();
        }

        public SieveModel(ParserBase filterParser, ParserBase sortByParser)
        {
            _filterParser = filterParser;
            _sortByParser = sortByParser;
        }
    }
}