using GalaSoft.MvvmLight;

namespace CustomConfigSectionSample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private NestedConfigCollectionViewModel _nestedConfigSampleControl;
        public NestedConfigCollectionViewModel NestedConfigSampleControl
        {
            get
            {
                if(_nestedConfigSampleControl == null)
                {
                    _nestedConfigSampleControl = new NestedConfigCollectionViewModel();
                }
                return _nestedConfigSampleControl;
            }
            set
            {
                if (value != _nestedConfigSampleControl)
                {
                    _nestedConfigSampleControl = value;
                    RaisePropertyChanged("NestedConfigSampleControl");
                }
            }
        }
        public MainViewModel()
        {
        }

    }
}