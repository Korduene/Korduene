using System.ComponentModel;
using System.Diagnostics;

namespace Korduene.Graphing
{
    /// <summary>
    /// Connection Result
    /// </summary>
    /// <seealso cref="INotifyPropertyChanged" />
    [Description("Connection Result")]
    [DisplayName("ConnectionResult")]
    [DebuggerDisplay("{Success}, {Message}")]
    public class ConnectionResult
    {
        #region [Private Objects]

        private string _message;
        private bool _success;

        #endregion

        #region [Public Properties]

        /// <summary>
        /// Gets or sets a value indicating whether the connection is a success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success
        {
            get { return _success; }
            set
            {
                if (_success != value)
                {
                    _success = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                }
            }
        }

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionResult"/> class.
        /// </summary>
        public ConnectionResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        public ConnectionResult(bool success) : this()
        {
            this.Success = success;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="message">The message.</param>
        public ConnectionResult(bool success, string message) : this(success)
        {
            this.Message = message;
        }

        #endregion

        #region [Public Methods]

        #endregion

        #region [Private Methods]

        #endregion
    }
}
