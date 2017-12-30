namespace Hqv.Seedwork.Utilities
{
    /// <summary>
    /// Extensions for string
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Is a string numeric? No decimals
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string arg)
        {
            return string.IsNullOrEmpty(arg) || long.TryParse(arg, out var _);
        }
    }
}