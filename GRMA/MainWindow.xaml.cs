using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms; // Đảm bảo tham chiếu đúng
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MessageBox = System.Windows.MessageBox; // Giải quyết xung đột Application
using Application = System.Windows.Application; // Giải quyết xung đột Application

namespace GRMA
{
    public partial class MainWindow : Window
    {
        private NotifyIcon _notifyIcon = new NotifyIcon();

        public MainWindow()
        {
            InitializeComponent();
            CreateSystemTray();
        }

        private void CreateSystemTray()
        {
            _notifyIcon = new NotifyIcon();
            _notifyIcon.Icon = new Icon("app.ico"); // Đảm bảo bạn có file icon.ico trong dự án
            _notifyIcon.Visible = false;

            // Menu chuột phải trên biểu tượng khay hệ thống
            var contextMenu = new ContextMenuStrip(); // Sử dụng ContextMenuStrip thay cho ContextMenu
            contextMenu.Items.Add("Open", null, (s, e) => ShowWindow());
            contextMenu.Items.Add("Exit", null, (s, e) => ExitApplication());

            _notifyIcon.ContextMenuStrip = contextMenu; // Sử dụng ContextMenuStrip
            _notifyIcon.DoubleClick += (s, e) => ShowWindow();
        }

        private void HideToTray_Click(object sender, RoutedEventArgs e)
        {
            HideWindow();
        }

        private void HideWindow()
        {
            this.Hide();
            _notifyIcon.Visible = true; // Hiện biểu tượng trong khay hệ thống

            // Thêm thông báo khi chương trình được ẩn xuống khay
            _notifyIcon.BalloonTipTitle = "Application Running";
            _notifyIcon.BalloonTipText = "The program is running in the background.";
            _notifyIcon.ShowBalloonTip(3000); // Thông báo sẽ hiển thị trong 3 giây
        }

        private void ShowWindow()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            _notifyIcon.Visible = false; // Ẩn biểu tượng khay hệ thống
        }

        private void ExitApplication()
        {
            _notifyIcon.Visible = false;
            Application.Current.Shutdown(); // Thoát ứng dụng, sử dụng System.Windows.Application
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true; // Ngăn đóng cửa sổ
            HideWindow(); // Ẩn ứng dụng khi đóng
        }
    }
}
