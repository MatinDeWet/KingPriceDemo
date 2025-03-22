using KingPriceDemo.Application.Common.Models;

namespace KingPriceDemo.Application.Common.Extensions
{
    public class EnumTools
    {
        public static List<BasicList> GetValuesAndDisplayNames<TEnum>() where TEnum : Enum
        {
            var enumType = typeof(TEnum);
            var result = new List<BasicList>();

            foreach (var enumValue in Enum.GetValues(enumType))
            {
                int intValue = Convert.ToInt32(enumValue);

                if (intValue == 0)
                    continue;

                var memberInfo = enumType.GetMember(enumValue.ToString()!).FirstOrDefault();

                string displayName = enumValue.ToString()!;

                if (memberInfo != null)
                    displayName = memberInfo.GetDisplayName();

                result.Add(new BasicList { Id = intValue, Name = displayName! });
            }

            return result;
        }
    }
}
