/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TibiaBotMvvm"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace TibiaBotMvvm.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<ManaSitterViewModel>();
            SimpleIoc.Default.Register<HealingViewModel>();
            SimpleIoc.Default.Register<AlarmViewModel>();
        }


        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        public ManaSitterViewModel ManaSitterView
        {
            get { return ServiceLocator.Current.GetInstance<ManaSitterViewModel>(); }
        }

        public HealingViewModel HealingView
        {
            get { return ServiceLocator.Current.GetInstance<HealingViewModel>(); }
        }

        public AlarmViewModel AlarmView
        {
            get { return ServiceLocator.Current.GetInstance<AlarmViewModel>(); }
        }

        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}