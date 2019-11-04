using CommandLine;

namespace MPAL.Entities
{
    [Verb("remove", HelpText = "Remove an anime.")]
    public class RemoveOptions
    {
        [Value(0, MetaName = "name", Required = true, HelpText = "Name of the anime to remove.")]
        public string Name { get; set; }
    }
}
