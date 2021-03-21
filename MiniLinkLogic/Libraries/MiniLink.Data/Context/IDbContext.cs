using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniLinkLogic.Libraries.MiniLink.Data
{
    /// <summary>
    /// Used to make repositories compatible with any context
    /// </summary>
    public interface IDbContext : IDisposable
    {     
        DbContext Instance { get; }
    }
}
