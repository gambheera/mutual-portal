using Mutual.Portal.Core.Persistence;
using Mutual.Portal.Utility.Operations;
using System;
using System.Data.Entity;

namespace Mutual.Portal.Service._helpers
{
    public class ContextHandler
    {
        public static void FlushAttachedObjects(IOperationDbContext operationDbContext)
        {
            try
            {
                foreach (var entity in ((DbContext)operationDbContext).ChangeTracker.Entries())
                {
                    entity.State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                ResponseManager.GetExceptionResponse("Exception in Attached Files Flushing", ex, "EXP-00000");
            }

        }
    }
}
