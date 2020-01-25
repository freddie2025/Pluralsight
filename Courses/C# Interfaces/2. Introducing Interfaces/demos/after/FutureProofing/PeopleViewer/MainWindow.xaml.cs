using Common;
using People.Library;
using System.Collections.Generic;
using System.Windows;

namespace PeopleViewer
{
    public partial class MainWindow : Window
    {
        PersonRepository repository = new PersonRepository();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConcreteFetchButton_Click(object sender, RoutedEventArgs e)
        {
            List<Person> people = repository.GetPeople();
            PersonListBox.ItemsSource = people;
        }

        private void AbstractFetchButton_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<Person> people = repository.GetPeople();
            PersonListBox.ItemsSource = people;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();
        }

        private void ClearListBox()
        {
            PersonListBox.ItemsSource = null;
        }
    }
}
