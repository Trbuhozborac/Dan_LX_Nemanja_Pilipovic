using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.ViewModels;

namespace Zadatak_1.Views
{
    /// <summary>
    /// Interaction logic for AddNewWorker.xaml
    /// </summary>
    public partial class AddNewWorker : Window
    {
        public AddNewWorker()
        {
            InitializeComponent();
            this.DataContext = new AddNewWorkerViewModel(this);
        }

        #region Functions

        /// <summary>
        /// Validate that User input is just letters
        /// </summary>
        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z ]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Validate that User input is just letters and digits
        /// </summary>
        private void LetterAndNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z0-9 ]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Validate that User input is just numbers
        /// </summary>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #endregion        

    }
}
