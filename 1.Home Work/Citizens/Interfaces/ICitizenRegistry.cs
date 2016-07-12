//-----------------------------------------------------------------------
// <copyright file="ICitizenRegistry.cs" company="NuvoLetta">
//     Copyright © 2001-2016, NuvoLetta, Inc. All Rights Reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Citizens.Interfaces
{
    /// <summary>
    /// Designate Registry for citizen entity.
    /// </summary>
    public interface ICitizenRegistry
    {
        /// <summary>
        /// Gets registered person by Id.
        /// </summary>
        /// <param name="id">Person's registration Id.</param>
        /// <returns>Registered person.</returns>
        ICitizen this[string id] { get; }

        /// <summary>
        /// Register citizen.
        /// </summary>
        /// <param name="citizen">Person for registration.</param>
        void Register(ICitizen citizen);

        /// <summary>
        /// Return statistic string.
        /// </summary>
        /// <returns>Statistic string.</returns>
        string Stats();
    }
}
