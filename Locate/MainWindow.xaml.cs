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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
namespace NASLocate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //private NotifyIcon _notifyIcon = null;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
            //InitialTray();
        }
        //private void InitialTray()
        //{
        //    //隐藏主窗体
        //    this.Visibility = Visibility.Visible;
        //    //设置托盘的各个属性
        //    _notifyIcon = new NotifyIcon();
        //    _notifyIcon.BalloonTipText = "服务运行中...";//托盘气泡显示内容
        //    _notifyIcon.Text = "ServerApp";
        //    _notifyIcon.Visible = true;//托盘按钮是否可见
        //    _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
        //    _notifyIcon.ShowBalloonTip(2000);//托盘气泡显示时间
        //    _notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
        //    //窗体状态改变时触发
        //    this.StateChanged += MainWindow_StateChanged;
        //}

        //#region 窗口状态改变
        //private void MainWindow_StateChanged(object sender, EventArgs e)
        //{
        //    if (this.WindowState == WindowState.Minimized)
        //    {
        //        this.Visibility = Visibility.Hidden;
        //    }
        //}
        //#endregion

        //#region 托盘图标鼠标单击事件
        //private void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        if (this.Visibility == Visibility.Visible)
        //        {
        //            this.Visibility = Visibility.Hidden;
        //        }
        //        else
        //        {
        //            this.Visibility = Visibility.Visible;
        //            this.Activate();
        //            this.WindowState = WindowState.Normal;
        //        }
        //    }
        //}
        //#endregion

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
        private void SearchTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var vm = DataContext as ViewModel;
                vm.SearchText = SearchTextBox.Text;
                vm.SSHSearch();
            }
        }
    }
}
