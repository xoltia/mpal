using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MPAL.Entities
{
    public class MALResponse
    {
        [JsonProperty("request_hash")]
        public string RequestHash { get; set; }

        [JsonProperty("request_cashed")]
        public bool RequestCached { get; set; }

        [JsonProperty("request_cache_expiry")]
        public int RequestCacheExpiry { get; set; }
        
        [JsonProperty("anime")]
        public List<MALAnime> Animes { get; set; }
    }

    public class MALAnime
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("watched_episodes")]
        public int WatchedEpisodes { get; set; }

        [JsonProperty("watch_end_date")]
        public DateTime? WatchEndDate { get; set; }

        [JsonProperty("total_episodes")]
        public int TotalEpisodes { get; set; }

        public Anime ToAnime()
        {
            return new Anime
            {
                Name = Title,
                Rating = Score,
                Progress = WatchedEpisodes,
                Finished = WatchedEpisodes >= TotalEpisodes,
                FinishTime = WatchEndDate
            };
        }
    }
}
