using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using DatabaseSkeleton.Main.Features.Lists.Customers;
using Persistence;

namespace DatabaseSkeleton.Main.Menu
{
    /// <summary>
    /// Main menu view model
    /// </summary>
    public class MenuViewModel : Caliburn.Micro.PropertyChangedBase
    {
        public MenuViewModel(IWindowManager windowManager, Func<Db> Db)
        {
            this.Db = Db;
            this.windowManager = windowManager;


        }
        readonly IWindowManager windowManager;
        readonly Func<Db> Db;

        public void Hello()
        {
            var Customer = new CustomerListViewModel(Db, windowManager);
            windowManager.ShowWindow(Customer);
        }
    }
}
