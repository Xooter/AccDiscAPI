namespace AccDiscAPI.Models
{
    public class Roll
    {
        public long id { get; set; }
        public string name { get; set; }

        public int color { get; set; }
        public string description { get; set; }
        public int flags { get; set; }
        public bool hoist { get; set; }
        public bool managed { get; set; }
        public bool mentionable { get; set; }
        public int position { get; set; }
        public long permissions { get; set; }

        public int members { get; set; }
    }
}
