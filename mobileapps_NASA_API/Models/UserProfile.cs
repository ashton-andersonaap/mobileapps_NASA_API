using System;
using System.Collections.Generic;
using System.Text;

namespace mobileapps_NASA_API.Models
{
    public class UserProfile
    {
        public string UserName { get; set; }
        public List<SavedList> Lists { get; set; }

        public string ProfilePicture { get; set; }
    }
}
