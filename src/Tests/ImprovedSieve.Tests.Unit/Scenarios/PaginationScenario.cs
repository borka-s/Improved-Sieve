using System.Linq;
using ImprovedSieve.Core;
using ImprovedSieve.Core.Extensions;
using ImprovedSieve.Core.Models;
using Xunit;

namespace ImprovedSieve.Tests.Unit.Scenarios
{
    [Collection(Consts.SieveCollection)]
    public class PaginationScenario
    {
        [Fact]
        public void FirstThreeItems()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Page = 1,
                PageSize = 3,
            };

            var result = query.ApplyPagination(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void SecondPage()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Page = 2,
                PageSize = 3,
            };

            var result = query.ApplyPagination(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DefaultPageSizeWithPageSet()
        {
            SieveProcessor.Current.Init(new SieveOptions { DefaultPageSize = 3 });

            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Page = 2,
            };

            var result = query.ApplyPagination(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DefaultPageSizeWithoutPage()
        {
            SieveProcessor.Current.Init(new SieveOptions { DefaultPageSize = 3 });

            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel();

            var result = query.ApplyPagination(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void PageSizeOverTheMaxPageSizeLimit()
        {
            SieveProcessor.Current.Init(new SieveOptions { MaxPageSize = 3 });

            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Page = 1,
                PageSize = 10
            };

            var result = query.ApplyPagination(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void PageSizeNotOverTheMaxPageSizeLimit()
        {
            SieveProcessor.Current.Init(new SieveOptions { MaxPageSize = 3 });

            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Page = 1,
                PageSize = 2
            };

            var result = query.ApplyPagination(sieveModel);

            Assert.Equal(2, result.Count());
        }
    }
}