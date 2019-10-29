using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoutubeJam.Persistence.Entities
{
    /// <summary>
    /// Entity that will become table in db
    /// </summary>
    public class Creator
    {
        [Key]
        public int CID { get; set; }

        [Index(IsUnique = true)]
        public string PhoneNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}