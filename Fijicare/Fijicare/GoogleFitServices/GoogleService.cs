using System;
using System.Threading.Tasks;
using Fijicare.Model.Response;
using FijiCareRequest;

namespace Fijicare.GoogleFitServices
{
    public class GoogleService : APIService
    {
        public static Task<BucketCount> GetBucketCountData(long starttime, long endTime, long interval,string dataSource)
        {
            AggregateData aggregateData = new AggregateData();
            
            aggregateData.aggregateBy.Add(new AggregateBy()
            {
                dataSourceId = dataSource
            });
            aggregateData.bucketByTime.durationMillis = interval;
            aggregateData.startTimeMillis = starttime;
            aggregateData.endTimeMillis = endTime;

            return POST<BucketCount>("fitness/v1/users/me/dataset:aggregate", aggregateData);
        }


        public static Task<BucketCount> GetBucketCountData(long starttime, long endTime, long interval, string[] dataSource)
        {
            AggregateData aggregateData = new AggregateData();

            foreach (var stream in dataSource)
            {
                aggregateData.aggregateBy.Add(new AggregateBy()
                {
                    dataSourceId = stream
                });
            }
            
            aggregateData.bucketByTime.durationMillis = interval;
            aggregateData.startTimeMillis = starttime;
            aggregateData.endTimeMillis = endTime;

            return POST<BucketCount>("fitness/v1/users/me/dataset:aggregate", aggregateData);
        }
    }
}
