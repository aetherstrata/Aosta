// See https://aka.ms/new-console-template for more information

using Aosta.Jikan;

var jikan = new JikanConfiguration().Build();

Console.WriteLine(jikan.GetAnimeAsync(1).Result.Data.Episodes);
