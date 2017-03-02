using LiteDB;
using System.Collections.Generic;

namespace HT.Data
{
    public class Household
    {
        public int Id { get; }
        public Person HeadOfHouse { get; set; }
        public Person Spouse { get; set; }
        public List<Person> Children { get; set; }

        public Household()
        {
            Children = new List<Person>();
        }
    }
}