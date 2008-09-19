using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Utils
{
	/// <summary>
	/// Provides functionality for executing a callback in a transaction.
	/// </summary>
    public class TransactionWrapper
    {
		/// <summary>
		/// Executes the provided delegate method in a transaction.
		/// </summary>
		/// <param name="callback">The delegate method to call.</param>
        public void RunInTransaction(Callback callback)
        {
            RunInTransaction(callback, IsolationLevel.Serializable, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Executes the provided delegate method in a transaction.
        /// </summary>
        /// <param name="callback">The delegate method to call.</param>
        /// <param name="isolationLevel">The isolation level of the transaction.</param>
        /// <param name="transactionTimeout">The timeout period of the transaction.</param>
        public void RunInTransaction(Callback callback, IsolationLevel isolationLevel, TimeSpan transactionTimeout)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = isolationLevel;
            options.Timeout = transactionTimeout;

            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, options))
            {
                callback();

                scope.Complete();
            }
        }
    }
}
