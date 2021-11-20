namespace AssetQuote.Domain.Helprs
{
    public static class BrazilianDate
    {
        public static DateTime Now => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
    }
}
