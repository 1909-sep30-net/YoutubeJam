using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YoutubeJam.Persistence.Entities
{
    /// <summary>
    /// Entity that will become table in db
    /// </summary>
    public class Creator
    {
        [Key]
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
