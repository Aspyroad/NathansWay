// System
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

// NathansWay
using NathansWay.Shared.DAL;
using NathansWay.Shared.BUS;
using NathansWay.Shared.DAL.Repository;
using NathansWay.Shared.BUS.ViewModel;


namespace NathansWay.Shared.Utilities
{
    /// <summary>
    /// Class for registering services for the app
    /// </summary>
    public static class SharedServiceRegistrar 
    {
        
		public static void Startup ()
		{
			InitializeShared ();
		}
        
		private static void InitializeShared()
		{
			SharedServiceContainer.Register<IRepoLessons> (() => new RepoLessons ());
			//            ServiceContainer.Register<IAssignmentService> (() => new SampleAssignmentService ());
			//
			#if !NETFX_CORE
				//Only do these on iOS or Android
				SharedServiceContainer.Register<LessonViewModel> ();
			//            ServiceContainer.Register<AssignmentViewModel>();
			//            ServiceContainer.Register<DocumentViewModel>();
			//            ServiceContainer.Register<ExpenseViewModel>();
			//            ServiceContainer.Register<HistoryViewModel>();
			//            ServiceContainer.Register<ItemViewModel>();
			//            ServiceContainer.Register<LaborViewModel>();
			//            ServiceContainer.Register<LoginViewModel>();
			//            ServiceContainer.Register<PhotoViewModel>();
			#endif		
		}
    }
}
