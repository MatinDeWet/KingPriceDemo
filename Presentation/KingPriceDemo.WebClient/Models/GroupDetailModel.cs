using System.ComponentModel.DataAnnotations;

namespace KingPriceDemo.WebClient.Models
{
    public class GroupDetailModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [Required(AllowEmptyStrings = true)]
        public string InviteToken { get; set; } = null!;

        public int GroupRights { get; set; }
        public string GroupRightsText { get; set; } = null!;

        public List<GroupDetailUserListModel> Users { get; set; } = new List<GroupDetailUserListModel>();
    }
}
