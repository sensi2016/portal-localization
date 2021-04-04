namespace Portal.DAL.Extensions
{
    public static class IntExtension
    {
        public static int TryToInt(this int? value)
        {
            int.TryParse(value.ToString(), out var intValue);
            return intValue;
        }

    }
}
