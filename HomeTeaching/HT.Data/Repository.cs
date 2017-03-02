using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT.Data
{
    public class Repository
    {
        private LiteDatabase db;
        private LiteCollection<Person> people;
        private LiteCollection<Household> households;
        private LiteCollection<Assignment> assignments;

        public Repository(string connectionString)
        {
            if (this.db != null)
                throw new Exception("DB Already Open");

            this.db = new LiteDatabase(connectionString);
            init(db);
        }

        public IEnumerable<Person> People => people.FindAll();
        public IEnumerable<Household> Households => households.FindAll();
        public IEnumerable<Assignment> Assignments => assignments.FindAll();

        public void AddPerson(Person p)
        {
            people.Insert(p);            
        }

        public void RemovePerson(Person p)
        {
            people.Delete(p.ID);
        }

        private void init(LiteDatabase db)
        {
            people = db.GetCollection<Person>("people");
            households = db.GetCollection<Household>("households");
            assignments = db.GetCollection<Assignment>("assignments");

            people.EnsureIndex(p => p.Name);
            BsonMapper.Global.Entity<Person>()
                .DbRef(p => p.Household, "households");

            BsonMapper.Global.Entity<Household>()
                .DbRef(h => h.HeadOfHouse, "people")
                .DbRef(h => h.Spouse, "people")
                .DbRef(h => h.Children, "people");

            BsonMapper.Global.Entity<Assignment>()
                .DbRef(a => a.Companion1, "people")
                .DbRef(a => a.Companion2, "people")
                .DbRef(a => a.Household, "people");
        }
    }
}
