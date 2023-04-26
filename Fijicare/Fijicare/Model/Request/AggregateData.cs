using System;
using System.Collections.Generic;

namespace FijiCareRequest
{
    public class AggregateData
    {
        public List<AggregateBy> aggregateBy { get; set; }
        public BucketByTime bucketByTime { get; set; }
        public long startTimeMillis { get; set; }
        public long endTimeMillis { get; set; }
        public AggregateData()
        {
            aggregateBy = new List<AggregateBy>();
            bucketByTime = new BucketByTime();
        }

        public AggregateData(List<AggregateBy> aggregateBies, BucketByTime bucketByTime)
        {
            this.aggregateBy = aggregateBies;
            this.bucketByTime = bucketByTime;
        }
    }

    public class AggregateBy
    {
        public string dataSourceId { get; set; }
    }

    public class BucketByTime
    {
        public long durationMillis { get; set; }
    }
}
