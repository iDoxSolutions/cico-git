using System;

namespace Cico.Models.Subscriptions
{
    public interface IDailyExecute
    {
        void PerformDaily(DateTime refDate);
    }
}