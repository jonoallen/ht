using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
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
            peopleToAdd = new Collection<Person>();
            peopleToDelete = new Collection<Person>();

            householdsToAdd = new Collection<Household>();
            householdsToDelete = new Collection<Household>();
            SaveCommand = new RelayCommand(save_Excecute);
            OpenCommand = new RelayCommand(open_Execute);
            NewHouseholdCommand = new RelayCommand(newHouseholdCommand_Execute, newHouseholdCommand_CanExecute);
        }

        public RelayCommand OpenCommand { get; private set; }

        private void open_Execute()
        {
            repo = new Repository(@"c:\ht\ht.db");
            People = new ObservableCollection<Person>(repo.People);
            People.CollectionChanged += People_CollectionChanged;
            Households = new ObservableCollection<Household>(repo.Households);
            Households.CollectionChanged += Households_CollectionChanged;

            RaisePropertyChanged(nameof(People));
            RaisePropertyChanged(nameof(Households));
        }

        private void Households_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Household newItem in e.NewItems)
                {
                    if (newItem != null)
                        householdsToAdd.Add(newItem);
                }
            if (e.OldItems != null)
                foreach (Household oldItem in e.OldItems)
                {
                    if (oldItem != null)
                        householdsToDelete.Add(oldItem);
                }
        }

        private void People_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (Person newItem in e.NewItems)
                {
                    if (newItem != null)
                        peopleToAdd.Add(newItem);
                }
            if (e.OldItems != null)
                foreach (Person oldItem in e.OldItems)
                {
                    if (oldItem != null)
                        peopleToDelete.Add(oldItem);
                }
        }

        Repository repo;

        private Collection<Person> peopleToAdd;
        private Collection<Person> peopleToDelete;

        private Collection<Household> householdsToAdd;
        private Collection<Household> householdsToDelete;

        public RelayCommand SaveCommand { get; private set; }
        private void save_Excecute()
        {
            foreach (var toSave in peopleToAdd)
            {
                repo.AddPerson(toSave);
            }

            foreach (var toDelete in peopleToDelete)
            {
                repo.RemovePerson(toDelete);
            }

            foreach (var person in People.Except(peopleToAdd))
            {
                repo.UpdatePerson(person);
            }
            peopleToAdd.Clear();
            peopleToDelete.Clear();

            foreach (var toSave in householdsToAdd)
            {
                repo.AddHousehold(toSave);
            }

            foreach(var toDelete in householdsToDelete)
            {
                repo.RemoveHousehold(toDelete);
            }

            foreach(var household in Households.Except(householdsToAdd))
            {
                repo.UpdateHousehold(household);
            }
            householdsToAdd.Clear();
            householdsToDelete.Clear();
        }

        public ObservableCollection<Person> People { get; set; }
        public ObservableCollection<Household> Households { get; set; }

        public Person HeadOfHouse { get; set; }
        public Person Spouse { get; set; }
        public RelayCommand NewHouseholdCommand { get; private set; }
        private void newHouseholdCommand_Execute()
        {
            var household = new Household();
            household.HeadOfHouse = HeadOfHouse;
            household.Spouse = Spouse;

            //repo.AddHousehold(household);
            Households.Add(household);
        }
        private bool newHouseholdCommand_CanExecute()
        {
            return HeadOfHouse != null;
        }

    }
}
