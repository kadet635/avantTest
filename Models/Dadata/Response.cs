using System.Collections.Generic;


namespace AvantTest.Models.Dadata
{
    public class Name
    {
        public string full_with_opf { get; set; }
    }

    public class Suggestion
    {
        public Data data { get; set; }
        //
    }

    public class RootObject
    {
        public List<Suggestion> suggestions { get; set; }
    }
    public class Data
    {
        public Name name { get; set; }
    }
}