using System;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using DatabaseSkeleton.Main;
using DatabaseSkeleton.Main.Menu;
using Persistence;
using SimpleInjector;

namespace DatabaseSkeleton
{
    public interface IShell
    {
    }

    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell
    {
        public ShellViewModel(MenuViewModel Menu, Func<Db> Db)
        {
            this.Menu = Menu;

            //Init database:
            Task.Run(() =>
            {

                using (var C = Db())
                {
                    C.Customer.Count();
                }
            });
        }

        public string Text { get; set; } = "Hola a todos";
        public MenuViewModel Menu { get; private set; }
    }
}