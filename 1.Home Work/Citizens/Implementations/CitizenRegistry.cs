//-----------------------------------------------------------------------
// <copyright file="CitizenRegistry.cs" company="NuvoLetta">
//     Copyright © 2001-2016, NuvoLetta, Inc. All Rights Reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Citizens.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Humanizer;
    using Implementations;

    /// <summary>
    /// Citizen register.
    /// </summary>
    public class CitizenRegistry : ICitizenRegistry
    {
        /// <summary>
        /// Registered people collection
        /// </summary>
        private Dictionary<string, ICitizen> register;

        /// <summary>
        /// Date of registration start.
        /// </summary>
        private DateTime startDate = new DateTime(1899, 12, 31);

        /// <summary>
        /// Initializes a new instance of the <see cref="CitizenRegistry" /> class.
        /// </summary>
        public CitizenRegistry()
        {
            register = new Dictionary<string, ICitizen>();
        }

        /// <inheritdoc />
        public ICitizen this[string id]
        {
            get
            {
                return register.ContainsKey(id) ? register[id] : null;
            }
        }

        /// <inheritdoc />
        public void Register(ICitizen citizen)
        {
            if (string.IsNullOrWhiteSpace(citizen.VatId))
            {
                int dayNumber = (citizen.BirthDate - startDate).Days;
                string dateString = dayNumber.ToString("D5");

                var oneDayBorned = register.Where(v => v.Key.StartsWith(dateString) && v.Key.Length == 10)
                    .Select(v => v.Key.Substring(5, 4));

                var genderSequenceNumbers = oneDayBorned.Where(v => GenderCondition(v, citizen.Gender))
                    .Select(v => int.Parse(v)).ToList();

                int currentNumber = genderSequenceNumbers.Any() ? genderSequenceNumbers.Max() : -1;

                int nextSequenceNumber = GetNextAllowed(currentNumber, citizen.Gender);
                string stringSequenceNumber = nextSequenceNumber.ToString("D4");

                int controlNumber = GetControlNumber(dateString, stringSequenceNumber);

                citizen.VatId = dateString + stringSequenceNumber + controlNumber;
            }

            if (!register.ContainsKey(citizen.VatId))
            {
                citizen.RegistrationDate = SystemDateTime.Now();
                register.Add(citizen.VatId, citizen.Clone() as ICitizen);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        /// <inheritdoc />
        public string Stats()
        {
            var genderList = register.Select(v => int.Parse(v.Key[8].ToString()) % 2).ToList();
            int maleCount = genderList.Count(v => v == 1);
            int femaleCount = genderList.Count - maleCount;
            string manNoun = maleCount == 1 ? "man" : "men";
            string womanNoun = femaleCount == 1 ? "woman" : "women";
            var registrationDays = register.Select(v => v.Value.RegistrationDate.Date - SystemDateTime.Now());

            var message = registrationDays.Any() ? DateTime.UtcNow.AddDays(registrationDays.Max().Days).Humanize() : string.Empty;

            message = message.Length > 0 ? ". Last registration was " + message : string.Empty;
            return string.Format("{0} {1} and {2} {3}{4}", maleCount, manNoun, femaleCount, womanNoun, message);
        }

        /// <summary>
        /// Check gender by Id.
        /// </summary>
        /// <param name="sequenceNumber">VAT id of person.</param>
        /// <param name="gender">Expected gender</param>
        /// <returns>True if person has passed gender and false in other case.</returns>
        private bool GenderCondition(string sequenceNumber, Gender gender)
        {
            int genderValue = gender == Gender.Male ? 1 : 0;
            return int.Parse(sequenceNumber[2].ToString()) % 2 == genderValue;
        }

        /// <summary>
        /// Generate VAT Id.
        /// </summary>
        /// <param name="number">Current last used id for this gender.</param>
        /// <param name="gender">Gender of person.</param>
        /// <returns>New VAT id.</returns>
        private int GetNextAllowed(int number, Gender gender)
        {
            int result = number + 1;
            int checkNumber = result % 10;

            if (gender == Gender.Male && checkNumber % 2 == 0)
            {
                return result + 1;
            }

            if (gender == Gender.Female && checkNumber % 2 != 0)
            {
                return result + 1;
            }

            return result;
        }

        /// <summary>
        /// Return control number for VAT id.
        /// </summary>
        /// <param name="dateString">Registration date.</param>
        /// <param name="stringSequenceNumber">Human's sequence number.</param>
        /// <returns>Digit which represent control number.</returns>
        private int GetControlNumber(string dateString, string stringSequenceNumber)
        {
            int controlNumber = (-1 * int.Parse(dateString[0].ToString())) +
                (5 * int.Parse(dateString[1].ToString())) +
                (7 * int.Parse(dateString[2].ToString())) +
                (9 * int.Parse(dateString[3].ToString())) +
                (4 * int.Parse(dateString[4].ToString())) +
                (6 * int.Parse(stringSequenceNumber[0].ToString())) +
                (10 * int.Parse(stringSequenceNumber[1].ToString())) +
                (5 * int.Parse(stringSequenceNumber[2].ToString())) +
                (7 * int.Parse(stringSequenceNumber[3].ToString()));
            controlNumber %= 11;
            controlNumber = controlNumber != 10 ? controlNumber : 0;
            return controlNumber;
        }
    }
}
