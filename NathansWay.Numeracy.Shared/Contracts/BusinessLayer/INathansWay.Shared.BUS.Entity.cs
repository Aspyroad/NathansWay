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
using NathansWay.MonoGame.Global;
using NathansWay.MonoGame.DB;
using NathansWay.MonoGame.BUS.Services;

namespace NathansWay.MonoGame.BUS.Entity
{
    public interface IBusEntity
    {

        int SEQ { get; set; }
	}
}
