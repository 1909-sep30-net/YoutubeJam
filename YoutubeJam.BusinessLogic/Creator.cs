using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeJam.BusinessLogic
{
    /// <summary>
    /// This is a class for our end users i.e the youtube content creators
    /// </summary>
    public class Creator
    {
        public long PhoneNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
