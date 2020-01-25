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

        public PeopleViewModel()
        {
            repository = RepositoryFactory.GetRepository();
        }

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

        public void FetchData()
        {
            People = repository.GetPeople();
        }

        public void ClearData()
        {
            People = new List<Person>();
        }

        public string RepositoryType
        {
            get { return repository.GetType().ToString(); }
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
