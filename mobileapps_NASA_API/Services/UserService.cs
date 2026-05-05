using mobileapps_NASA_API.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace mobileapps_NASA_API.Services
{
    public class UserService
    {

        private const string STORAGE_KEY = "UserProfile";

        public UserProfile CurrentUser { get; set; }

        public void LoadUser()
        {

        }
    }
}
