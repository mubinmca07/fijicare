using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fijicare.Model;
using Fijicare.Model.Response;
using Microcharts;
using SkiaSharp;
namespace Fijicare.Helpers
{
    
    public class WeekWiseActivityCalculator
    {
        public float[] StepColletion;
        public WeekWiseActivityCalculator(float[] StepColletion)
        {
            this.StepColletion = StepColletion;
        }
       public async Task<RadialGaugeChart> ActivitySelection(WeekWiseStepData weekWiseStepData,decimal val, string str)
        {
            RadialGaugeChart radialGaugeChart = null;
            switch (str)
            {
                case "Heart":
                    radialGaugeChart = await weekWiseStepData.HeartRadialGraph(val);
                    break;
                case "Distance":
                    radialGaugeChart = await weekWiseStepData.DistanceRadialGraph(val);
                    break;
                case "Step":
                    radialGaugeChart = await weekWiseStepData.StepRadialGraph(val);
                    break;
                case "cal":
                    radialGaugeChart = await weekWiseStepData.CaloresRadialGraph(val);
                    break;
            }

            return radialGaugeChart;
        }
       public async Task<WeekWiseStepData> InitialiseWeekWiseData(int day, List<Dataset> dataset, WeekWiseStepData WeekWiseStepData,string type)
        {

            switch (day)
            {

                case 0:
                    WeekWiseStepData.Sun = await GetStepCount(dataset[0]);
                    WeekWiseStepData.SunSelectDay = day;
                    WeekWiseStepData.SunCal = await GetCaloreis(dataset[1]);
                    WeekWiseStepData.SunDistance = await GetDistance(dataset[2]);
                    WeekWiseStepData.SunHeart = await GetHeartPoint(dataset[3]);
                    StepColletion[day] = WeekWiseStepData.Sun;
                    WeekWiseStepData.SunRadialGaugeGraph
                        = await ActivitySelection(WeekWiseStepData, WeekWiseStepData.dayvalue(day,type), type);
                    break;
                case 1:
                    WeekWiseStepData.Mon = await GetStepCount(dataset[0]);
                    WeekWiseStepData.MonCal = await GetCaloreis(dataset[1]);
                    WeekWiseStepData.MonSelectDay = day;
                    WeekWiseStepData.MonDistance = await GetDistance(dataset[2]);
                    WeekWiseStepData.MonHeart = await GetHeartPoint(dataset[3]);
                    StepColletion[day] = WeekWiseStepData.Mon;
                    WeekWiseStepData.MonRadialGaugeGraph
                       = await ActivitySelection(WeekWiseStepData, WeekWiseStepData.dayvalue(day, type), type);
                    break;
                case 2:
                    WeekWiseStepData.Tue = await GetStepCount(dataset[0]);
                    WeekWiseStepData.TueSelectDay = day;
                    WeekWiseStepData.TueCal = await GetCaloreis(dataset[1]);
                    WeekWiseStepData.TueDistance = await GetDistance(dataset[2]);
                    WeekWiseStepData.TueHeart = await GetHeartPoint(dataset[3]);
                    StepColletion[day] = WeekWiseStepData.Tue;
                    WeekWiseStepData.TueRadialGaugeGraph
                       = await ActivitySelection(WeekWiseStepData, WeekWiseStepData.dayvalue(day, type), type);
                    break;
                case 3:
                    WeekWiseStepData.Wed = await GetStepCount(dataset[0]);
                    WeekWiseStepData.WedSelectDay = day;
                    WeekWiseStepData.WedCal = await GetCaloreis(dataset[1]);
                    WeekWiseStepData.WedDistance = await GetDistance(dataset[2]);
                    WeekWiseStepData.WedHeart = await GetHeartPoint(dataset[3]);
                    StepColletion[day] = WeekWiseStepData.Wed;
                    WeekWiseStepData.WedRadialGaugeGraph
                       = await ActivitySelection(WeekWiseStepData, WeekWiseStepData.dayvalue(day, type), type);
                    break;
                case 4:
                    WeekWiseStepData.Thu = await GetStepCount(dataset[0]);
                    WeekWiseStepData.ThuSelectDay = day;
                    WeekWiseStepData.ThuCal = await GetCaloreis(dataset[1]);
                    WeekWiseStepData.ThuDistance = await GetDistance(dataset[2]);
                    WeekWiseStepData.ThuHeart = await GetHeartPoint(dataset[3]);
                    StepColletion[day] = WeekWiseStepData.Thu;
                    WeekWiseStepData.ThuRadialGaugeGraph
                       = await ActivitySelection(WeekWiseStepData, WeekWiseStepData.dayvalue(day, type), type);
                    break;
                case 5:
                    WeekWiseStepData.Fri = await GetStepCount(dataset[0]);
                    WeekWiseStepData.FriSelectDay = day;
                    WeekWiseStepData.FriCal = await GetCaloreis(dataset[1]);
                    WeekWiseStepData.FriDistance = await GetDistance(dataset[2]);
                    WeekWiseStepData.FriHeart = await GetHeartPoint(dataset[3]);
                    StepColletion[day] = WeekWiseStepData.Fri;
                    WeekWiseStepData.FriRadialGaugeGraph
                       = await ActivitySelection(WeekWiseStepData, WeekWiseStepData.dayvalue(day, type), type);
                    break;
                case 6:
                    WeekWiseStepData.Sat = await GetStepCount(dataset[0]);
                    WeekWiseStepData.SatCal = await GetCaloreis(dataset[1]);
                    WeekWiseStepData.SatSelectDay = day;
                    WeekWiseStepData.SatDistance = await GetDistance(dataset[2]);
                    WeekWiseStepData.SatHeart = await GetHeartPoint(dataset[3]);
                    StepColletion[day] = WeekWiseStepData.Sat;
                    WeekWiseStepData.SatRadialGaugeGraph
                       = await ActivitySelection(WeekWiseStepData, WeekWiseStepData.dayvalue(day, type), type);
                    break;

                default:
                    // code block
                    break;
            }
            return WeekWiseStepData;
        }


        public async Task<int> GetStepCount(Dataset dataset)
        {
            await Task.Delay(1);
            int caloreis = 0;
            if (dataset != null)
            {
                var Points = (List<Fijicare.Model.Response.Point>)dataset.point;
                foreach (var point in Points)
                {
                    caloreis += point.value[0].intVal;
                }
            }
            return caloreis;
        }

        public async Task<decimal> GetCaloreis(Dataset dataset)
        {
            await Task.Delay(1);
            float caloreis = 0;
            if (dataset != null)
            {
                var Points = (List<Fijicare.Model.Response.Point>)dataset.point;
                foreach (var point in Points)
                {
                    caloreis += point.value[0].fpVal;
                }
            }
            return Convert.ToDecimal(string.Format("{0:0.00}", caloreis)); ; ;
        }

        public async Task<int> GetHeartPoint(Dataset dataset)
        {
            float hearts = 0;
            await Task.Delay(1);
            if (dataset != null)
            {
                var Points = (List<Fijicare.Model.Response.Point>)dataset.point;
                foreach (var point in Points)
                {
                    foreach (var item in point.value)
                    {
                        hearts += item.intVal;
                    }

                }
            }
            return (int)hearts;
        }

        public async Task<decimal> GetDistance(Dataset dataset)
        {
            float distance = 0;
            await Task.Delay(1);
            if (dataset != null)
            {
                var Points = (List<Fijicare.Model.Response.Point>)dataset.point;
                foreach (var point in Points)
                {
                    distance += point.value[0].fpVal;
                }
            }
            distance = distance / 1000;
            return Convert.ToDecimal(string.Format("{0:0.00}", distance)); ;
        }


        public async Task<RadialGaugeChart> CaloresRadialGraph(int spt)
        {
            await Task.Delay(1);
            var entries = new[]
            {

                 new ChartEntry(spt)
                 {
                     ValueLabel = spt.ToString(),

                     Color = SKColor.Parse("#296fde"),
                     ValueLabelColor= SKColor.Parse("#ffffff")
                 }
            };
           var  RadialGaugeGraph = new RadialGaugeChart()
            {
                MinValue = 0,
                MaxValue = 100,
                AnimationDuration = TimeSpan.FromSeconds(2),
                LineAreaAlpha = 40,
                StartAngle = 105,
                Entries = entries,
                BackgroundColor = SKColor.Parse("#ffffff")
            };
            return RadialGaugeGraph;

        }
    }
}
