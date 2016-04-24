using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Chat;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CrowdHacakthon.Models;
using CrowdHacakthon.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.WindowsAzure.MobileServices;

namespace CrowdHacakthon
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        public MainPageViewModel MPVM { get; set; }
        public NotificationViewModel NVM { get; set; }
        public delegate void MessageReceived(Request request);

        public event MessageReceived _MessageReceived;
        public MobileServiceClient MobileService { get; set; }
        public HubConnection conn { get; set; }
        public IHubProxy proxy { get; set; }
        public bool IsUser { get; set; } = false;
        public string BusinessId { get; set; } = "b0001";
        public string UserId { get; set; } = "u0001";
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            MPVM = new MainPageViewModel();
            NVM = new NotificationViewModel();
            MobileService = new MobileServiceClient("http://localhost:58452/");
            SignalR();
        }

        public async Task LoadVMs()
        {
            await MobileService.GetTable<Business>().InsertAsync(new Business());
            if (!MPVM.Loaded)
            {
                await MPVM.Init();
            }
            if (!NVM.Loaded)
            {

                await NVM.Init();
            }

        }
        public void SignalR()
        {
            conn = new HubConnection("http://localhost:58452/");
            conn.ConnectionId = !IsUser ? "business" : "user";
            proxy = conn.CreateHubProxy("NotificationHub");
            
            conn.Start();

            if (!IsUser)
                proxy.On<Request>("ReceiveBusiness", OnBusinessMessage);
            else
                proxy.On<Request>("ReceiveUser", OnUserMessage);


        }
        public void UserSendRequest(string businessId, string itemId, int quantity, string userId)
        {
            proxy.Invoke("SendBusiness",businessId, itemId, quantity, userId);
        }
        public void BusinessAnswerRequest(string reqId,bool accepted)
        {
            proxy.Invoke("SendUser", reqId, accepted);
        }
        private void OnBusinessMessage(Request req)
        {
            //REQUEST RECEIVED - BUS

            OnMessageReceived(req);
        }
        private void OnUserMessage(Request req)
        {
            //RESPONSE RECEIVED - USER
            OnMessageReceived(req);

        }
        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
//                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (IsUser)
                {
                    rootFrame.Navigate(typeof(MainUserPage), e.Arguments);

                }
                else
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);

                }
            }
            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        private void OnMessageReceived(Request request)
        {
            _MessageReceived?.Invoke(request);
        }
    }
}
