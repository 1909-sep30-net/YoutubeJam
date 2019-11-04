using System.ComponentModel.DataAnnotations;

namespace YoutubeJam.Persistence.Entities
{
    public class Channel
    {
        [Key]
        public int ChannelID { get; set; }

        public string ChannelName { get; set; }
        public Creator ChannelAuthor { get; set; }
    }
}