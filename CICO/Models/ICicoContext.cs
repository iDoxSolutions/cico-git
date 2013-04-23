using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Cico.Models.Helpers;

namespace Cico.Models
{
    public interface ICicoContext
    {
        IDbSet<Reminder> Reminders { get; set; }
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        DbEntityEntry Entry(object entity);
        IDbSet<AppFeature> AppFeatures { get; set; }
        IDbSet<DependentFile> DependentFiles { get; set; }
        IDbSet<SystemRole> SystemRoles { get; set; }
        IDbSet<EmailSubscription> EmailSubscriptions { get; set; }
        IDbSet<SystemFile> SystemFiles { get; set; }
        IDbSet<DropdownItem> DropdownItems { get; set; }
        IDbSet<CheckListTemplate> CheckListTemplates { get; set; }
        IDbSet<CheckListItemType> CheckListItemTypes { get; set; }
        IDbSet<Employee> Employees { get; set; }
        IDbSet<Dependent> Dependents { get; set; }
        IDbSet<Office> Offices { get; set; }
        IDbSet<Staff> Staffs { get; set; }
        IDbSet<CheckListItemTemplate> CheckListItemTemplates { get; set; }
        IDbSet<CheckList> CheckLists { get; set; }
        IDbSet<Setting> Settings { get; set; }
        IDbSet<Note> Notes { get; set; }
        IDbSet<CheckListItemSubmitionTrack> CheckListItemSubmitionTracks { get; set; }
        IDbSet<CheckListSession> CheckListSessions { get; set; }
        int SaveChanges();
    }
}