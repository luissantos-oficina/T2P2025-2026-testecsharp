namespace OficinaApp.Models
{
    public class DragonBallList
    {
        public List<Characters> Items { get; set; }


    }

    public class Characters
    {
        public int id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string? description  { get; set; }
        public string? affiliation  { get; set; }
    }
/*
    public class item
    {
        public string official { get; set; }
    }

    public class Flags
    {
        public string png { get; set; }
    }*/
}