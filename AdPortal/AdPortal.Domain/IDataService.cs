namespace AdPortal.Domain
{
    using System;
    using System.ServiceModel;

    /// <summary>
    /// The contract we will use for interacting with data services.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Returns an array of advertisement data by querying between the given date ranges.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FaultException">Thrown if an error occurs within the data layer.</exception>
        /// <exception cref="CommunicationException">This and Child types are thrown if there is an exception connecting to the data layer.</exception>
        /// <exception cref="Exception">Child types are thrown if an error occurs while communicating to the data layer.</exception>
        Ad[] GetAdDataByDateRange(DateTime startDate, DateTime endDate);
    }
}
