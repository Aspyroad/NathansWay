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
using NathansWay.Numeracy.Shared;
using NathansWay.Numeracy.Shared.DB;
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.BUS;


namespace NathansWay.Numeracy.Shared.DAL.Repository
{
	public interface IRepoBlocks
	{
		RepoBlock<EntityBlock> repBlock { get; }
		RepoBlockDetail<EntityBlockDetail> repBlockDetail { get; } 
	}
}

