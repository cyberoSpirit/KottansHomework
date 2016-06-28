using System;

namespace Citizens.Contructors
{
    public interface ICitizenRegistry
    {
        void Register(ICitizen citizen);
        ICitizen this[string id] { get; }
        string Stats();
    }
}
