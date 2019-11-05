using CommandLine;

namespace MPAL.Entities
{
    [Verb("finish", HelpText = "Mark an anime as finished and set finish time to current time.")]
    public class FinishOptions
    {
        [Value(0, MetaName = "name", Required = true, HelpText = "Name of the anime to .")]
        public string Name { get; set; }
    }
}
