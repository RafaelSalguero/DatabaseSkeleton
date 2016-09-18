using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using DatabaseSkeleton.Dialogs.Simple;
using Persistence;
using Persistence.Models;

namespace DatabaseSkeleton.Main.Features.Lists.Customers
{
    class CustomerListViewModel : Screen
    {
        public CustomerListViewModel(Func<Db> Db, IWindowManager windowManager)
        {
            this.windowManager = windowManager;
            this.DisplayName = "Customers list";
            this.Db = Db;
        }
        readonly IWindowManager windowManager;
        readonly Func<Db> Db;


        public async Task RefreshData()
        {
            using (var C = Db())
            {
                this.Items = await C.Customer.ToListAsync();
            }
        }


        private List<Customer> items;
        public List<Customer> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                NotifyOfPropertyChange();
            }
        }

        private Customer selectedItem;
        public Customer SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanRemove));
            }
        }

        public void Remove()
        {
            //We use the window manager to show windows

            //This is a ViewModel-First aproach, so we only need  the view model instance and the view is resolved 
            if (windowManager.ShowDialog(new SimpleDialogViewModel("Delete item", "Delete this item?", SimpleDialogMode.YesNo)) == true)
            {

            }
        }
        public bool CanRemove => SelectedItem != null;
    }
}
