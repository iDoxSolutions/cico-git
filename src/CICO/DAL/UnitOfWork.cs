using System;
using Cico;
using Cico.Models;

namespace Cico.DAL
{
    public class UnitOfWork : IDisposable
    {
        private SchoolContext context = new SchoolContext();
        private GenericRepository<Department> departmentRepository;
        private GenericRepository<CheckList> checklistRepository;
        private CourseRepository courseRepository;

        public GenericRepository<Department> DepartmentRepository
        {
            get
            {

                if (this.departmentRepository == null)
                {
                    this.departmentRepository = new GenericRepository<Department>(context);
                }
                return departmentRepository;
            }
        }
        public GenericRepository<CheckList> ChecklistRepository {
            get {

                if (this.checklistRepository == null) {
                    this.checklistRepository = new GenericRepository<Cico.Models.CheckList>(context);
                }
                return checklistRepository;
            }
        }

        public CourseRepository CourseRepository
        {
            get
            {

                if (this.courseRepository == null)
                {
                    this.courseRepository = new CourseRepository(context);
                }
                return courseRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}