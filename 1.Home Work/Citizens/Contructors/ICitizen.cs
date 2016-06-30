//-----------------------------------------------------------------------
// <copyright file="ICitizen.cs" company="NuvoLetta">
//     Copyright © 2001-2016, NuvoLetta, Inc. All Rights Reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Citizens.Contructors
{
    using System;
    using Citizens.Implementations;

    /// <summary>
    /// Designate citizen entity.
    /// </summary>
    public interface ICitizen : ICloneable
    {
        /// <summary>
        /// Gets first name of person.
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Gets last name of person.
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// Gets gender of person.
        /// </summary>
        Gender Gender { get; }

        /// <summary>
        /// Gets birthday Date of person.
        /// </summary>
        DateTime BirthDate { get; }

        /// <summary>
        /// Gets or sets VAT Id number of person.
        /// </summary>
        string VatId { get; set; }

        /// <summary>
        /// Gets or sets  registration date of registration.
        /// </summary>
        DateTime RegistrationDate { get; set; }
    }
}