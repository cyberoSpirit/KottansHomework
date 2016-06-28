using System;
using Citizens.Implementations;

namespace Citizens.Contructors
{
    public interface ICitizen : ICloneable
    {
        string FirstName { get; }
        string LastName { get; }
        Gender Gender { get; }
        DateTime BirthDate { get; }
        string VatId { get; set; }
        DateTime RegistrationDate { get; set; }
    }
}