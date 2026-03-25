using System;
using System.Windows;
using System.Windows.Input;

namespace sample
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void panel_MouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            try
            {
                var response = ApiClient.Post<LoginResponse>("auth.php", new
                {
                    username = username,
                    password = password
                });

                if (response == null || !response.success)
                {
                    MessageBox.Show("Invalid username or password.");
                    return;
                }

                if (!response.is_activated)
                {
                    MessageBox.Show("Account not activated. Please enter your CDK.");
                    var activationWindow = new ActivationWindow(new User
                    {
                        Username = response.username,
                        IsAdmin = response.is_admin,
                        IsActivated = false
                    });
                    this.Close();
                    activationWindow.ShowDialog();
                }
                else
                {
                    var executor = new Executor();
                    executor.Show();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please find the admin to buy the account and CDK. Thank you.");
        }
    }
}
