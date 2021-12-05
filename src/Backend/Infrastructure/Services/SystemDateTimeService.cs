using System;
using EvrenDev.Application.Interfaces.Shared;

namespace EvrenDev.Infrastructure.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}