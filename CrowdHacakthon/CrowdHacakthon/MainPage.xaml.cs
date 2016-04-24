using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CrowdHacakthon.Models;
using CrowdHacakthon.ViewModels;
using Syncfusion.UI.Xaml.Charts;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CrowdHacakthon
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public DispatcherTimer DTimer { get; set; }

        private SolidColorBrush SCB1 = new SolidColorBrush(Colors.BlueViolet);
        private SolidColorBrush SCB2 = new SolidColorBrush(Colors.Coral);
        private SolidColorBrush SCB3 = new SolidColorBrush(Colors.MediumTurquoise);
        private SolidColorBrush SCB4 = new SolidColorBrush(Colors.DarkSlateBlue);
        private SolidColorBrush SCBTR = new SolidColorBrush(Colors.Transparent);

        public MainPage()
        {
            this.InitializeComponent();
            topbar.DataContext = (Application.Current as App).NVM;
            Pane.DataContext = (Application.Current as App).MPVM;
            //DoughnutSeries.ItemsSource = 
            /*DoughnutSeries2.ItemsSource = CreateData2();
            RoundSeries.ItemsSource = CreateData3();
            PaymentSeries.ItemsSource = CreateData4();
            DonationSeries.ItemsSource = CreateData5();
*/
            ChartColorModel cm1 = new ChartColorModel();
            cm1.CustomBrushes.Add(SCB1);
            cm1.CustomBrushes.Add(SCB2);
            cm1.CustomBrushes.Add(SCB3);
            cm1.CustomBrushes.Add(SCBTR);
            DoughnutSeries.ColorModel = cm1;

            ChartColorModel cm2 = new ChartColorModel();
            cm2.CustomBrushes.Add(SCB4);
            cm2.CustomBrushes.Add(SCBTR);
            DoughnutSeries2.ColorModel = cm2;

            ChartColorModel cm3 = new ChartColorModel();
            cm3.CustomBrushes.Add(SCB1);
            cm3.CustomBrushes.Add(SCBTR);

            RoundSeries.ColorModel = cm3;

            ChartColorModel cm4 = new ChartColorModel();
            cm4.CustomBrushes.Add(SCB2);
            cm4.CustomBrushes.Add(SCBTR);

            PaymentSeries.ColorModel = cm4;

            ChartColorModel cm5 = new ChartColorModel();
            cm5.CustomBrushes.Add(SCB3);
            cm5.CustomBrushes.Add(SCBTR);

            DonationSeries.ColorModel = cm5;


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

        private bool forth = true;
        private void DTimer_Tick(object sender, object e)
        {

            if (forth && flipview.SelectedIndex < flipview.Items.Count)
                flipview.SelectedIndex++;
            else if (!forth && flipview.SelectedIndex > 0)
                flipview.SelectedIndex--;


            if (flipview.SelectedIndex == 0)
                forth = true;
            else if (flipview.SelectedIndex == flipview.Items.Count - 1)
                forth = false;

        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ActualWidth < 748)
            {
                username2.Visibility = !Pane.IsPaneOpen ? Visibility.Collapsed : Visibility.Visible;
            }
            
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            
            await (Application.Current as App).LoadVMs();
            DTimer = new DispatcherTimer();
            DTimer.Interval = TimeSpan.FromSeconds(5);
            DTimer.Tick += DTimer_Tick;
            DTimer.Start();

            cal.Date = (Application.Current as App).MPVM.NextPaymentDate;


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Pane.IsPaneOpen = !Pane.IsPaneOpen;

        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof (ProfilePage));
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

        private void Userimage2_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(ProfilePage));
        }

        private void UIElement_OnTappeds(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));
        }
    }
    
}
