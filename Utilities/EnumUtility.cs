using System;

namespace ClipTweet.Utilities
{
    public static class EnumUtility
    {
        public static T ToEnum<T>(this int i)
        {
            return (T)Enum.ToObject(typeof(T), i);
        }
    }
}