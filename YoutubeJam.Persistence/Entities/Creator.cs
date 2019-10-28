using System.ComponentModel.DataAnnotations;

namespace YoutubeJam.Persistence.Entities
{
    /// <summary>
    /// Entity that will become table in db
    /// </summary>
    public class Creator
    {
        [Key]
        public long PhoneNumber { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}