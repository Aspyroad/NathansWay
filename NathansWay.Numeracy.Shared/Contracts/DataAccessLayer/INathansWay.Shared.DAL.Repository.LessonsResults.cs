// Core
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


// NathansWay
using NathansWay.Numeracy.Shared.BUS.Entity;
using NathansWay.Numeracy.Shared.BUS;

// Order of class declaration
// The order of things in a class declaration is:

// 1. Attributes, in square brackets
// 2. Modifiers("public", "static", and so on)
// 3. "partial"
// 4. "class"
// 5. The class name
// 6. A comma-separated list of type parameter declarations inside angle brackets
// 7. A colon followed a comma-separated list of base types(base class and implemented interfaces, base class must go first if there is one)
// 8. Type parameter constraints
// 9. The body of the class, surrounded by bracestw
// 10.A semicolon

namespace NathansWay.Numeracy.Shared.DAL.Repository
{
    public interface IRepoLessonResults<T> where T : EntityLessonResults, new()
    {
    }

	public interface IRepoLessonDetailResults<T> where T : EntityLessonDetailResults, new()
	{
        // Filters
        NWFilter FilterLessonResultSeq { get; set; }

        // Database Operations
        Task<List<T>> GetFilteredLessonDetailResultsAsync();
	}
}

