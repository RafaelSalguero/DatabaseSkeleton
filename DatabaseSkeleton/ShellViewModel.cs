using System;
using System.Linq;
using DatabaseSkeleton.Main;
using DatabaseSkeleton.Main.Menu;
using Persistence;

namespace DatabaseSkeleton
{
    public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell
    {
        public ShellViewModel(MenuViewModel Menu)
        {
            this.Menu = Menu;


        }

        public string Text { get; set; } = "Hola a todos";
        public MenuViewModel Menu { get; private set; }
    }
}