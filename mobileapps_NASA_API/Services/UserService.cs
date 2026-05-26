using mobileapps_NASA_API.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace mobileapps_NASA_API.Services
{
    public class UserService
    {

        private const string STORAGE_KEY = "UserProfile";

        public UserProfile CurrentUser { get; set; }


        public List<NASAItemViewModel> GetFavourites()
        {
            LoadUser();
            

            return CurrentUser?.Lists?
                .FirstOrDefault(l => l.Name == "Favourites")?
                .Items?
                .AsEnumerable()
                .Reverse()
                .ToList()
                ?? new List<NASAItemViewModel> ();
        }

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
                    Lists = new List<SavedList>()
                    {
                        new SavedList { Name = "Favourites",
                        Items = new List<NASAItemViewModel>() }
                    }
                };

                SaveUser();


                // SAFETY CHECKS
                CurrentUser.Lists ??= new List<SavedList>();

                foreach (var list in CurrentUser.Lists)
                {
                    list.Items ??= new List<NASAItemViewModel>();
                }
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

                return;
            }

            if (!list.Items.Any(x => x.Id == item.Id))
            {
                list.Items.Add(item);
                SaveUser();
            }
        }


    }
}
