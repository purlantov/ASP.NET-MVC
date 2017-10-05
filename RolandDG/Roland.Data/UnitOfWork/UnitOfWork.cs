using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roland.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MsSqlDbContext context;

        public UnitOfWork(MsSqlDbContext context)
        {
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
