using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using CrowdHacakthon.Annotations;
using CrowdHacakthon.Models;

namespace CrowdHacakthon.ViewModels
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        private int _notifications;
        public bool Loaded { get; set; }

        public int Notifications
        {
            get { return _notifications; }
            set
            {
                _notifications = value;
                OnPropertyChanged(nameof(Notifications));


            }
        }

        public NotificationViewModel()
        {
            Loaded = false;
        }
        public async Task Init()
        {
            Notifications =
                (await
                    (Application.Current as App).MobileService.GetTable<Request>()
                        .Where(
                            x =>
                                x.BusinessId == (Application.Current as App).BusinessId && x.Accepted == false &&
                                x.Completed == false)
                        .ToListAsync()).Count;
            (Application.Current as App)._MessageReceived += MainPageViewModel__MessageReceived;

            Loaded = true;

        }
        private async void MainPageViewModel__MessageReceived(Request request)
        {
            //Sound, animation, push whatever.

            var template = Windows.UI.Notifications.ToastTemplateType.ToastText02;
            var toastXml = ToastNotificationManager.GetTemplateContent(template);

            XmlNodeList toastTextAttributes = toastXml.GetElementsByTagName("text");
            toastTextAttributes[0].InnerText = "Μόλις λάβατε μια χορηγία!";
            toastTextAttributes[1].InnerText = "Ο χρήστης George Georgiou μόλις σας έκανε μια χορηγία των 10" + " ευρώ!";
            ToastNotificationManager.CreateToastNotifier("App").Show(new ToastNotification(toastXml));

            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,async () =>
            {
                Notifications++;

                await (Application.Current as App).MPVM.UpdateVM(new Payment { Date = DateTime.Now,Amount = 10, BusinessId = request.BusinessId, Id = Guid.NewGuid().ToString().Replace("-", ""), Type = 2 });
            });
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

