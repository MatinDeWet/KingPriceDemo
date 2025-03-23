namespace KingPriceDemo.WebClient.Models
{
    public class GroupListModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int UserCount { get; set; }

        public int GroupRights { get; set; }
        public string GroupRightsText { get; set; } = null!;
    }
}
