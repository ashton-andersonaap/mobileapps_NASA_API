using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace mobileapps_NASA_API
{


    public class Rootobject
    {
        public Collection collection { get; set; }
    }

    public class Collection
    {
        public string version { get; set; }
        public string href { get; set; }
        public Item[] items { get; set; }
        public Metadata metadata { get; set; }
        public Link[] links { get; set; }
    }

    public class Metadata
    {
        public int total_hits { get; set; }
    }

    public class Item
    {
        public string href { get; set; }
        public Datum[] data { get; set; }
        public Link[] links { get; set; }
    }

    public class Datum
    {
        public string center { get; set; }
        public DateTime date_created { get; set; }
        public string description { get; set; }
        public string description_508 { get; set; }
        public string[] keywords { get; set; }
        public string media_type { get; set; }
        public string nasa_id { get; set; }
        public string secondary_creator { get; set; }
        public string title { get; set; }
        public string[] album { get; set; }
        public string location { get; set; }
        public string photographer { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string render { get; set; }
        public int width { get; set; }
        public int size { get; set; }
        public int height { get; set; }
        public string prompt { get; set; }
    }



}
