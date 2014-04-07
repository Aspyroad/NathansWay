using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NathansWay.Shared.Forcopying
{
    /// <summary>
    /// Class for registering services for the app
    /// </summary>
    public static class ServiceRegistrar 
    {
        
		public static void Startup ()
		{
			InitializeShared ();
		}
        
		private static void InitializeShared()
		{
			//            ServiceContainer.Register<ILoginService> (() => new SampleLoginService ());
			//            ServiceContainer.Register<IAssignmentService> (() => new SampleAssignmentService ());
			//
			//#if !NETFX_CORE
			//            //Only do these on iOS or Android
			//            ServiceContainer.Register<MenuViewModel> ();
			//            ServiceContainer.Register<AssignmentViewModel>();
			//            ServiceContainer.Register<DocumentViewModel>();
			//            ServiceContainer.Register<ExpenseViewModel>();
			//            ServiceContainer.Register<HistoryViewModel>();
			//            ServiceContainer.Register<ItemViewModel>();
			//            ServiceContainer.Register<LaborViewModel>();
			//            ServiceContainer.Register<LoginViewModel>();
			//            ServiceContainer.Register<PhotoViewModel>();
			//#endif		
		}
    }
}
