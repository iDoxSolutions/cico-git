using System.Linq;
using Cico.Models;
using PagedList;

namespace Cico.Areas.Admin
{
    public class EmployeeIndexModel
    {
        public IPagedList<Employee> Employees { get; set; }
        public string SearchString { get; set; }
        public int? Page { get; set; }
        public bool EditEnabled { get; set; }

        public void Requery(ICicoContext db)
        {
            this.Page = Page ?? 1;
            IQueryable<Employee> emps = db.Employees;
            if (!string.IsNullOrEmpty(this.SearchString))
            {
                emps = emps.Where(s => s.FirstName.ToUpper().Contains(SearchString.ToUpper())
                                       || s.LastName.ToUpper().Contains(SearchString.ToUpper()));
            }
            emps = emps.Where(c => c.Active);
            emps = emps.OrderByDescending(c => c.ArrivalDate);

            Employees = emps.ToPagedList(Page.Value, 50);
        }

    }
}