using PFS.Domain.Models.Entities.Management;

namespace PFS.Domain.Extensions;

public static class StringExtension
{
    public static string ValuesQueryInsert(this string[] values)
    {
        return string.Join(',',values.Select(x=>string.Concat("@",x)));
    }
    
    public static string SelectQueryInsert(this string[] values)
    {
        return string.Join(',',values.Select(x=>x));
    }
    public static string InsertPrefixTableAbv(this string[] values,string abv)
    {
        return string.Join(',',values.Select(x=>$"{abv}.{x}"));
    }
}