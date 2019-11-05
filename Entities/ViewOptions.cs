using CommandLine;

namespace MPAL.Entities
{
    public enum Field
    {
        Name,
        Rating,
        Progress,
        Finished,
        FinishTime,
    }

    [Verb("view", HelpText = "View entries in a table format.")]
    public class ViewOptions
    {
        [Option('o', "order-by", HelpText = "Field to order by.")]
        public Field? OrderBy { get; set; }

        [Option('d', "desc", HelpText = "Use together with order-by to order by descending.")]
        public bool Descending { get; set; }

        [Option('l', "limit", HelpText = "Maximum amount of entries to show.")]
        public int? Limit { get; set; }
    }
}
