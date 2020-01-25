using PersonRepository.Factory;
using PersonRepository.Interface;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeopleViewer
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        private IPersonRepository repository;

        private IEnumerable<Person> people;
        public IEnumerable<Person> People
        {
            get { return people; }
            set
            {
                people = value;
                RaisePropertyChanged();
            }
        }


        public string RepositoryType
        {
            get { return repository.GetType().ToString(); }
        }

        public PeopleViewModel()
        {
            repository = RepositoryFactory.GetRepository();
        }

        public void FetchData()
        {
            People = repository.GetPeople();
        }

        public void ClearData()
        {
            People = new List<Person>();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
