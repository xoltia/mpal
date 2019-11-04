using CommandLine;

namespace MPAL.Entities
{
    [Verb("finish", HelpText = "Remove an anime.")]
    public class FinishOptions
    {
        [Value(0, MetaName = "name", Required = true, HelpText = "Name of the anime to .")]
        public string Name { get; set; }
    }
}
