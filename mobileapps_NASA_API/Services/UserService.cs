using mobileapps_NASA_API.Models;

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace mobileapps_NASA_API.Services
{
    public class UserService
    {

        private const string STORAGE_KEY = "UserProfile";

        public UserProfile CurrentUser { get; set; }

        public void LoadUser()
        {
            var json = Preferences.Get(STORAGE_KEY, "");

            if (!string.IsNullOrEmpty(json))
            {
                CurrentUser = JsonSerializer.Deserialize<UserProfile>(json);

            }
            if (CurrentUser == null)
            {
                CurrentUser = new UserProfile()
                {
                    UserName = "Ashton",
                    Lists = new List<SaveList>()
                    {
                        new SaveList { Name = "Favourites"}
                    }
                };

                SaveUser();
            }
        }
        public void SaveUser()
        {
            var json = JsonSerializer.Serialize(CurrentUser);
            Preferences.Set(STORAGE_KEY, json);
        }

        public void SaveItemToList(NASAItemViewModel item, string listName)
        {
            var list = CurrentUser.Lists.FirstOrDefault(l => l.Name == listName);

            if (list == null)
            {

                if (!list.Items.Any(x => x.Id == item.Id))
                {
                    list.Items.Add(item);
                    SaveUser();
                }
            }
            return;
        }


    }
}
