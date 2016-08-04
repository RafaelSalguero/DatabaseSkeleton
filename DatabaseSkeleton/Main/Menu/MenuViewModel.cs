using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseSkeleton.Main.Menu
{
    /// <summary>
    /// Main menu view model
    /// </summary>
    public class MenuViewModel : Caliburn.Micro.PropertyChangedBase
    {
        public MenuViewModel()
        {

        }
        public void Hello()
        {
            MessageBox.Show("Hello");
        }
    }
}
