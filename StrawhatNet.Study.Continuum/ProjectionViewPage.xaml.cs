using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StrawhatNet.UWP.Continuum
{
    public sealed partial class ProjectionViewPage : Page
    {
        private int mainViewId;

        public ProjectionViewPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.Message.Text = String.Format("UserInteractionMode={0}", UIViewSettings.GetForCurrentView().UserInteractionMode.ToString());
            //switch (UIViewSettings.GetForCurrentView().UserInteractionMode)
            //{
            //    case UserInteractionMode.Mouse:
            //        //VisualStateManager.GoToState(this, "MouseLayout", true);
            //        break;
            //    case UserInteractionMode.Touch:
            //    default:
            //        //VisualStateManager.GoToState(this, "TouchLayout", true);
            //        break;
            //}
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.mainViewId = (int)e.Parameter;
            this.Message.Text = String.Format("UserInteractionMode={0}", UIViewSettings.GetForCurrentView().UserInteractionMode.ToString());
        }

        private async void StopProjection_Click(object sender, RoutedEventArgs e)
        {
            await ProjectionManager.StopProjectingAsync(
                 ApplicationView.GetForCurrentView().Id, mainViewId);
        }
    }
}
