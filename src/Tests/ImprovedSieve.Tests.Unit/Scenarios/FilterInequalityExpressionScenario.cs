using System;
using System.Linq;
using ImprovedSieve.Core;
using ImprovedSieve.Core.Extensions;
using ImprovedSieve.Core.Models;
using Xunit;

namespace ImprovedSieve.Tests.Unit.Scenarios
{
    public class FilterInequalityExpressionScenario : IDisposable
    {
        public void Dispose()
        {
            SieveProcessor.Current.Init(SieveOptions.Defaults());
        }

        #region GreaterThan

        [Fact]
        public void IntegerGreaterThan()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Age>30",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DoubleGreaterThan()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Score>89.9d",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DateTimeGreaterThan()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = $"LastPlayed>{DateTime.Now.AddHours(-1):O}",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void NestedFloatGreaterThan()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Score>4.1f",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void NestedDecimalGreaterThan()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Price>49.6m",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        #endregion

        #region GreaterThanEqual

        [Fact]
        public void IntegerGreaterThanEqual()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Age>=38",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DoubleGreaterThanEqual()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Score>=92.4d",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DateTimeGreaterThanEqual()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = $"LastPlayed>={new DateTime(2020, 3, 15, 18, 22, 24):O}",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void NestedFloatGreaterThanEqual()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Score>=4.4f",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void NestedDecimalGreaterThanEqual()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Price>=55.5m",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        #endregion


        #region LessThan

        [Fact]
        public void IntegerLessThan()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Age<21",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DoubleLessThan()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Score<60",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void DateTimeLessThan()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = $"LastPlayed<{DateTime.Now.AddHours(-1):O}",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void NestedFloatLessThan()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Score<4.4f",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void NestedDecimalLessThan()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Price<49.5m",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        #endregion

        #region LessThanEqual

        [Fact]
        public void IntegerLessThanEqual()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Age<=21",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void DoubleLessThanEqual()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Score<=62.47",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void DateTimeLessThanEqual()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = $"LastPlayed<={new DateTime(2020, 3, 15, 18, 22, 24):O}",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void NestedFloatLessThanEqual()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Score<=4.4f",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void NestedDecimalLessThanEqual()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Price<=49.5m",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        #endregion
    }
}