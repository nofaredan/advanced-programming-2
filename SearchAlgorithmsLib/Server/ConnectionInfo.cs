using System;
namespace Server
{
    /// <summary>
    /// Connection information
    /// </summary>
    public class ConnectionInfo
	{
        /// <summary>
        /// Gets or sets a value indicating whether [close connection].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [close connection]; otherwise, <c>false</c>.
        /// </value>
        public bool CloseConnection
		{
			get;
			set;
		}

        /// <summary>
        /// Gets or sets the answer.
        /// </summary>
        /// <value>
        /// The answer.
        /// </value>
        public string Answer
		{
			get;
			set;
		}
	}
}