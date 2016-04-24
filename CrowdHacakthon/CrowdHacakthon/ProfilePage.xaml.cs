using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CrowdHacakthon
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            this.InitializeComponent();
            if (!DesignMode.DesignModeEnabled)
            {



                this.SizeChanged += MainPage_SizeChanged;

                Pane.RegisterPropertyChangedCallback(SplitView.IsPaneOpenProperty, (sender, dp) =>
                {
                    if (ActualWidth < 748)
                    {
                        username2.Visibility = !Pane.IsPaneOpen ? Visibility.Collapsed : Visibility.Visible;
                    }
                });
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pane.IsPaneOpen = !Pane.IsPaneOpen;

        }
        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth < 748)
            {
                username2.Visibility = !Pane.IsPaneOpen ? Visibility.Collapsed : Visibility.Visible;
            }

        }
        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        private void UIElement_OnTapped1(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapPage));
        }
        private void UIElement_OnTapped2(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfilePage));
        }
        private void UIElement_OnTapped3(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfilePage));
        }
        private void UIElement_OnTapped4(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfilePage));
        }
    }
}
