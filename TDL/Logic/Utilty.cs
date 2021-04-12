using System;


namespace TDL.App_Code
{
    public class Utilty
    {
        public static Boolean CheckStringLength(string target, int length)
        {
            return target.Length > length;
        }

        public static Boolean CheckParseDateTime(string target)
        {
            return !DateTime.TryParse(target, out _);
        }

        public static string DatetimeOrNot(string target, Boolean callByTop = false)
        {
            if (target.Equals(""))
            {
                if (callByTop) { return ""; }
                else { return "日付に未入力があります。"; }
            }
            else
            {
                if (CheckParseDateTime(target))
                {
                    return "日付が不正です。";
                }
            }
            return "";
        }
    }
}