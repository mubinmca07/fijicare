using System;
using Fijicare.Model;

namespace Fijicare.Model
{
    public class CWeek : ModelBase
    {
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { SetProperty<string>(ref _Title, value); }
        }

        private bool _IsSunSelected;
        private bool _IsMonSelected;
        private bool _IsTueSelected;
        private bool _IsWedSelected;
        private bool _IsThuSelected;
        private bool _IsFriSelected;
        private bool _IsSatSelected;

        public bool IsSunSelected { get { return _IsSunSelected; } set { SetProperty<bool>(ref _IsSunSelected, value); } }
        public bool IsMonSelected { get { return _IsMonSelected; } set { SetProperty<bool>(ref _IsMonSelected, value); } }
        public bool IsTueSelected { get { return _IsTueSelected; } set { SetProperty<bool>(ref _IsTueSelected, value); } }
        public bool IsWedSelected { get { return _IsWedSelected; } set { SetProperty<bool>(ref _IsWedSelected, value); } }
        public bool IsThuSelected { get { return _IsThuSelected; } set { SetProperty<bool>(ref _IsThuSelected, value); } }
        public bool IsFriSelected { get { return _IsFriSelected; } set { SetProperty<bool>(ref _IsFriSelected, value); } }
        public bool IsSatSelected { get { return _IsSatSelected; } set { SetProperty<bool>(ref _IsSatSelected, value); } }

        private DateTime _Sun;
        private DateTime _Mon;
        private DateTime _Tue;
        private DateTime _Wed;
        private DateTime _Thu;
        private DateTime _Fri;
        private DateTime _Sat;
        public DateTime Sun { get { return _Sun; } set { SetProperty<DateTime>(ref _Sun, value); } }

        public DateTime Mon { get { return _Mon; } set { SetProperty<DateTime>(ref _Mon, value); } }
        public DateTime Tue { get { return _Tue; } set { SetProperty<DateTime>(ref _Tue, value); } }
        public DateTime Wed { get { return _Wed; } set { SetProperty<DateTime>(ref _Wed, value); } }
        public DateTime Thu { get { return _Thu; } set { SetProperty<DateTime>(ref _Thu, value); } }
        public DateTime Fri { get { return _Fri; } set { SetProperty<DateTime>(ref _Fri, value); } }
        public DateTime Sat { get { return _Sat; } set { SetProperty<DateTime>(ref _Sat, value); } }

        // public DeligateCommand WeekDaysCommand

        int prev = 0;
        int next = 0;
        DateTime sunday;
        public CWeek()
        {
           

            sunday = Fijicare.Helper.Utility.FirstDateInWeek(DateTime.Now);
            sunday = new DateTime(sunday.Year, sunday.Month, sunday.Day, 0, 0, 1);
            InicialiseWeek(sunday);
            Title = DateTime.Now.ToString("MMMM");

        }

        public void UnSelected()
        {
            IsSunSelected = false;
            IsMonSelected = false;
            IsTueSelected = false;
            IsWedSelected = false;
            IsThuSelected = false;
            IsFriSelected = false;
            IsSatSelected = false;
        }
        public void Selected(DateTime date)
        {
            UnSelected();
            if (date.Date==Sun.Date)
            {
                IsSunSelected = true;
            }
            else if (date.Date == Mon.Date)
            {
                IsMonSelected = true;
            }
            else if (date.Date == Tue.Date)
            {
                IsTueSelected = true;
            }
            else if (date.Date == Wed.Date)
            {
                IsWedSelected = true;
            }
            else if (date.Date == Thu.Date)
            {
                IsThuSelected = true;
            }
            else if (date.Date == Fri.Date)
            {
                IsFriSelected = true;
            }
            else if (date.Date == Sat.Date)
            {
                IsSatSelected = true;
            }
            Title = date.ToString("MMMM");
        }

        public void NextWeek()
        {
            if (prev <0)
            {
                sunday = sunday.AddDays(7);
                InicialiseWeek(sunday);
                Title = sunday.ToString("MMMM");
                ++prev;
                Selected(Fijicare.App.SelectedDate);
            }
            else
            {
               // Selected(DateTime.Now.Date);
            }
            
        }

        public  void PrevWeek()
        {
            --prev;
            sunday = sunday.AddDays(-7);
            Title = sunday.ToString("MMMM");
            InicialiseWeek(sunday);
            UnSelected();
            Selected(Fijicare.App.SelectedDate);

        }

       

        void InicialiseWeek(DateTime sunday)
        {
            Sun   = sunday;
            Mon = sunday.AddDays(1);
            Tue = sunday.AddDays(2);
            Wed = sunday.AddDays(3);
            Thu = sunday.AddDays(4);
            Fri = sunday.AddDays(5);
            Sat = sunday.AddDays(6);
        }
    }

}
