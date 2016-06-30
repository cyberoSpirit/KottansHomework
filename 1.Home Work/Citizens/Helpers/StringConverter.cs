//-----------------------------------------------------------------------
// <copyright file="StringConverter.cs" company="NuvoLetta">
//     Copyright © 2001-2016, NuvoLetta, Inc. All Rights Reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Citizens.Helpers
{
    using System.Globalization;

    /// <summary>
    /// Class convert string to title case.
    /// </summary>
    public static class StringConverter
    {
        /// <summary>
        /// Convert string to title case.
        /// </summary>
        /// <param name="value">Value for converting.</param>
        /// <returns>Title cased string.</returns>
        public static string ToTitleCase(this string value)
        {
            return new CultureInfo("en").TextInfo.ToTitleCase(value);
            ////return String.Join(" ", value.Split(' ')
            ////    .Select(i => i.Substring(0, 1).ToUpper() + i.Substring(1).ToLower()).ToArray());
        }
    }
}
