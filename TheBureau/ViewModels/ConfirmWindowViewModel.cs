using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;

namespace TheBureau.ViewModels
{
    public class ConfirmWindowViewModel : ViewModelBase
    {
        private ICommand _okCommand;
        private ICommand _cancelCommand;

        public ICommand OkCommand => _okCommand ??= new RelayCommand(DoOk, CanDo);
        public ICommand CancelCommand => _cancelCommand ??= new RelayCommand(DoCancel, CanDo);

        void DoOk(object win)
        {
            var win1 = win as Window;
            if (win1 != null)
            {
                win1.DialogResult = true;
                win1.Close();
            }
        }

        void DoCancel(object win)
        {
            var win1 = win as Window;
            if (win1 != null)
            {
                win1.DialogResult = false;
                win1.Close();
            }
        }
        bool CanDo(object win)
        {
            return true;
        }
    }

}