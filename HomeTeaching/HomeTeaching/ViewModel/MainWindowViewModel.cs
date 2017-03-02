using GalaSoft.MvvmLight;
using HT.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeTeaching.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            repo = new Repository(@"c:\ht\ht.db");
            People = new ObservableCollection<Person>(repo.People);
            People.CollectionChanged += People_CollectionChanged;
            
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Person newItem in e.NewItems)
                {
                    if (newItem != null)
                        repo.AddPerson(newItem);
                }
            if (e.OldItems != null)
                foreach (Person oldItem in e.OldItems)
                {
                    if (oldItem != null)
                        repo.RemovePerson(oldItem);
                }
        }

        Repository repo;

        public ObservableCollection<Person> People { get; set; }
    }
}
