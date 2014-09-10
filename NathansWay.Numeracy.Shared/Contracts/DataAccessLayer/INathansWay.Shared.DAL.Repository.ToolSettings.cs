// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// SQLite
using SQLite.Net.Interop;

// NathansWay
using NathansWay.Shared.Global;
using NathansWay.Shared.DB;
using NathansWay.Shared.BUS.Entity;


namespace NathansWay.Shared.DAL.Repository
{
	public interface IRepoToolSettings<EntityToolSettings> : IRepository<IBusEntity>
	{
	}
}

