namespace RESERVATION_SYSTEM.Domain.Helpers
{
    public static class DateConverterHelper
    {
        public static DateTime ConvertDatetimeToLocalZone(
            this DateTime creationDate,
            string localZoneId = "SA Pacific Standard Time"
        )
        {
            TimeZoneInfo localZone = TimeZoneInfo.FindSystemTimeZoneById(localZoneId);

            return TimeZoneInfo.ConvertTime(creationDate, localZone);
        }

        public static bool IsLeapYear(this DateTime date)
        {
            const int FOUR_HUNDRED = 400;
            const int ONE_HUNDRED = 100;
            const int FOUR = 4;

            if ((date.Year % FOUR_HUNDRED) == 0 ||
                (date.Year % ONE_HUNDRED) == 0 ||
                (date.Year % FOUR) == 0
            )
            {
                return true;
            }

            return false;
        }
    }
}
