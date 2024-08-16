using System.Collections.Generic;

namespace G_APIs.Models
{
    public class GoldTypesVM
    {
        public List<GoldType> GoldTypes { get; set; }
        public List<GoldCarat> GoldCarats { get; set; }
    }

    public class GoldCarat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class GoldType
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public short Staus { get; set; }
    }
}