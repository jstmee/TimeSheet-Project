using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL.Infrastructure.Contract;

namespace TSheet.IBL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TSheetDB _dbContext;

        public UnitOfWork()
        {
            _dbContext = new TSheetDB();
        }

        public DbContext Db
        {
            get { return _dbContext; }
        }

        public void Dispose()
        {
        }
    }
}
