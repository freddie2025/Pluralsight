using PeopleViewer.Common;
using PersonDataReader.Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PeopleViewer.Presentation
{
    public class PeopleViewModel : INotifyPropertyChanged
    {
        protected ServiceReader DataReader;

        private IEnumerable<Person> _people;

        public IEnumerable<Person> People
        {
            get { return _people; }
            set
            {
                if (_people == value)
                    return;
                _people = value;
                RaisePropertyChanged();
            }
        }

        public PeopleViewModel()
        {
            DataReader = new ServiceReader();
        }

        public void RefreshPeople()
        {
            People = DataReader.GetPeople();
        }

        public void ClearPeople()
        {
            People = new List<Person>();
        }

        public string DataReaderType
        {
            get { return DataReader.GetType().ToString(); }
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
