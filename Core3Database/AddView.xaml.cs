using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
using ThumbnailSharp;

namespace Core3Database
{
    /// <summary>
    /// Interaction logic for AddView.xaml
    /// </summary>
    public partial class AddView : UserControl
    {
        public AddView()
        {
            InitializeComponent();

            imgProfile.Source = new BitmapImage(new Uri("pack://application:,,,/Core3Database;component/Resources/DevOps-Boards.png"));
            
        }

        public async Task<string> InsertToUser(string name, string lname, byte[] profile)
        {
            var db = new mydbContext();
            var data = new tbl_Users
            {
                Name = name,
                LName = lname,
                Profile = profile
            };
            db.tbl_Users.Add(data);
            await db.SaveChangesAsync();
            return "user added";
        }

        public async void CallInsert(string name, string lname, byte[] profile)
        {
            var query = await InsertToUser(name, lname, profile);
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtLName.Text))
            {
                try
                {
                    using (var db = new mydbContext())
                    {
                        //var data = new tbl_Users
                        //{
                        //    Name = txtName.Text,
                        //    LName = txtLName.Text,
                        //    Profile = createThumb(imgProfile.Source as BitmapImage)
                        //};
                        //db.tbl_Users.Add(data);
                        //db.SaveChanges();
                        CallInsert(txtName.Text, txtLName.Text, createThumb(imgProfile.Source as BitmapImage));
                        MainWindow.main.showNotification(false, "Saved to Database");
                    }
                }
                catch (Exception)
                {

                    MainWindow.main.showNotification(true, "Error");
                }

            }
            else
            {
                MainWindow.main.showNotification(true, "Fill input");
            }

        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            var imageExtension = string.Join(";", ImageCodecInfo.GetImageDecoders().Select(x => x.FilenameExtension));
            dialog.Filter = string.Format("Select|{0}| Images|*.*", imageExtension);
            if ((bool)dialog.ShowDialog())
                imgProfile.Source = new BitmapImage(new Uri(dialog.FileName));
        }

        public byte[] getImageAsByte(BitmapImage image)
        {
            MemoryStream mem = new MemoryStream();
            JpegBitmapEncoder encode = new JpegBitmapEncoder();
            encode.Frames.Add(BitmapFrame.Create(image));
            encode.Save(mem);
            return mem.ToArray();
        }

        public byte[] createThumb(BitmapImage image)
        {
            MemoryStream mem = new MemoryStream();
            JpegBitmapEncoder encode = new JpegBitmapEncoder();
            encode.Frames.Add(BitmapFrame.Create(image));
            encode.Save(mem);

            byte[] resultBytes = new ThumbnailCreator().CreateThumbnailBytes(
        
                thumbnailSize: 50,
        imageBytes: mem.ToArray(),
        imageFormat: Format.Png
);
            return resultBytes;
        }

    }
}
