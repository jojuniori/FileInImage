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
        private void DragWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();//实现整个窗口的拖动
        }

        private void SynthesisButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SynthesisButton.Margin = new Thickness(0, 1, 0, 15);
        }

        private void SynthesisButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SynthesisButton.Margin = new Thickness(0, 0, 0, 16);
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Info window = new Info();
            window.Show();
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
