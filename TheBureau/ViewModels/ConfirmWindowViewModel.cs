using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;

namespace TheBureau.ViewModels
{
    public class ConfirmWindowViewModel : ViewModelBase

    {
    private ICommand _okCommand;
    private ICommand _cancelCommand;

    public ICommand OkCommand
    {
        get
        {
            if (_okCommand == null)
            {
                _okCommand = new DelegateCommand<Window>(DoOk, CanDo);
            }
            OnPropertyChanged("OkCommand");
            return _okCommand;
        }
    }


    public ICommand CancelCommand
    {
        get
        {
            if (_cancelCommand == null)
            {
                _cancelCommand = new DelegateCommand<Window>(DoCancel, CanDo);
            }
            OnPropertyChanged("CancelCommand");
            return _cancelCommand;
        }
    }

    void DoOk(Window win)
    {
        win.DialogResult = true;
        win.Close();
    }

    void DoCancel(Window win)
    {
        win.DialogResult = false;
        win.Close();
    }


    bool CanDo(Window win)
    {
        return true;
    }
    }

}