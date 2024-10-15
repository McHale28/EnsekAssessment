using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeterReadingsApi.Storage.Repositories.Interfaces
{
    public interface IAccountsRepository
    {
        Task<bool> AccountExistsWithId(int accountId);
    }
}
