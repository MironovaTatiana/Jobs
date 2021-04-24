using System;


namespace Application
{
    /// <summary>
    /// Custom Exception
    /// </summary>
    public class JobsException : ArgumentException
    {
        /// <summary>
        /// Custom Exception
        /// </summary>
        public JobsException(string message)
        : base(message) {}
    }
}
