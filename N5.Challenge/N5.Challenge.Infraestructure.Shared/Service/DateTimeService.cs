using N5.Challenge.Core.Application.Interfaces;
using System;

namespace N5.Challenge.Infraestructure.Shared.Service
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.Now;
    }
}
