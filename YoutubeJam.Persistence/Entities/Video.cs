using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoutubeJam.Persistence.Entities
{
    public class Video
    {
        [Key]
        public int VID { get; set; }

        [Index(IsUnique = true)]
        public string URL { get; set; }

        public Channel VideoChannel { get; set; }
    }
}