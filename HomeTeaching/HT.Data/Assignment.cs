using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Data
{
    public class Assignment
    {
        public int Id { get; }
        public Person Companion1 { get; set; }
        public Person Companion2 { get; set; }
        public Household Household { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public List<Action> Actions { get; set; }

        public Assignment()
        {
            Actions = new List<Action>();
        }
    }
}
