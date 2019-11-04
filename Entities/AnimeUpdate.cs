using CommandLine;
using System;

namespace MPAL.Entities
{
    [Verb("update", HelpText = "Update an existing anime.")]
    public class AnimeUpdate
    {
        [Value(0, MetaName = "name", Required = true, HelpText = "Name of the anime to update.")]
        public string Name { get; set; }

        [Option('r', "rating", HelpText = "Your rating score for the anime.")]
        public float? Rating { get; set; }

        [Option('p', "progress", HelpText = "Your current episode in the anime.")]
        public int? Progress { get; set; }

        [Option('f', "finished", HelpText = "Whether or not you've finished the anime.", Default = false)]
        public bool? Finished { get; set; }

        [Option('t', "finish-time", HelpText = "The date and time of completion.")]
        public DateTime? FinishTime { get; set; }

        public void Merge(Anime anime)
        {
            anime.Rating = Rating.HasValue ? Rating.Value : anime.Rating;
            anime.Progress = Progress.HasValue ? Progress.Value : anime.Progress;
            anime.Finished = Finished.HasValue ? Finished.Value : anime.Finished;
            anime.FinishTime = FinishTime.HasValue ? FinishTime : anime.FinishTime;
        }
    }
}
