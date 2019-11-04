using CommandLine;
using Newtonsoft.Json;
using System;

namespace MPAL.Entities
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
}
