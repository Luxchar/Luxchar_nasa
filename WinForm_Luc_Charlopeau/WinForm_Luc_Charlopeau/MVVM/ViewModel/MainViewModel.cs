using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForm_Luc_Charlopeau.Core;

namespace WinForm_Luc_Charlopeau.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public DiscoveryViewModel DiscoveryVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; } // return the current view
            set { // set the current view to the value passed in the parameter
                _currentView = value; 
                OnPropertyChanged(); 
            }
        }
        public MainViewModel()
        {
            // assign the view models to the properties of the main view model 
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();
            // set the current view to the home view
            CurrentView = HomeVM;

            // assign the commands to the properties of the main view model
            HomeViewCommand = new RelayCommand(o => CurrentView = HomeVM);
            DiscoveryViewCommand = new RelayCommand(o => CurrentView = DiscoveryVM);
        }
    }
}
