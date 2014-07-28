// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// SQLite
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Global;
using NathansWay.Shared.DB;
using NathansWay.Shared.BUS.Services;

namespace NathansWay.Shared.BUS.Entity
{
    public interface IBusEntity
    {
        int SEQ { get; set; }


	}
}
