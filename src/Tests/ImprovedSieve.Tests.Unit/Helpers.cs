using System;
using System.Collections.Generic;
using System.Linq;
using ImprovedSieve.Tests.Unit.Models;

namespace ImprovedSieve.Tests.Unit
{
    public class Helpers
    {
        public static IQueryable<Player> GetPlayersList(bool withoutNulls = false)
        {
            var players = new List<Player>
            {
                new Player
                {
                    Id = Guid.Parse("ebc62dbf-0e9f-4f37-a1b8-40d6c63986c6"),
                    Nickname = "alice",
                    Age = 16,
                    Score = 55.7,
                    IsOnline = true,
                    LastPlayed = DateTime.Now,
                    FavoriteGame = new Game
                    {
                        Title = "Hitman 3",
                        Price = 59.99m,
                        Score = 4.7f
                    },
                },
                new Player
                {
                    Id = Guid.Parse("26f37896-60ab-47fe-9a41-8a5debfc407c"),
                    Nickname = "bob",
                    Age = 42,
                    Score = 34.2,
                    IsOnline = false,
                    LastPlayed = DateTime.Now.AddDays(-3),
                    FavoriteGame = null
                },
                new Player
                {
                    Id = Guid.Parse("9bb8b9a0-7527-46ca-86c0-27ecc095e43a"),
                    Nickname = "петар",
                    Age = 19,
                    Score = 92.4,
                    IsOnline = false,
                    LastPlayed = DateTime.Now.AddHours(-4),
                    FavoriteGame = new Game
                    {
                        Title = "Spiderman",
                        Price = 49.5m,
                        Score = 4.4f
                    },
                },
                new Player
                {
                    Id = Guid.Parse("dc309fea-7d6b-4a2b-a8af-bbbfa67cd4b5"),
                    Nickname = "jane",
                    Age = 21,
                    Score = 97.55,
                    IsOnline = true,
                    LastPlayed = DateTime.Now,
                    FavoriteGame = new Game
                    {
                        Title = "The Last Of Us",
                        Price = 30.25m,
                        Score = 3.8f
                    },
                },
                new Player
                {
                    Id = Guid.Parse("86f71120-1d04-41e0-bbd2-5a0f7ec05817"),
                    Nickname = "john",
                    Age = 38,
                    Score = 62.47,
                    IsOnline = false,
                    LastPlayed = new DateTime(2020, 3, 15, 18, 22, 24),
                    FavoriteGame = new Game
                    {
                        Title = "Red Dead Redemption 2",
                        Price = 55.5m,
                        Score = 4.3f
                    },
                },
            };

            if (withoutNulls)
            {
                players = players.Where(x => x.FavoriteGame != null).ToList();
            }

            return players.AsQueryable();
        }
    }
}