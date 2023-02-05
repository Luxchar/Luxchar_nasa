using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WinForm_Luc_Charlopeau.MVVM.View
{
    public partial class DiscoveryView : UserControl, INotifyPropertyChanged
    {
        public DiscoveryView()
        {
            InitializeComponent();
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}