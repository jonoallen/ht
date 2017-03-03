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

        public void UpdatePerson(Person p)
        {
            people.Update(p);
        }

        public void AddHousehold(Household h)
        {
            households.Insert(h);
        }

        public void RemoveHousehold(Household h)
        {
            households.Delete(h.ID);
        }

        public void UpdateHousehold(Household h)
        {
            households.Update(h);
        }

        private void init(LiteDatabase db)
        {
            people = db.GetCollection<Person>("people");

            households = db.GetCollection<Household>("households")
                .Include(x => x.HeadOfHouse)
                .Include(x => x.Spouse);

            assignments = db.GetCollection<Assignment>("assignments")
                .Include(x => x.Companion1)
                .Include(x => x.Companion2)
                .Include(x => x.Household)
                .Include(x => x.Actions);

            people.EnsureIndex(p => p.Name);
            var mapper = BsonMapper.Global;
            mapper.Entity<Person>()
                .Id(p => p.ID)
                .Index(p => p.Name, unique: true);

            mapper.Entity<Household>()
                .DbRef(h => h.HeadOfHouse, "people")
                .DbRef(h => h.Spouse, "people");

            mapper.Entity<Assignment>()
                .DbRef(a => a.Companion1, "people")
                .DbRef(a => a.Companion2, "people")
                .DbRef(a => a.Household, "people");
        }
    }
}
