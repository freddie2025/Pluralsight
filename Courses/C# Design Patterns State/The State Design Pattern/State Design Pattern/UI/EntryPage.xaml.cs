using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace State_Design_Pattern.UI
{
    /// <summary>
    /// Interaction logic for EntryPage.xaml
    /// </summary>
    public partial class EntryPage : Page
    {
        public EntryPage()
        {
            InitializeComponent();
        }

        private void ValidateNumber(Object sender, TextCompositionEventArgs e)
        {
            Regex regX = new Regex("[^0-9]");
            e.Handled = regX.IsMatch(e.Text);
        }
    }
}
