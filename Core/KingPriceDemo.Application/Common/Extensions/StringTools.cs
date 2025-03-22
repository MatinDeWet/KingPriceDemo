using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace KingPriceDemo.Application.Common.Extensions
{
    public static class StringTools
    {
        public static string GenerateRandomString(int length)
        {
            string allowed = "ABCDEFGHIJKLMONOPQRSTUVWXYZabcdefghijklmonopqrstuvwxyz0123456789";
            char[] randomChars = new char[length];

            for (int i = 0; i < length; i++)
                randomChars[i] = allowed[RandomNumberGenerator.GetInt32(0, allowed.Length)];

            string result = new string(randomChars);

            return result;
        }

        public static string ToStringWithSpaces(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return source;

            source = source.Replace("_", " ")
                .Trim();
            // regex to replace any 'cC' matched with 'c C'
            return Regex.Replace(source, "([a-z])([A-Z])", "$1 $2");
        }

        public static string GetName(this PropertyInfo source)
            => $"{source.DeclaringType!.Name}.{source.Name}";
    }
}
