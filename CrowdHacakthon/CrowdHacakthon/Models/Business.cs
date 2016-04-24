using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CrowdHacakthon.Annotations;

namespace CrowdHacakthon.Models
{
    public class Business : INotifyPropertyChanged
    {
        private string _name;
        private double _loan;
        private double _paid;
        private double _donation;
        private double _roundUp;
        private string _type;
        private string _profileImage;
        private string _backImage;
        private string _location;
        private string _details;
        private int _payday;
        private int _employees;
        private string _locationDetails;
        public string Id { get; set; }
        public byte[] Version { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = value;OnPropertyChanged(nameof(Name)); }
        }

        public double Loan
        {
            get { return _loan; }
            set { _loan = value;OnPropertyChanged(nameof(Loan)); }
        }

        public double Paid
        {
            get { return _paid; }
            set { _paid = value;OnPropertyChanged(nameof(Paid)); }
        }

        public double Donation
        {
            get { return _donation; }
            set { _donation = value;OnPropertyChanged(nameof(Donation)); }
        }

        public double RoundUp
        {
            get { return _roundUp; }
            set { _roundUp = value;OnPropertyChanged(nameof(RoundUp)); }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value;OnPropertyChanged(nameof(Type)); }
        }

        public string ProfileImage
        {
            get { return _profileImage; }
            set { _profileImage = value;OnPropertyChanged(nameof(ProfileImage)); }
        }

        public string BackImage
        {
            get { return _backImage; }
            set { _backImage = value;OnPropertyChanged(nameof(BackImage)); }
        }

        public string Location
        {
            get { return _location; }
            set { _location = value;OnPropertyChanged(nameof(Location)); }
        }

        public string Details
        {
            get { return _details; }
            set { _details = value;OnPropertyChanged(nameof(Details)); }
        }

        public int Employees
        {
            get { return _employees; }
            set { _employees = value;OnPropertyChanged(nameof(Employees)); }
        }

        public string LocationDetails
        {
            get { return _locationDetails; }
            set { _locationDetails = value;OnPropertyChanged(nameof(LocationDetails)); }
        }

        public int Payday
        {
            get
            {
                return _payday;
            }

            set
            {
                _payday = value; OnPropertyChanged(nameof(Payday));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}