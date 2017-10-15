using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;

namespace Roland.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private MsSqlDbContext context;

        public UnitOfWork(MsSqlDbContext context)
        {
            Guard.WhenArgument(context, nameof(context)).IsNull().Throw();
            this.context = context;
        }

        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
