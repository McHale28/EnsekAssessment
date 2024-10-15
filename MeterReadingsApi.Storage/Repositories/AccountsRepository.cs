using MeterReadingsApi.Storage.Context.Interfaces;
using MeterReadingsApi.Storage.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Storage.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IMeterReadingContext _context;

        public AccountsRepository(IMeterReadingContext context)
        {
            _context = context;
        }

        public Task<bool> AccountExistsWithId(int accountId)
        {
            return _context.Accounts.AnyAsync(a => a.Id == accountId);   
        }
    }
}
