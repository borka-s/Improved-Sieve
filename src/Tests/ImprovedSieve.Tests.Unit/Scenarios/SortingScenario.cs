using System;
using System.Linq;
using ImprovedSieve.Core;
using ImprovedSieve.Core.Extensions;
using ImprovedSieve.Core.Models;
using Xunit;

namespace ImprovedSieve.Tests.Unit.Scenarios
{
    public class SortingScenario
    {
        [Fact]
        public void NestedSortingWithNulls()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Sorts = "FavoriteGame.Price",
            };

            var result = query.ApplySorts(sieveModel).ToList();
            var first = result.First();

            Assert.Equal(new Guid("26f37896-60ab-47fe-9a41-8a5debfc407c"), first.Id);
        }

        [Fact]
        public void NestedSorting_WithNulls_IgnoreNullsSetInModel_ThrowsException()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Sorts = "!FavoriteGame.Price",
            };

            Assert.Throws<NullReferenceException>(() => query.ApplySorts(sieveModel).ToList());
        }

        [Fact]
        public void NestedSorting_WithNulls_IgnoreNullsSetInOptions_ThrowsException()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true, IgnoreSortingNulls = true});
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Sorts = "FavoriteGame.Price",
            };

            Assert.Throws<NullReferenceException>(() => query.ApplySorts(sieveModel).ToList());
        }

        [Fact]
        public void NestedSortingWithoutNulls()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Sorts = "FavoriteGame.Price",
            };

            var result = query.ApplySorts(sieveModel).ToList();
            var first = result.First();

            Assert.Equal(new Guid("dc309fea-7d6b-4a2b-a8af-bbbfa67cd4b5"), first.Id);
        }

        [Fact]
        public void SortingBools()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Sorts = "-IsOnline",
            };

            var result = query.ApplySorts(sieveModel).ToList();

            Assert.Equal(new Guid("ebc62dbf-0e9f-4f37-a1b8-40d6c63986c6"), result.First().Id);
        }
    }
}