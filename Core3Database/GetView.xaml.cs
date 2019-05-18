using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Core3Database
{
    /// <summary>
    /// Interaction logic for GetView.xaml
    /// </summary>
    public partial class GetView : UserControl
    {
        List<tbl_Users> _initialData;
        public GetView()
        {
            InitializeComponent();
            callGetData();
            

        }

        public async void callGetData()
        {
            var data = await GetData();
            dgv.ItemsSource = data;
        }

        private void SearchBar_SearchStarted(object sender, HandyControl.Data.FunctionEventArgs<string> e)
        {
            using (var db = new mydbContext())
            {
                dgv.ItemsSource = db.tbl_Users.Where(x => x.Name.Contains(txtSearch.Text) || x.LName.Contains(txtSearch.Text)).Select(x=>x).ToList();
            }
        }

        public async Task<List<tbl_Users>> GetData()
        {
            var db = new mydbContext();
            var data = db.tbl_Users.ToListAsync();
            return await data;
        }

        public void loadData()
        {
            using (var db = new mydbContext())
            {
                var data  = db.tbl_Users.ToList();
                _initialData = data;
                dgv.ItemsSource = data;

            }
        }
    }
}
