namespace KingPriceDemo.Application.Common.Extensions
{
    public static class ReflectionTools
    {
        public static string GetName(this Type source)
        {
            if (!source.IsGenericType)
                return source.Name;

            Type typeDef = source.GetGenericTypeDefinition();
            var typeName = typeDef.Name?.Split('`').First();
            var genericNames = source.GenericTypeArguments.Select(o => o.GetName());
            var res = $"{typeName}<{string.Join(", ", genericNames)}>";
            return res;
        }
    }
}
