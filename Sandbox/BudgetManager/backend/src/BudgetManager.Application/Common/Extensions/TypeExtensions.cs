using System.Text;

namespace BudgetManager.Application.Common.Extensions;

public static class TypeExtensions
{
    public static string GetFriendlyName(this Type type)
    {
        if (!type.IsGenericType)
            return type.Name;

        var sb = new StringBuilder();
        sb.Append(type.Name, 0, type.Name.IndexOf('`'));
        sb.Append('<');

        var args = type.GetGenericArguments();
        for (var i = 0; i < args.Length; i++)
        {
            if (i > 0) sb.Append(", ");
            sb.Append(args[i].GetFriendlyName());
        }

        sb.Append('>');
        return sb.ToString();
    }
}
