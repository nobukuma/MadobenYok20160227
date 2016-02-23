using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace StrawhatNet.UWP.Continuum
{
    public sealed partial class MainPage : Page
    {
        private int screenViewId = 0;

        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Current_SizeChanged;
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            switch (UIViewSettings.GetForCurrentView().UserInteractionMode)
            {
                case UserInteractionMode.Mouse:
                    //VisualStateManager.GoToState(this, "MouseLayout", true);
                    break;
                case UserInteractionMode.Touch:
                default:
                    //VisualStateManager.GoToState(this, "TouchLayout", true);
                    break;
            }
        }

        private async void Projection_Click(object sender, RoutedEventArgs e)
        {
            if (ProjectionManager.ProjectionDisplayAvailable)
            {
                int phoneViewId = ApplicationView.GetForCurrentView().Id;

                await CoreApplication.CreateNewView().Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal, () =>
                    {
                        var rootFrame = new Frame();
                        rootFrame.Navigate(typeof(ProjectionViewPage), phoneViewId);

                        Window.Current.Content = rootFrame;
                        Window.Current.Activate();

                        var screenView = ApplicationView.GetForCurrentView();
                        screenViewId = screenView.Id;

                    });

                // メイン表示（端末）がphoenViewId, プロジェクター側がscreenViewId
                await ProjectionManager.StartProjectingAsync(screenViewId, phoneViewId);
            }
            else
            {
                var messageDialog = new MessageDialog("外部ディスプレイがありません");
                await messageDialog.ShowAsync();
            }
        }

        private async void SwapProjection_Click(object sender, RoutedEventArgs e)
        {
            await ProjectionManager.SwapDisplaysForViewsAsync(screenViewId, ApplicationView.GetForCurrentView().Id);
        }
    }
}
