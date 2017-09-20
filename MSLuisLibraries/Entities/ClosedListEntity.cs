using System;
using System.Collections.Generic;
using System.Text;

namespace MSLuisLibraries.Entities {
    public class ClosedListEntity {
        public string id { get; set; }
        public string name { get; set; }
        public int typeId { get; set; }
        public string readableType { get; set; }
        public Sublist[] subLists { get; set; }
    }

    public class Sublist {
        public int id { get; set; }
        public string canonicalForm { get; set; }
        public string[] list { get; set; }
    }
}
