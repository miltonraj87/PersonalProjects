using System.ComponentModel;
namespace CrossCutting
{
    public enum EntityStatus
    {
        Added,
        Deleted,
        Detached,
        Modified,
        Unchanged,
        Selected
    }

    public enum Country
    {
        [Description("India")]
        India,
        [Description("United States")]
        USA,
        [Description("United Kingdom")]
        UK
    }
}
