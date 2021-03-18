using System;

namespace ImprovedSieve.Tests.Unit.Models
{
    public class Player
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; }

        public int Age { get; set; }

        public double Score { get; set; }

        public bool IsOnline { get; set; }

        public DateTime LastPlayed { get; set; }

        public Game FavoriteGame { get; set; }
    }
}