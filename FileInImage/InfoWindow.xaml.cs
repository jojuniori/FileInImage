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
using System.Windows.Shapes;

namespace FileInImage
{
    /// <summary>
    /// Info.xaml 的交互逻辑
    /// </summary>
    public partial class Info : Window
    {
        public Info()
        {
            InitializeComponent();
        }
        // 实现整个窗口的拖动
        private void DragWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }
        // 绑定关闭按钮
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        // 超链接
        private void OpenWebGit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/jojuniori/FileInImage");
        }
        private void OpenWebMoem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://neko0.com/");
        }
    }
}
