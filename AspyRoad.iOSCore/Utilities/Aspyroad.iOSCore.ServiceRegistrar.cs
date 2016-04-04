using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AspyRoad.iOSCore
{
    /// <summary>
    /// Class for registering services for the app
    /// </summary>
    public static class iOSCoreServiceRegistrar 
    {
        
		public static void Startup (Action fInitialize)
		{
			fInitialize ();
		}
        

    }
}
