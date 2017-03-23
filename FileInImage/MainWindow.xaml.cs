using System;
using System.Collections.Generic;
using System.IO;
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

            if (file != null && img != null)
            {
                var output = File.OpenWrite(file.DirectoryName + "\\" + file.Name + "_combined" + img.Extension);
                var f = file.OpenRead();
                var i = img.OpenRead();
                i.CopyTo(output);
                f.CopyTo(output);
                i.Close();
                f.Close();
                output.Close();
            }
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

        private void FileDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            
            if (files.Length > 0)
            {
                try
                {
                    file = new FileInfo(files[0]);
                    fileSize.Content = fileSize2Text(file.Length);
                }
                catch (Exception)
                {
                    file = null;
                }
            }
        }

        private string fileSize2Text(double size)
        {
            int level = 0;
            string[] suffixs = new string[4] { "B", "KB", "MB", "GB" };
            while (size >= 1024 && level <= 2)
            {
                level++;
                size /= 1024;
            }

            return level == 0 ? size + "B" : String.Format("{0:0.00}", size) + suffixs[level];
        }

        private void ImageDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }
            
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                try
                {
                    img = new FileInfo(files[0]);
                    imageSize.Content = fileSize2Text(img.Length);
                }
                catch (Exception)
                {
                    file = null;
                }
            }
        }

        private FileInfo file=null;
        private FileInfo img=null;
    }
}
