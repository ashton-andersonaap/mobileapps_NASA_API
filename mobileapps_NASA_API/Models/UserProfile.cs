using System;
using System.Collections.Generic;
using System.Text;

namespace mobileapps_NASA_API.Models
{
    public class UserProfile
    {
        public string UserName { get; set; }
        public List<SavedList> Lists { get; set; } = new();

        public SavedList Favourites =>
            Lists.FirstOrDefault(x => x.Name == "Favourites");
    }
}
