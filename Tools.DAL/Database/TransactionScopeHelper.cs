using System.Transactions;

namespace Tools.DAL.Database
{
    public static class TransactionScopeHelper
    {
        public static TransactionScope GetTransactionScope()
        {
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted, Timeout = TransactionManager.MaximumTimeout });
        }
    }
}