using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniLinkLogic.Libraries.MiniLink.Data
{
    public interface IDbContext : IDisposable
    {     
        DbContext Instance { get; }
    }
}
