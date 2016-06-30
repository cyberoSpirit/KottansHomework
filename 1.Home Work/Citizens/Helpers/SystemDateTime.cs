//-----------------------------------------------------------------------
// <copyright file="SystemDateTime.cs" company="NuvoLetta">
//     Copyright © 2001-2016, NuvoLetta, Inc. All Rights Reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Citizens
{
    using System;

    /// <summary>
    /// Class for time providing.
    /// </summary>
    public static class SystemDateTime
    {
        /// <summary>
        /// Function redefinition for data generating.
        /// </summary>
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
