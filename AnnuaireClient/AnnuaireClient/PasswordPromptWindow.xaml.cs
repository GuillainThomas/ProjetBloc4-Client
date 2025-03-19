using System.Windows;

namespace AnnuaireClient
{
    public partial class PasswordPromptWindow : Window
    {
        public bool IsAuthenticated { get; private set; } = false;

        public PasswordPromptWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordInput.Focus();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredPassword = PasswordInput.Password; // Get the entered password

            if (enteredPassword == "1234") // Change this to your real password
            {
                IsAuthenticated = true; // Mark as authenticated
                this.DialogResult = true; // Close the window with success
            }
            else
            {
                MessageBox.Show("Incorrect password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Close the window without success
        }
    }
}
