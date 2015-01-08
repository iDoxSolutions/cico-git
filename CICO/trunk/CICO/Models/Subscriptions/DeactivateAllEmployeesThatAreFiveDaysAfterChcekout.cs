using System;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using log4net;

namespace Cico.Models.Subscriptions
{
    public class DeactivateAllEmployeesThatAreFiveDaysAfterChcekout : IDailyExecute
    {
         private readonly ICicoContext _db;
        private readonly HttpContextBase _context;
        private static readonly ILog log = LogManager.GetLogger(typeof(DeactivateAllEmployeesThatAreFiveDaysAfterChcekout).Name);

        public DeactivateAllEmployeesThatAreFiveDaysAfterChcekout(ICicoContext db, HttpContextBase context)
        {
            _db = db;
            _context = context;
        }
        public void PerformDaily(DateTime refDate)
        {
            var oldChecklists = _db.CheckListSessions.Where(c => c.Completed && c.CheckListTemplate.Type == "CheckOut" && SqlFunctions.DateDiff("day", c.DateCompleted, refDate) > 5);
            log.Debug("Looking for employees to deactivate");
            foreach (var checkListSession in oldChecklists)
            {
                log.DebugFormat("checklist {0}",checkListSession.Id);
                if (
                    checkListSession.CheckListItemSubmitionTracks.Any(
                        c => c.CheckListItemTemplate.Provisional && !c.Provisioned))
                {
                    log.DebugFormat("checklist {0} has provisional items without approval", checkListSession.Id);
                }
                else
                {
                    log.DebugFormat("Deactivating user {0} - {1} {2}", checkListSession.Employee.Id, checkListSession.Employee.FirstName, checkListSession.Employee.LastName);
                    checkListSession.Employee.Active = false;    
                }
                
            }
        }
    }
}