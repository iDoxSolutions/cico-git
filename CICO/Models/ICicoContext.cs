using System.Data.Entity;

namespace Cico.Models
{
    public interface ICicoContext
    {
        IDbSet<CheckListTemplate> CheckListTemplates { get; set; }
        IDbSet<CheckListItemType> CheckListItemTypes { get; set; }
        IDbSet<Employee> Employees { get; set; }
        IDbSet<Staff> Staffs { get; set; }
        IDbSet<CheckListItemTemplate> CheckListItemTemplates { get; set; }
        IDbSet<CheckList> CheckLists { get; set; }
        IDbSet<Setting> Settings { get; set; }
        IDbSet<CheckListItemSubmitionTrack> CheckListItemSubmitionTracks { get; set; }
        IDbSet<CheckListSession> CheckListSessions { get; set; }
        int SaveChanges();
    }
}