using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;

namespace DatabaseSkeleton.Dialogs.Simple
{
    [Flags]
    public enum SimpleDialogMode
    {
        Yes = 1,
        No = 2,
        Ok = 4,
        Cancel = 8,

        YesNo = Yes | No,
        YesNoCancel = Yes | No | Cancel,
        OkCancel = Ok | Cancel
    }

    /// <summary>
    /// Simple message box view model
    /// </summary>
    public class SimpleDialogViewModel : Screen
    {
        public SimpleDialogViewModel(string Title, string Message, SimpleDialogMode Mode)
        {
            this.mode = Mode;
            DisplayName = Title;
            this.Message = Message;
        }

        /// <summary>
        /// Dialog message
        /// </summary>
        public string Message { get; private set; }

        SimpleDialogMode mode;

        public Visibility YesVisible => (mode & SimpleDialogMode.Yes) != 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility NoVisible => (mode & SimpleDialogMode.No) != 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility OkVisible => (mode & SimpleDialogMode.Ok) != 0 ? Visibility.Visible : Visibility.Collapsed;
        public Visibility CancelVisible => (mode & SimpleDialogMode.Cancel) != 0 ? Visibility.Visible : Visibility.Collapsed;


        /// <summary>
        /// Close the dialog with a true dialog result
        /// </summary>
        public void Yes()
        {
            TryClose(true);
        }

        /// Close the dialog with a false dialog result
        /// </summary>
        public void No()
        {
            TryClose(false);
        }

        /// <summary>
        /// Close the dialog with a true dialog result
        /// </summary>
        public void Ok()
        {
            TryClose(true);
        }

        /// <summary>
        /// Close the dialog with a null dialog result
        /// </summary>
        public void Cancel()
        {
            TryClose(null);
        }
    }
}
