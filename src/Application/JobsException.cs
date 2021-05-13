using System;
using System.Net.Http;

namespace Application
{
    /// <summary>
    /// Custom Exception
    /// </summary>
    public class JobsException : HttpRequestException
    {
        /// <summary>
        /// Custom Exception
        /// </summary>
        public JobsException(string message)
        : base(message) {}
    }
}
