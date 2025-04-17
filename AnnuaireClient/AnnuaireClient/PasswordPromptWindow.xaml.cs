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
            string enteredPassword = PasswordInput.Password;

            if (enteredPassword == "1234")
            {
                IsAuthenticated = true;

                // Fermer la fenêtre et retourner un succès
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Incorrect password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Fermer la fenêtre et retourner un echec
            this.DialogResult = false;
        }
    }
}
