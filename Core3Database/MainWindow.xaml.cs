using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace Core3Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        internal static MainWindow main;
        public MainWindow()
        {
            InitializeComponent();
            main = this;
        }

        public void ChangeContent(UserControl userControl)
        {
            content.Content = userControl;
        }
       
        public void showNotification(bool isError, string Message)
        {
            if(isError)
                Growl.Error(Message);
            else
                Growl.Info(Message);
        }
    }
}
