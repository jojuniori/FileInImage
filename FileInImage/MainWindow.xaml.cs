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
using System.Net;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Interop;

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
            // 统计启动次数
            string strURL = "https://www.moem.cc/software/FileInImage/launch";
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST"; // Post请求方式
            request.ContentType = "application/x-www-form-urlencoded"; // 内容类型
            System.Net.HttpWebResponse response;
            // 获得响应流
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
            // string responseText = myreader.ReadToEnd();
            myreader.Close();
            // MessageBox.Show(responseText);
        }

        // 实现整个窗口的拖动
        private void DragWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
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
        // 文件大小转换
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
        // 拖动文件事件
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
                    
                    string ext = System.IO.Path.GetExtension(file.ToString());
                    if (ext == ".zip" || ext == ".rar" || ext == ".7z" || ext == ".tar" || ext == ".gz")
                    {
                        // File size
                        fileSize.Content = fileSize2Text(file.Length);
                        // File Image
                        DropFileBG.Source = new BitmapImage(new Uri(@"\Image\icon_fileInputted.png", UriKind.Relative));
                        // File Name
                        DropFileTitle.Content = file.Name.ToString();
                    }
                    else
                    {
                        return;
                    }

                }
                catch (Exception)
                {
                    file = null;
                }
            }
        }
        // 拖动图片事件
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
                    
                    string ext = System.IO.Path.GetExtension(img.ToString());
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".bmp" || ext == ".gif")
                    {
                        // Image size
                        imageSize.Content = fileSize2Text(img.Length);
                        // Image preview
                        Bitmap bi3 = CreateBitmapThumbNail(files[0], target.RenderSize.Height, target.RenderSize.Width);
                        target.Stretch = Stretch.Uniform;
                        target.Source = ChangeBitmapToImageSource(bi3);
                        // Image name
                        DropImageTitle.Content = file.Name.ToString();
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception)
                {
                    file = null;
                }
            }
        }
        // 预览缩略图
        private Bitmap CreateBitmapThumbNail(string fromFile, double maxWidth, double maxHeight)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(fromFile);
            float scale = (float)Math.Max(maxWidth / image.Width, maxHeight / image.Height);
            var destRect = new System.Drawing.Rectangle(0, 0, (int)(image.Width * scale), (int)(image.Height * scale));

            var destImage = new Bitmap(destRect.Width, destRect.Height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                //graphics.CompositingQuality = CompositingQuality.HighQuality;
                //graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                //graphics.SmoothingMode = SmoothingMode.HighQuality;
                //graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static ImageSource ChangeBitmapToImageSource(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            ImageSource wpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return wpfBitmap;
        }

        private FileInfo file=null;
        private FileInfo img=null;


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

                // 统计合成次数
                string strURL = "https://www.moem.cc/software/FileInImage/use";
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "POST"; // Post请求方式
                request.ContentType = "application/x-www-form-urlencoded"; // 内容类型
                System.Net.HttpWebResponse response;
                // 获得响应流
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
                // string responseText = myreader.ReadToEnd();
                myreader.Close();
                // MessageBox.Show(responseText);

                tip.Content = "提示：合成完成文件储存于压缩包源文件同目录";
            }
        }
        private void SynthesisButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SynthesisButton.Margin = new Thickness(0, 0, 0, 16);
        }
    }
}
