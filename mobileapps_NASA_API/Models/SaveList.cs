using System;
using System.Collections.Generic;
using System.Text;

namespace mobileapps_NASA_API.Models
{
    public class SaveList
    {
        public string Name { get; set; }
        public List<NASAItemViewModel> Items { get; set; }
    }
}
