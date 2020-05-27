namespace Korduene
{
    /// <summary>
    /// Operation Result
    /// </summary>
    /// <seealso cref="Korduene.OperationResult{System.Object}" />
    public class OperationResult : OperationResult<object>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        public OperationResult() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> success.</param>
        public OperationResult(bool success) : base(success)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> success.</param>
        /// <param name="message">The message.</param>
        public OperationResult(bool success, string message) : base(success, message)
        {
        }
    }
}
