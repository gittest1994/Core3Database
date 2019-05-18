using HandyControl.Controls;
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
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        private int _Id;
        public EditView()
        {
            InitializeComponent();

            loadData();
        }

        public void loadData()
        {
            using (var db = new mydbContext())
            {
                dgv.ItemsSource = db.tbl_Users.ToList();
            }
        }

        private void Dgv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                stc.IsEnabled = true;
                dynamic selectedItem = dgv.SelectedItems[0];
                txtName.Text = selectedItem.Name;
                txtLName.Text = selectedItem.LName;
                _Id = selectedItem.Id;
                byte[] bytes = selectedItem.Profile as byte[];
                MemoryStream stream = new MemoryStream(bytes);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.EndInit();
                imgProfile.Source = image;
            }
            catch (Exception)
            {

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

        public async Task<string> DeleteUser(int id)
        {
            var db = new mydbContext();
            var delete = await db.tbl_Users.FindAsync(id);
            db.tbl_Users.Remove(delete);
            await db.SaveChangesAsync();
            return "deleted";
        }
        public async Task<string> UpdateUser(int id, string name, string lname, byte[] profile)
        {
            var db = new mydbContext();
            var edit = await db.tbl_Users.FindAsync(id);
            edit.NameX = name;
            edit.LName = lname;
            edit.Profile = profile;
            await db.SaveChangesAsync();
            return "edited";
        }
        public async void CallUpdate(int id, string name, string lname, byte[] profile)
        {
            var query = await UpdateUser(id,name, lname, profile);

        }
        public async void CallDelete(int id)
        {
            var query = await DeleteUser(id);

        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CallUpdate(_Id, txtName.Text, txtLName.Text, createThumb(imgProfile.Source as BitmapImage));
                MainWindow.main.showNotification(false, "Edited");
                    loadData();
                //using (var db = new mydbContext())
                //{
                //    var EditUser = db.tbl_Users.Find(_Id);
                //    EditUser.Name = txtName.Text;
                //    EditUser.LName = txtLName.Text;
                //    EditUser.Profile = createThumb(imgProfile.Source as BitmapImage);
                //    db.SaveChanges();
                //    MainWindow.main.showNotification(false, "Edited");
                //    loadData();
                //}
            }
            catch (Exception)
            {

                MainWindow.main.showNotification(true, "Error");
            }
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

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(dgv.Items.Count > 0)
            {
                var message = HandyControl.Controls.MessageBox.Ask("Are you sure for delete?", "Delte");
                if (message == MessageBoxResult.OK)
                {
                    try
                    {
                        CallDelete(_Id);
                        MainWindow.main.showNotification(false, "Deleted");
                        loadData();
                        //using (var db = new mydbContext())
                        //{
                        //    var DeleteUser = db.tbl_Users.Find(_Id);
                        //    db.tbl_Users.Remove(DeleteUser);
                        //    db.SaveChanges();
                        //    MainWindow.main.showNotification(false, "Deleted");
                        //    loadData();
                        //}
                    }
                    catch (Exception)
                    {

                        MainWindow.main.showNotification(true, "Error");
                    }
                }
            }
            
        }
    }
}
