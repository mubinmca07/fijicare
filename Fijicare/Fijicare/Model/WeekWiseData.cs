using System;
using System.Threading.Tasks;
using Fijicare.Model;
using Fijicare.Helper;
using Microcharts;

namespace Fijicare.Model
{
    public class WeekWiseStepData : ModelBase
    {

        
       
        private int _SunSelectDay;
        private int _MonSelectDay;
        private int _TueSelectDay;
        private int _WedSelectDay;
        private int _ThuSelectDay;
        private int _FriSelectDay;
        private int _SatSelectDay;

        public int SunSelectDay { get { return _SunSelectDay; } set { SetProperty<int>(ref _SunSelectDay, value); } }
        public int MonSelectDay { get { return _MonSelectDay; } set { SetProperty<int>(ref _MonSelectDay, value); } }
        public int TueSelectDay { get { return _TueSelectDay; } set { SetProperty<int>(ref _TueSelectDay, value); } }
        public int WedSelectDay { get { return _WedSelectDay; } set { SetProperty<int>(ref _WedSelectDay, value); } }
        public int ThuSelectDay { get { return _ThuSelectDay; } set { SetProperty<int>(ref _ThuSelectDay, value); } }
        public int FriSelectDay { get { return _FriSelectDay; } set { SetProperty<int>(ref _FriSelectDay, value); } }
        public int SatSelectDay { get { return _SatSelectDay; } set { SetProperty<int>(ref _SatSelectDay, value); } }





        private int _CurrentDay;
        private int _Sun;
        private int _Mon;
        private int _Tue;
        private int _Wed;
        private int _Thu;
        private int _Fri;
        private int _Sat;
        public int Sun { get { return _Sun; } set { SetProperty<int>(ref _Sun, value); } }
        public int Mon { get { return _Mon; } set { SetProperty<int>(ref _Mon, value); } }
        public int Tue { get { return _Tue; } set { SetProperty<int>(ref _Tue, value); } }
        public int Wed { get { return _Wed; } set { SetProperty<int>(ref _Wed, value); } }
        public int Thu { get { return _Thu; } set { SetProperty<int>(ref _Thu, value); } }
        public int Fri { get { return _Fri; } set { SetProperty<int>(ref _Fri, value); } }
        public int Sat { get { return _Sat; } set { SetProperty<int>(ref _Sat, value); } }

        private int _SunHeart;
        private int _MonHeart;
        private int _TueHeart;
        private int _WedHeart;
        private int _ThuHeart;
        private int _FriHeart;
        private int _SatHeart;

        public int SunHeart { get { return _SunHeart; } set { SetProperty<int>(ref _SunHeart, value); } }
        public int MonHeart { get { return _MonHeart; } set { SetProperty<int>(ref _MonHeart, value); } }
        public int TueHeart { get { return _TueHeart; } set { SetProperty<int>(ref _TueHeart, value); } }
        public int WedHeart { get { return _WedHeart; } set { SetProperty<int>(ref _WedHeart, value); } }
        public int ThuHeart { get { return _ThuHeart; } set { SetProperty<int>(ref _ThuHeart, value); } }
        public int FriHeart { get { return _FriHeart; } set { SetProperty<int>(ref _FriHeart, value); } }
        public int SatHeart{ get { return _SatHeart; } set { SetProperty<int>(ref _SatHeart, value); } }



        private decimal _SunDistance;
        private decimal _MonDistance;
        private decimal _TueDistance;
        private decimal _WedDistance;
        private decimal _ThuDistance;
        private decimal _FriDistance;
        private decimal _SatDistance;
        public decimal SunDistance { get { return _SunDistance; } set { SetProperty<decimal>(ref _SunDistance, value); } }
        public decimal MonDistance { get { return _MonDistance; } set { SetProperty<decimal>(ref _MonDistance, value); } }
        public decimal TueDistance { get { return _TueDistance; } set { SetProperty<decimal>(ref _TueDistance, value); } }
        public decimal WedDistance { get { return _WedDistance; } set { SetProperty<decimal>(ref _WedDistance, value); } }
        public decimal ThuDistance { get { return _ThuDistance; } set { SetProperty<decimal>(ref _ThuDistance, value); } }
        public decimal FriDistance { get { return _FriDistance; } set { SetProperty<decimal>(ref _FriDistance, value); } }
        public decimal SatDistance { get { return _SatDistance; } set { SetProperty<decimal>(ref _SatDistance, value); } }


        private decimal _SunCal;
        private decimal _MonCal;
        private decimal _TueCal;
        private decimal _WedCal;
        private decimal _ThuCal;
        private decimal _FriCal;
        private decimal _SatCal;
        public decimal SunCal { get { return _SunCal; } set { SetProperty<decimal>(ref _SunCal, value); } }
        public decimal MonCal { get { return _MonCal; } set { SetProperty<decimal>(ref _MonCal, value); } }
        public decimal TueCal { get { return _TueCal; } set { SetProperty<decimal>(ref _TueCal, value); } }
        public decimal WedCal { get { return _WedCal; } set { SetProperty<decimal>(ref _WedCal, value); } }
        public decimal ThuCal { get { return _ThuCal; } set { SetProperty<decimal>(ref _ThuCal, value); } }
        public decimal FriCal { get { return _FriCal; } set { SetProperty<decimal>(ref _FriCal, value); } }
        public decimal SatCal { get { return _SatCal; } set { SetProperty<decimal>(ref _SatCal, value); } }

      

        public decimal dayvalue(int day,string type)
        {
            decimal temp = 0;
            switch (type)
            {
                case "Heart":
                    switch (day)
                    {
                        case 0:
                            temp = SunHeart;
                            break;

                        case 1:
                            temp = MonHeart;
                            break;
                        case 2:
                            temp = TueHeart;
                            break;
                        case 3:
                            temp = WedHeart;
                            break;
                        case 4:
                            temp = ThuHeart;
                            break;
                        case 5:
                            temp = FriHeart;
                            break;
                        case 6:
                            temp = SatHeart;
                            break;

                    }
                    break;
                case "Distance":
                    switch (day)
                    {
                        case 0:
                            temp = SunDistance;
                            break;

                        case 1:
                            temp = MonDistance;
                            break;
                        case 2:
                            temp = TueDistance;
                            break;
                        case 3:
                            temp = WedDistance;
                            break;
                        case 4:
                            temp = ThuDistance;
                            break;
                        case 5:
                            temp = FriDistance;
                            break;
                        case 6:
                            temp = SatDistance;
                            break;

                    }
                    break;
                case "Step":
                    switch (day)
                    {
                        case 0:
                            temp = Sun;
                            break;

                        case 1:
                            temp = Mon;
                            break;
                        case 2:
                            temp = Tue;
                            break;
                        case 3:
                            temp = Wed;
                            break;
                        case 4:
                            temp = Thu;
                            break;
                        case 5:
                            temp = Fri;
                            break;
                        case 6:
                            temp = Sat;
                            break;

                    }
                    break;
                case "cal":
                    switch (day)
                    {
                        case 0:
                            temp = SunCal;
                            break;

                        case 1:
                            temp = MonCal;
                            break;
                        case 2:
                            temp = TueCal;
                            break;
                        case 3:
                            temp = WedCal;
                            break;
                        case 4:
                            temp = ThuCal;
                            break;
                        case 5:
                            temp = FriCal;
                            break;
                        case 6:
                            temp = SatCal;
                            break;

                    }
                    break;
            }
            return temp;
        }

        private RadialGaugeChart _SunRadialGaugeGraph;
        public RadialGaugeChart SunRadialGaugeGraph
        {
            get { return _SunRadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _SunRadialGaugeGraph, value); }
        }

        private RadialGaugeChart _MonRadialGaugeGraph;
        public RadialGaugeChart MonRadialGaugeGraph
        {
            get { return _MonRadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _MonRadialGaugeGraph, value); }
        }

        private RadialGaugeChart _TueRadialGaugeGraph;
        public RadialGaugeChart TueRadialGaugeGraph
        {
            get { return _TueRadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _TueRadialGaugeGraph, value); }
        }

        private RadialGaugeChart _WedRadialGaugeGraph;
        public RadialGaugeChart WedRadialGaugeGraph
        {
            get { return _WedRadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _WedRadialGaugeGraph, value); }
        }

        private RadialGaugeChart _ThuRadialGaugeGraph;
        public RadialGaugeChart ThuRadialGaugeGraph
        {
            get { return _ThuRadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _ThuRadialGaugeGraph, value); }
        }

        private RadialGaugeChart _FriRadialGaugeGraph;
        public RadialGaugeChart FriRadialGaugeGraph
        {
            get { return _FriRadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _FriRadialGaugeGraph, value); }
        }

        private RadialGaugeChart _SatRadialGaugeGraph;
        public RadialGaugeChart SatRadialGaugeGraph
        {
            get { return _SatRadialGaugeGraph; }
            set { SetProperty<RadialGaugeChart>(ref _SatRadialGaugeGraph, value); }
        }

        public int CurrentDay { get { return _CurrentDay; } set { SetProperty<int>(ref _CurrentDay, value); } }

        public async System.Threading.Tasks.Task<RadialGaugeChart> StepRadialGraph(decimal StepCount)
        {
            if (StepCount == 0) StepCount = 0;
            var spt = (float)((StepCount * 100) / (Constant.Steps));
            spt = spt > 100 ? 100 : spt;
            await Task.Delay(1);
            var entries = new[]
            {
            new ChartEntry(spt)
                 {
                     ValueLabel = spt.ToString(),
                     Color = SkiaSharp.SKColor.Parse("#296fde"),
                     ValueLabelColor= SkiaSharp.SKColor.Parse("#ffffff")
                 }
            };
            return new RadialGaugeChart()
            {
                MinValue = 0,
                MaxValue = 100,
                AnimationDuration = TimeSpan.FromSeconds(2),
                LineAreaAlpha = 40,
                StartAngle = 105,
                Entries = entries,
                BackgroundColor = SkiaSharp.SKColor.Parse("#ffffff")
            };
        }

        public async System.Threading.Tasks.Task<RadialGaugeChart> CaloresRadialGraph(decimal calories)
        {
            if (calories == 0) calories = 0.0M;
            var spt = (float)(calories * 100) / (Constant.Caloreis);
            spt = spt > 100 ? 100 : spt;
            await Task.Delay(1);
            var entries = new[]
            {

            new ChartEntry(spt)
                 {
                     ValueLabel = spt.ToString(),

                     Color = SkiaSharp.SKColor.Parse("#296fde"),
                     ValueLabelColor= SkiaSharp.SKColor.Parse("#ffffff")
                 }
            };
            return new RadialGaugeChart()
            {
                MinValue = 0,
                MaxValue = 100,
                AnimationDuration = TimeSpan.FromSeconds(2),
                LineAreaAlpha = 40,
                StartAngle = 105,
                Entries = entries,
                BackgroundColor = SkiaSharp.SKColor.Parse("#ffffff")
            };
        }

        public async System.Threading.Tasks.Task<RadialGaugeChart> HeartRadialGraph(decimal heart)
        {
            if (heart == 0) heart = 0.00M;
            var spt = (float)(heart * 100) / (Constant.HeartPoint);
            spt = spt > 100 ? 100 : spt;
            await Task.Delay(1);
            var entries = new[]
            {


            new ChartEntry(spt)
                 {
                     ValueLabel = spt.ToString(),

                     Color = SkiaSharp.SKColor.Parse("#296fde"),
                     ValueLabelColor= SkiaSharp.SKColor.Parse("#ffffff")
                 }
            };
            return new RadialGaugeChart()
            {
                MinValue = 0,
                MaxValue = 100,
                AnimationDuration = TimeSpan.FromSeconds(2),
                LineAreaAlpha = 40,
                StartAngle = 105,
                Entries = entries,
                BackgroundColor = SkiaSharp.SKColor.Parse("#ffffff")
            };

        }

        public async System.Threading.Tasks.Task<RadialGaugeChart> DistanceRadialGraph(decimal distance)
        {

            if (distance == 0) distance = 0.00M;
            // var spt = (float)(distance * 100) / (10);
            var spt = (float)(distance * 100) / (Constant.Distance);
            spt = spt > 100 ? 100 : spt;
            await Task.Delay(1);
            var entries = new[]
            {

            new ChartEntry(spt)
                 {
                     ValueLabel = spt.ToString(),
                     Color = SkiaSharp.SKColor.Parse("#296fde"),
                     ValueLabelColor= SkiaSharp.SKColor.Parse("#ffffff")
                 }
            };
            return new RadialGaugeChart()
            {
                MinValue = 0,
                MaxValue = 100,
                AnimationDuration = TimeSpan.FromSeconds(2),
                LineAreaAlpha = 40,
                StartAngle = 105,
                Entries = entries,
                BackgroundColor = SkiaSharp.SKColor.Parse("#ffffff")
            };

        }
    }
}


