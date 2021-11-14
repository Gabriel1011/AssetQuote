namespace AssetQuote.Domain.Helprs;

public static class OpenMarket
{
    public static bool CheckOpenMarket()
    {
        var today = DateTime.Now;

        return (today.Hour > 9 && today.Hour < 18) && !(today.DayOfWeek == DayOfWeek.Sunday || today.DayOfWeek == DayOfWeek.Saturday);
    }
}
