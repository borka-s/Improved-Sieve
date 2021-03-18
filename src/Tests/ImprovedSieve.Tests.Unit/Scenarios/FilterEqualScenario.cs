using System;
using System.Linq;
using ImprovedSieve.Core;
using ImprovedSieve.Core.Extensions;
using ImprovedSieve.Core.Models;
using Xunit;

namespace ImprovedSieve.Tests.Unit.Scenarios
{
    [Collection(Consts.SieveCollection)]
    public class FilterEqualExpressionsScenario : IDisposable
    {
        public void Dispose()
        {
            SieveProcessor.Current.Init(SieveOptions.Defaults());
        }

        [Fact]
        public void BoolEqualsTrue()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "IsOnline==true",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void BoolEqualsFalse()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "IsOnline==false",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void BoolEqualsWithoutArgument()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "IsOnline",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void StringEquals()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname=='alice'",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void StringEqualsUnicode()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname=='петар'",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void IntegerEquals()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Age==16",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void DoubleEquals()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Score==55.7d",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void DateTimeEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = $"LastPlayed=={new DateTime(2020, 3, 15, 18, 22, 24):O}",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void GuidEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Id==26f37896-60ab-47fe-9a41-8a5debfc407c",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void NullEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame==null",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void NestedFloatEquals()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Score==4.7f",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void NestedDecimalEquals()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Price==30.25m",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void StringEqualsWithoutArgumentThrowsException()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname",
            };

            Assert.Throws<Exception>(() => query.ApplyFilters(sieveModel));
        }

        [Fact]
        public void BoolNotEqualsTrue()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "IsOnline!=true",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void BoolNotEqualsFalse()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "IsOnline!=false",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void BoolNotEqualsWithoutArgument()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "!IsOnline",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }


        [Fact]
        public void StringNotEquals()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname!='alice'",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void StringNotEqualsUnicode()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname!='петар'",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void IntegerNotEquals()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Age!=16",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void DoubleNotEquals()
        {
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Score!=55.7d",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void DateTimeNotEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = $"LastPlayed!={new DateTime(2020, 3, 15, 18, 22, 24):O}",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void GuidNotEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Id!=26f37896-60ab-47fe-9a41-8a5debfc407c",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void NullNotEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame!=null",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void NestedFloatNotEquals()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Score!=4.7f",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void NestedDecimalNotEquals()
        {
            var query = Helpers.GetPlayersList(true);

            var sieveModel = new SieveModel
            {
                Filters = "FavoriteGame.Price!=30.25m",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ConditionalEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname=='alice'|'bob'",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(2, result.Count());
            Assert.True(result.Any(x => x.Nickname == "alice"));
            Assert.True(result.Any(x => x.Nickname == "bob"));
        }

        [Fact]
        public void ConditionalEqualsUnicode()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname=='alice'|'bob'|'петар'",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
            Assert.True(result.Any(x => x.Nickname == "alice"));
            Assert.True(result.Any(x => x.Nickname == "bob"));
            Assert.True(result.Any(x => x.Nickname == "петар"));
        }

        [Fact]
        public void ComplexConditionalOrEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname=='alice'|'bob'|Age==21",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(3, result.Count());
            Assert.True(result.Any(x => x.Nickname == "alice"));
            Assert.True(result.Any(x => x.Nickname == "bob"));
            Assert.True(result.Any(x => x.Nickname == "jane"));
        }

        [Fact]
        public void ComplexConditionalAndEquals()
        {
            SieveProcessor.Current.Init(new SieveOptions { ThrowExceptions = true });
            var query = Helpers.GetPlayersList();

            var sieveModel = new SieveModel
            {
                Filters = "Nickname=='alice'|'jane',Age==21",
            };

            var result = query.ApplyFilters(sieveModel);

            Assert.Equal(1, result.Count());
            Assert.True(result.Any(x => x.Nickname == "jane"));
        }
    }
}