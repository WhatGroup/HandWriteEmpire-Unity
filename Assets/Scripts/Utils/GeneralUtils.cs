
//通过工具，用于打印数组等功能，后续待添加
public class GeneralUtils
{
    public static string printArray(string[] arr)
    {
        var result = "[ ";
        for (var i = 0; i < arr.Length; i++)
            if (i == arr.Length - 1)
                result += arr[i];
            else
                result += arr[i] + ", ";

        result += " ]";


        return result;
    }

    public static string JsonArrayToObject(string jsonData, string arrayName)
    {
        return "{" +
               "\"" + arrayName + "\" : " + jsonData +
               "}";
    }

    public static bool IsStringEmpty(string str)
    {
        if (str == null || "".Equals(str)) return true;

        return false;
    }
}