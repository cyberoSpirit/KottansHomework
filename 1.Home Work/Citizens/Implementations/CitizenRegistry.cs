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
    using Contructors;
    using Implementations;
    using Humanizer;

    /// <summary>
    /// Citizenz registrator.
    /// </summary>
    public class CitizenRegistry : ICitizenRegistry
    {
        private Dictionary<string, ICitizen> register;
        private DateTime startDate = new DateTime(1899, 12, 31);

        public CitizenRegistry()
        {
            register = new Dictionary<string, ICitizen>();
        }

        public ICitizen this[string id]
        {
            get
            {
                return register.ContainsKey(id) ? register[id] : null;
            }
        }

        public void Register(ICitizen citizen)
        {
            if(string.IsNullOrWhiteSpace(citizen.VatId))
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

                int controlNumber = -1 * int.Parse(dateString[0].ToString()) +
                    5 * int.Parse(dateString[1].ToString()) +
                    7 * int.Parse(dateString[2].ToString()) +
                    9 * int.Parse(dateString[3].ToString()) +
                    4 * int.Parse(dateString[4].ToString()) +
                    6 * int.Parse(stringSequenceNumber[0].ToString()) +
                    10 * int.Parse(stringSequenceNumber[1].ToString()) +
                    5 * int.Parse(stringSequenceNumber[2].ToString()) +
                    7 * int.Parse(stringSequenceNumber[3].ToString());
                controlNumber %= 11;
                controlNumber = controlNumber != 10 ? controlNumber : 0;

                //citizen.VatId = string.Format("{0:D5}{1:D4}{2}", dayNumber, nextSequenceNumber, controlNumber);
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

        private bool GenderCondition(string sequenceNumber, Gender gender)
        {
            return int.Parse(sequenceNumber[2].ToString()) % 2 == (int)gender;
        }

        private int GetNextAllowed(int number, Gender gender)
        {
            int result = number + 1;
            int checkNumber;
            checkNumber = (result % 10);
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

        public string Stats()
        {
            var genderList = register.Select(v => int.Parse(v.Key[8].ToString()) % 2).ToList();
            int maleCount = genderList.Where(v => v == 1).Count();
            int femaleCount = genderList.Where(v => v == 0).Count();
            string manNoun = maleCount == 1 ? "man" : "men";
            string womanNoun = femaleCount == 1 ? "woman" : "women";
            var registrationDays = register.Select(v => v.Value.RegistrationDate.Date - SystemDateTime.Now());

            var message = registrationDays.Any() ? DateTime.UtcNow.AddDays(registrationDays.Max().Days).Humanize() : "";

            message = message.Length > 0 ? ". Last registration was " + message : string.Empty;
            return string.Format("{0} {1} and {2} {3}{4}", maleCount, manNoun, femaleCount, womanNoun, message);
        }
    }
}
