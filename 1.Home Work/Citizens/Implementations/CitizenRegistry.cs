using System;
using Citizens.Contructors;

namespace Citizens.Implementation
{
    public class CitizenRegistry : ICitizenRegistry
    {
        public ICitizen this[string id]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Register(ICitizen citizen)
        {
            throw new NotImplementedException();
        }

        public string Stats()
        {
            throw new NotImplementedException();
        }
    }
}
