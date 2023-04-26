using System;
namespace Fijicare.Interfaces
{
    public interface IStepCounter
    {
        long StepNotStored { get; set; }

        void InitSensorService();

        void StopSensorService();
        bool IsAvailable();
    }
}
