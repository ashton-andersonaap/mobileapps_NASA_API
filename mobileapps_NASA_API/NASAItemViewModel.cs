using System;
using System.Collections.Generic;
using System.Text;

namespace mobileapps_NASA_API
{
    public class NASAItemViewModel
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime date { get; set; }

        public string FormattedDate => date.ToString("dd MM yy");

        public string TitleWithDate => $"{Title} - {FormattedDate}";
    }


}
