using System;
using Newtonsoft.Json;
using CommandLine;

namespace MPAL
{
    [Verb("new", HelpText = "Add a new anime.")]
    public class Anime
    {
        [JsonProperty("name")]
        [Value(0, MetaName = "name", Required = true, HelpText = "Name of the anime.")]
        public string Name { get; set; }

        [JsonProperty("rating")]
        [Option('r', "rating", HelpText = "Your rating score for the anime.")]
        public float? Rating { get; set; }

        [JsonProperty("progress")]
        [Option('p', "progress", HelpText = "Your current episode in the anime.", Default = 0)]
        public int Progress { get; set; }

        [JsonProperty("finished")]
        [Option('f', "finished", HelpText = "Whether or not you've finished the anime.", Default = false)]
        public bool Finished { get; set; }

        [JsonProperty("finishTime")]
        [Option('t', "finish-time", HelpText = "The date and time of completion.")]
        public DateTime? FinishTime { get; set; }
    }

    [Verb("update", HelpText = "Update an existing anime.")]
    public class AnimeUpdate
    {
        [Value(0, MetaName = "name", Required = true, HelpText = "Name of the anime to update.")]
        public string Name { get; set; }

        [Option('r', "rating", HelpText = "Your rating score for the anime.")]
        public float? Rating { get; set; }

        [Option('p', "progress", HelpText = "Your current episode in the anime.", Default = 0)]
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

    [Verb("remove", HelpText = "Remove an anime.")]
    public class RemoveOptions
    {
        [Value(0, MetaName = "name", Required = true, HelpText = "Name of the anime to remove.")]
        public string Name { get; set; }
    }

    [Verb("finish", HelpText = "Remove an anime.")]
    public class FinishOptions
    {
        [Value(0, MetaName = "name", Required = true, HelpText = "Name of the anime to mark as finished.")]
        public string Name { get; set; }
    }
}
