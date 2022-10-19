using System;

namespace Application.Interfaces
{
    public interface ISystemTimeService
    {
        DateTime SystemTime { get; set; }
        string IncreaseTime(int hour);
    }
}
