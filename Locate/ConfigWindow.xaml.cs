using NASLocate;
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

namespace Locate
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : Window
    {
        public ConfigWindow(ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void SSHPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.SSHPassword = SSHPassword.Password;
        }

        private void SSHReset_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = DataContext as ViewModel;
            vm.SSHReset = false;
            SSHPassword.Clear();
        }
    }
}
