﻿using NLayer.Core.UnitOfWorks;
using NLayer.Repository.Context;

namespace NLayer.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Commit()
        {
            _appDbContext.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
