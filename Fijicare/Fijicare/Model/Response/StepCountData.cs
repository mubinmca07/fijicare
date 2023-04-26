using System;
using System.Collections.Generic;

namespace Fijicare.Model.Response
{
    public class Value
    {
        public List<float> mapVal { get; set; } //
        public int intVal { get; set; }// for step
        public float fpVal { get; set; }// for calories
        //"fpVal": 591.591549873352
    }

    public class Point
    {
        public string startTimeNanos { get; set; }
        public string originDataSourceId { get; set; }
        public string endTimeNanos { get; set; }
        public List<Value> value { get; set; }
        public string dataTypeName { get; set; }
    }

    public class Dataset
    {
        public string dataSourceId { get; set; }
        public List<Point> point { get; set; }
    }

    public class Bucket
    {
        public string startTimeMillis { get; set; }
        public string endTimeMillis { get; set; }
        public List<Dataset> dataset { get; set; }
    }

    public class BucketCount
    {
        public List<Bucket> bucket { get; set; }
    }

}
