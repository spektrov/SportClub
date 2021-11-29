using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SportClub.View;

namespace SportClub.ViewModel
{
    class TopMenuViewModel : ViewModelBase
    {
        private RelayCommand _openQueryEditWindow;

        public ICommand OpenQueryEditWindow =>
            _openQueryEditWindow ?? (_openQueryEditWindow =
            new RelayCommand(() =>
           {
               var window = new QueryEdit();
               window.Show();
           }));
    }
}
