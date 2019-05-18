using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3Database
{
    public class ViewModel : ViewModelBase
    {
        public RelayCommand<FunctionEventArgs<object>> OpenViewCMD => new Lazy<RelayCommand<FunctionEventArgs<object>>>(() => new RelayCommand<FunctionEventArgs<object>>(OpenView)).Value;

        private void OpenView(FunctionEventArgs<object> obj)
        {
            var item = obj.Info as SideMenuItem;
            if(obj.Info is SideMenuItem)
            {
                switch (item.Header)
                {
                    case "Add User":
                        MainWindow.main.ChangeContent(new AddView());
                        break;
                    case "Edit User":
                        MainWindow.main.ChangeContent(new EditView());
                        break;
                    case "View Data":
                        MainWindow.main.ChangeContent(new GetView());

                        break;
                    case "Delete User":
                        MainWindow.main.ChangeContent(new EditView());

                        break;
                }
            }

        }
    }
}
