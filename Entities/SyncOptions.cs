using CommandLine;

namespace MPAL.Entities
{
    [Verb("sync", HelpText = "Add animes from your MyAnimeList account to your list.")]
    public class SyncOptions
    {
        [Value(0, HelpText = "MAL user name to sync from.")]
        public string Username { get; set; }

        [Option(Default = false, HelpText = "Print status messages to console.")]
        public bool Verbose { get; set; }

        [Option(Default = false, HelpText = "Force, don't ask to overwrite existing entries with synced values.")]
        public bool Force { get; set; }

        // TODO: add option for filtering by status
    }
}
