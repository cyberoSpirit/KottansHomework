using System;
using Citizens.Contructors;
using Citizens.Helpers;

namespace Citizens.Implementations
{
    public class Citizen : ICitizen
    {
        private DateTime dateOfBirth;
        private string firstName;
        private Gender gender;
        private string lastName;

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

        public DateTime BirthDate
        {
            get
            {
                return dateOfBirth;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
        }

        public Gender Gender
        {
            get
            {
                return gender;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
        }

        public string VatId { get; set; }


    }
}
