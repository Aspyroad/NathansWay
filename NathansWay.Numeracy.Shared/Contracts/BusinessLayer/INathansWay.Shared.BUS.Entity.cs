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
using NathansWay.Numeracy.Shared;
using NathansWay.Numeracy.Shared.DB;
using NathansWay.Numeracy.Shared.BUS.Services;

namespace NathansWay.Numeracy.Shared.BUS.Entity
{
    public interface IBusEntity
    {

        int SEQ { get; set; }
	}
}
