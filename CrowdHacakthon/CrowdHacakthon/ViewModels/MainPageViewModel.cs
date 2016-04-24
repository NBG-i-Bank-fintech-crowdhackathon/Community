using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using CrowdHacakthon.Annotations;
using CrowdHacakthon.Models;
using Syncfusion.UI.Xaml.Controls;

namespace CrowdHacakthon.ViewModels
{
    public class Data
    {
        public double Value { get; set; }
    }
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private Business _currentBusiness;
        private ObservableCollection<Payment> _lastPayments;
        private int _overallPercent;
        private int _roundPercent;
        private int _payPercent;
        private int _donationPercent;
        private int _mainroundPercent;
        private int _mainpayPercent;
        private int _maindonationPercent;
        private int _remainingPercent;
        private DateTimeOffset _nextPaymentDate;
        private List<Data> _mainData;
        private List<Data> _overallData;
        private List<Data> _payData;
        private List<Data> _donationData;
        private List<Data> _roundUpData;

        public DateTimeOffset NextPaymentDate
        {
            get { return _nextPaymentDate; }
            set { _nextPaymentDate = value; OnPropertyChanged(nameof(NextPaymentDate)); }
        }

        public bool Loaded { get; set; }

        public List<Data> MainData
        {
            get { return _mainData; }
            set { _mainData = value; OnPropertyChanged(nameof(MainData)); }
        }
        public List<Data> OverallData
        {
            get { return _overallData; }
            set { _overallData = value; OnPropertyChanged(nameof(OverallData)); }
        }

        public List<Data> PayData
        {
            get { return _payData; }
            set { _payData = value;OnPropertyChanged(nameof(PayData)); }
        }

        public List<Data> DonationData
        {
            get { return _donationData; }
            set { _donationData = value;OnPropertyChanged(nameof(DonationData)); }
        }

        public List<Data> RoundUpData
        {
            get { return _roundUpData; }
            set { _roundUpData = value;OnPropertyChanged(nameof(RoundUpData)); }
        }

        public int OverallPercent
        {
            get { return _overallPercent; }
            set { _overallPercent = value; OnPropertyChanged(nameof(OverallPercent));
            }
        }

        public int MainRoundUpsPercent
        {
            get { return _mainroundPercent; }
            set
            {
                _mainroundPercent = value; OnPropertyChanged(nameof(MainRoundUpsPercent));
            }
        }

        public int MainPayPercent
        {
            get { return _mainpayPercent; }
            set { _mainpayPercent = value; OnPropertyChanged(nameof(MainPayPercent)); }
        }

        public int MainDonationPercent
        {
            get { return _maindonationPercent; }
            set { _maindonationPercent = value; OnPropertyChanged(nameof(MainDonationPercent)); }
        }

        public int RoundUpsPercent
        {
            get { return _roundPercent; }
            set
            {
                _roundPercent = value; OnPropertyChanged(nameof(RoundUpsPercent));
            }
        }

        public int PayPercent
        {
            get { return _payPercent; }
            set { _payPercent = value; OnPropertyChanged(nameof(PayPercent)); }
        }

        public int DonationPercent
        {
            get { return _donationPercent; }
            set { _donationPercent = value;OnPropertyChanged(nameof(DonationPercent)); }
        }

        public int RemainingPercent
        {
            get { return _remainingPercent; }
            set { _remainingPercent = value; OnPropertyChanged(nameof(RemainingPercent)); }
        }


        public Business CurrentBusiness
        {
            get { return _currentBusiness; }
            set
            {
                _currentBusiness = value;
                OnPropertyChanged(nameof(CurrentBusiness));
            }
        }

        public ObservableCollection<Payment> LastPayments
        {
            get { return _lastPayments; }
            set { _lastPayments = value; OnPropertyChanged(nameof(LastPayments)); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
            Loaded = false;
        }

        public async Task UpdateVM(Payment payment)
        {
            LastPayments.Insert(0,payment);
            await (Application.Current as App).MobileService.GetTable<Payment>().InsertAsync(payment);
            CurrentBusiness.Donation += payment.Amount;

            await (Application.Current as App).MobileService.GetTable<Business>().UpdateAsync(CurrentBusiness);

            OverallPercent =
    (int)
        ((CurrentBusiness.Paid + CurrentBusiness.RoundUp + CurrentBusiness.Donation) / CurrentBusiness.Loan *
         100);
            PayPercent =
                (int)
                    ((CurrentBusiness.Paid) / (CurrentBusiness.Paid + CurrentBusiness.RoundUp + CurrentBusiness.Donation) * 100);
            RoundUpsPercent = (int)
                    ((CurrentBusiness.RoundUp) / (CurrentBusiness.Paid + CurrentBusiness.RoundUp + CurrentBusiness.Donation) * 100);
            DonationPercent = (int)
                    ((CurrentBusiness.Donation) / (CurrentBusiness.Paid + CurrentBusiness.RoundUp + CurrentBusiness.Donation) * 100);

            MainPayPercent = (int)
                    (CurrentBusiness.Paid / CurrentBusiness.Loan * 100);
            MainRoundUpsPercent = (int)
                    (CurrentBusiness.RoundUp / CurrentBusiness.Loan * 100);
            MainDonationPercent = (int)
                    (CurrentBusiness.Donation / CurrentBusiness.Loan * 100);
            MainData = new List<Data>
            {
                new Data {Value = (Application.Current as App).MPVM.MainPayPercent},
                new Data {Value = (Application.Current as App).MPVM.MainRoundUpsPercent},
                new Data {Value = (Application.Current as App).MPVM.MainDonationPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.MainPayPercent - (Application.Current as App).MPVM.MainRoundUpsPercent - (Application.Current as App).MPVM.MainDonationPercent}
            };
            OverallData = new List<Data>
            {
                new Data {Value = (Application.Current as App).MPVM.OverallPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.OverallPercent},
            };
            DonationData = new List<Data>
            {
                new Data {Value = (Application.Current as App).MPVM.DonationPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.DonationPercent},
            };
            PayData = new List<Data>
            {
                                new Data {Value = (Application.Current as App).MPVM.PayPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.PayPercent},
            };
            RoundUpData = new List<Data>
            {
                                new Data {Value = (Application.Current as App).MPVM.RoundUpsPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.RoundUpsPercent},
            };
        }
        public async Task Init()
        {
            CurrentBusiness = (await (Application.Current as App).MobileService.GetTable<Business>().LookupAsync((Application.Current as App).BusinessId));
            LastPayments = new ObservableCollection<Payment>(await
                (Application.Current as App).MobileService.GetTable<Payment>()
                    .Where(x => x.BusinessId == (Application.Current as App).BusinessId).Take(10)
                    .ToEnumerableAsync());
            int month;
            if (CurrentBusiness.Payday > DateTime.Now.Day)
            {
                month = DateTime.Now.Month;
            }
            else
            {
                month = DateTime.Now.AddMonths(1).Month;
            }
            var dat = $"{CurrentBusiness.Payday}/{month}/{DateTime.Now.Year}";
            NextPaymentDate = DateTime.ParseExact(dat, "dd/M/yyyy",CultureInfo.InvariantCulture,DateTimeStyles.None);
            OverallPercent =
                (int)
                    ((CurrentBusiness.Paid + CurrentBusiness.RoundUp + CurrentBusiness.Donation)/CurrentBusiness.Loan*
                     100);
            PayPercent =
                (int)
                    ((CurrentBusiness.Paid)/(CurrentBusiness.Paid + CurrentBusiness.RoundUp + CurrentBusiness.Donation)*100);
            RoundUpsPercent = (int)
                    ((CurrentBusiness.RoundUp) / (CurrentBusiness.Paid + CurrentBusiness.RoundUp + CurrentBusiness.Donation)*100);
            DonationPercent = (int)
                    ((CurrentBusiness.Donation) / (CurrentBusiness.Paid + CurrentBusiness.RoundUp + CurrentBusiness.Donation)*100);

            MainPayPercent = (int)
                    (CurrentBusiness.Paid/ CurrentBusiness.Loan * 100);
            MainRoundUpsPercent = (int)
                    ( CurrentBusiness.RoundUp/ CurrentBusiness.Loan * 100);
            MainDonationPercent = (int)
                    ( CurrentBusiness.Donation/ CurrentBusiness.Loan * 100);
            MainData = new List<Data>
            {
                new Data {Value = (Application.Current as App).MPVM.MainPayPercent},
                new Data {Value = (Application.Current as App).MPVM.MainRoundUpsPercent},
                new Data {Value = (Application.Current as App).MPVM.MainDonationPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.MainPayPercent - (Application.Current as App).MPVM.MainRoundUpsPercent - (Application.Current as App).MPVM.MainDonationPercent}
            };
            OverallData = new List<Data>
            {
                new Data {Value = (Application.Current as App).MPVM.OverallPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.OverallPercent},
            };
            DonationData = new List<Data>
            {
                new Data {Value = (Application.Current as App).MPVM.DonationPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.DonationPercent},
            };
            PayData = new List<Data>
            {
                                new Data {Value = (Application.Current as App).MPVM.PayPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.PayPercent},
            };
            RoundUpData = new List<Data>
            {
                                new Data {Value = (Application.Current as App).MPVM.RoundUpsPercent},
                new Data {Value = 100 - (Application.Current as App).MPVM.RoundUpsPercent},
            };
            Loaded = true;
        }



        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
