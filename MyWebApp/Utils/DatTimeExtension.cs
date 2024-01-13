namespace MyWebApp.Utils
{
    public static class DatTimeExtension
    {
        public static DateTime TruncateToMinutes(this DateTime dateTime)
        {
            return dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.FromMinutes(1).Ticks));
        }
    }
}
