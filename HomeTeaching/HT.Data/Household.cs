using LiteDB;
using System.Collections.Generic;

namespace HT.Data
{
    public class Household
    {
        public int ID { get; set; }
        public Person HeadOfHouse { get; set; }
        public Person Spouse { get; set; }
        public string Address { get; set; }
        public List<Person> Children { get; set; }

        public Household()
        {
            Children = new List<Person>();
        }
    }
}