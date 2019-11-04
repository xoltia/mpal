using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using CommandLine;
using MPAL.Entities;
using Newtonsoft.Json;

namespace MPAL
{
    class Program
    {
        // TODO: purge and viewing commands

        static AnimeListManager manager = new AnimeListManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "xoltia/mpal/data.json"));
        static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<Anime, AnimeUpdate, RemoveOptions, FinishOptions, SyncOptions>(args)
                .MapResult(
                    (Anime anime) => NewAnime(anime),
                    (AnimeUpdate anime) => UpdateAnime(anime),
                    (RemoveOptions opts) => RemoveAnime(opts.Name),
                    (FinishOptions opts) => FinishAnime(opts.Name),
                    (SyncOptions opts) => SyncAnimes(opts),
                    errors => 1
                );
        }

        public static int TryActionAndSave(Action action)
        {
            try
            {
                action();
                manager.Save();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }

            return 0;
        }

        private static int NewAnime(Anime anime) =>
            TryActionAndSave(() => manager.Add(anime));

        private static int UpdateAnime(AnimeUpdate anime) =>
            TryActionAndSave(() => manager.Update(anime));

        private static int RemoveAnime(string anime) =>
            TryActionAndSave(() => manager.Remove(anime));

        private static int FinishAnime(string anime) =>
            TryActionAndSave(() => manager.Finish(anime));

        /*
         * TODO:
         * This is more of a statement than an actual todo note.
         * This is likely one of the most horrendous things I've ever had the privilege
         * of typing and I know that I'll probably be to lazy to ever come back and clean
         * this up. But hey, if it ain't broke don't fix it. If you do feel like you're
         * up to the task future me, that'd be great.
         */
        private static int SyncAnimes(SyncOptions opts)
        {
            // That'd be a mess if you made a typo
            Console.Write($"You are about to add all the entries from {opts.Username} on MyAnimeList, are you sure you'd like to make this change? (Y/N) ");
            if (Console.ReadLine().ToLower() != "y")
            {
                return 0;
            }

            HttpClient client = new HttpClient();

            for (int index = 1; ; index++)
            {
                if (opts.Verbose)
                {
                    Console.WriteLine($"Making request to https://api.jikan.moe/v3/user/{opts.Username}/animelist/all/{index}");
                }
                MALResponse response = JsonConvert.DeserializeObject<MALResponse>(client.GetStringAsync($"https://api.jikan.moe/v3/user/{opts.Username}/animelist/all/{index}").Result);
                if (opts.Verbose)
                    Console.WriteLine($"Adding batch of {response.Animes.Count} animes");
                foreach (MALAnime anime in response.Animes)
                {
                    if (opts.Verbose)
                    {
                        Console.WriteLine($"Adding anime: {anime.Title}");
                    }

                    if (manager.Exists(anime.Title))
                    {
                        if (opts.Force)
                        {
                            if (opts.Verbose)
                                Console.WriteLine($"Overwriting {anime.Title}");

                            manager.Remove(anime.Title);
                            manager.Add(anime.ToAnime());
                            continue;
                        }

                        Console.Write($"Anime by name {anime.Title} already exists, would you like to overwrite it? (Y/N) ");
                        if (Console.ReadLine().ToLower() == "y")
                        {
                            if (opts.Verbose)
                                Console.WriteLine($"Overwriting {anime.Title}");
                            manager.Remove(anime.Title);
                            manager.Add(anime.ToAnime());
                        }
                        else if (opts.Verbose)
                        {
                            Console.WriteLine($"Skipping {anime.Title}");
                        }
                    }
                    else
                    {
                        manager.Add(anime.ToAnime());
                    }
                }
                if (response.Animes.Count < 300)
                {
                    if (opts.Verbose)
                        Console.WriteLine("Reached final page, breaking.");
                    break;
                }
                // If the response wasn't cached we need to wait so a rate limit isn't hit
                if (!response.RequestCached)
                {
                    if (opts.Verbose)
                        Console.WriteLine("Waiting 30 seconds");
                    Thread.Sleep(500);
                }
            }

            manager.Save();
            return 0;
        }
    }
}
