using Application.Interfaces;
using System;

namespace Application.Services
{
    public class SystemTimeService : ISystemTimeService
    {
        public DateTime SystemTime { get; set; }

        public string IncreaseTime(int hour)
        {
            SystemTime = SystemTime.AddHours(hour);
            return SystemTime.ToString("HH:mm");
        }
    }
}
