using System;
using Cico.Models;

namespace Cico.DAL
{
    public class ChecklistRepository : GenericRepository<CheckList>
    {
        public ChecklistRepository(SchoolContext context)
            : base(context)
        {
        }

        public int UpdateCourseCredits(int multiplier)
        {
            return context.Database.ExecuteSqlCommand("UPDATE Course SET Credits = Credits * {0}", multiplier);
        }

    }
}