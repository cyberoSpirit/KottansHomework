using System;
using Citizens.Implementations;

namespace Citizens.Contructors
{
    public interface ICitizen
    {
        string FirstName { get; }
        string LastName { get; }
        Gender Gender { get; }
        DateTime BirthDate { get; }
        string VatId { get; set; }
    }
}