using System;
using System.IO;
using CommandLine;

namespace MPAL
{
    class Program
    {
        static AnimeListManager manager = new AnimeListManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "xoltia/mpal/data.json"));
        static int Main(string[] args)
        {
            return CommandLine.Parser.Default.ParseArguments<Anime, AnimeUpdate, RemoveOptions, FinishOptions>(args)
                .MapResult(
                    (Anime anime) => NewAnime(anime),
                    (AnimeUpdate anime) => UpdateAnime(anime),
                    (RemoveOptions opts) => RemoveAnime(opts.Name),
                    (FinishOptions opts) => FinishAnime(opts.Name),
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
    }
}
