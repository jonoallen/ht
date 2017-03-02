using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Data
{
    public class Action
    {
        public int Id { get; }
        public bool IsVisit { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
    }
}
