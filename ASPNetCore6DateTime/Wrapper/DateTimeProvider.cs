namespace ASPNetCore6DateTime.Wrapper
{
    using System;

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}
