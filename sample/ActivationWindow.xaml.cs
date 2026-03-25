using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sample
{
    /// <summary>
    /// ActivationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ActivationWindow : Window
    {
        private readonly User currentUser;
        public ActivationWindow(User user)
        {
            InitializeComponent();
            currentUser = user; // 保存传入的用户
        }



        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void Panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ActivateButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredCDK = CDKTextBox.Text.Trim();

            if (string.IsNullOrEmpty(enteredCDK))
            {
                MessageBox.Show("Please enter a CDK.");
                return;
            }

            try
            {
                var response = ApiClient.Post<dynamic>("activate.php", new
                {
                    username = currentUser.Username,
                    cdk = enteredCDK
                });

                if (response.success == true)
                {
                    MessageBox.Show("Activation successful!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Activation failed: {response.message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Activation failed:\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
