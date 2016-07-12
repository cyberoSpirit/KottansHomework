//-----------------------------------------------------------------------
// <copyright file="Citizen.cs" company="NuvoLetta">
//     Copyright © 2001-2016, NuvoLetta, Inc. All Rights Reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Citizens.Implementations
{
    using System;
    using Citizens.Interfaces;
    using Citizens.Helpers;

    /// <summary>
    /// Citizen logical model.
    /// </summary>
    public class Citizen : ICitizen
    {
        /// <summary>
        /// Container for birthday date.
        /// </summary>
        private DateTime dateOfBirth;

        /// <summary>
        /// Container for first name of person.
        /// </summary>
        private string firstName;

        /// <summary>
        /// Container for last name of person.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// Container for first name of person.
        /// </summary>
        private string lastName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Citizen" /> class.
        /// </summary>
        /// <param name="firstName">First name of person.</param>
        /// <param name="lastName">Last name of person.</param>
        /// <param name="dateOfBirth">Person's birthday date.</param>
        /// <param name="gender">Person's gender.</param>
        public Citizen(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            this.firstName = firstName.ToTitleCase();
            this.lastName = lastName.ToTitleCase();

            if (DateTime.Compare(dateOfBirth, DateTime.Now) <= 0)
            {
                this.dateOfBirth = dateOfBirth.Date;
            }
            else
            {
                throw new ArgumentException();
            }

            if (Enum.IsDefined(typeof(Gender), gender))
            {
                this.gender = gender;
            }
            else
            {
                throw new ArgumentOutOfRangeException(Gender.ToString());
            }
        }

        /// <inheritdoc />
        public DateTime BirthDate
        {
            get
            {
                return dateOfBirth;
            }
        }

        /// <inheritdoc />
        public string FirstName
        {
            get
            {
                return firstName;
            }
        }

        /// <inheritdoc />
        public Gender Gender
        {
            get
            {
                return gender;
            }
        }

        /// <inheritdoc />
        public string LastName
        {
            get
            {
                return lastName;
            }
        }

        /// <inheritdoc />
        public string VatId { get; set; }

        /// <inheritdoc />
        public DateTime RegistrationDate { get; set; }

        /// <inheritdoc />
        public object Clone()
        {
            var copy = new Citizen(firstName, lastName, dateOfBirth, gender);
            copy.VatId = VatId;
            copy.RegistrationDate = RegistrationDate;
            return copy;
        }
    }
}
