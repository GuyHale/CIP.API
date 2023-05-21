﻿namespace CIP.API.Models
{
    public class Cryptocurrency
    {
        private static readonly Cryptocurrency DefaultInstance = new();
        public short Rank { get; set; }
        public string? Name { get; set; }
        public string? Abbreviation { get; set; }
        public string? USDValuation { get; set; }
        public string? MarketCap { get; set; }
        public string? Description { get; set; }

        public static Cryptocurrency Empty() => DefaultInstance;
    }
}
