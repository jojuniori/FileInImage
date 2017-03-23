using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileInImage
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

        // 实现整个窗口的拖动
        private void DragWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }
        // 合成按钮点击效果
        private void SynthesisButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SynthesisButton.Margin = new Thickness(0, 1, 0, 15);
        }
        private void SynthesisButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SynthesisButton.Margin = new Thickness(0, 0, 0, 16);
        }
        // 打开Info窗口
        private void WindowOpenInfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Info window = new Info();
            window.Show();
        }
        // 关闭窗口
        private void WindowClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // 检测主窗口关闭则关闭程序
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
