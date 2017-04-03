#region File Description
//-----------------------------------------------------------------------------
// Aspyroad.iOSCore.Window.cs
//
// Window level base class for AspyRoad Ltd.
// 
//-----------------------------------------------------------------------------
#endregion

#region Includes
using System;
using CoreGraphics;
using Foundation;
using UIKit;
#endregion

namespace AspyRoad.iOSCore
{
	// Aspy Delegates
	public delegate void GlobalTouchEventHandler (Object sender, GlobalTouchEventArgs e);
	
	public class GlobalTouchEventArgs : EventArgs
	{
		public UITouch UITouchObj { get; set; }
		public UIGestureRecognizer gestureType { get; set; }
		public string strGestureType { get; set; }
		public nint gestureCount { get; set; }
		public NSSet TouchSet { get; set; }
		public nint numTaps { get; set; }
		public double TimeReached { get; set; }		
	}

	public class AspyWindow : UIKit.UIWindow
	{
		#region Events

		public event GlobalTouchEventHandler SomeonesTouchingMeInMySpecialPlace;

        #endregion

        #region Private Variables

        // Actions
        private Action<UIView> _actionOnTouch;
        private UIView _vwViewMonitoringTouch;
        private bool _bInterceptTouch;

		#endregion

		#region Constructors
		
		public AspyWindow ()
		{
			//Initialize ();
		}
		public AspyWindow (IntPtr handle) : base(handle)
		{
			//Initialize ();
		}
		public AspyWindow (CGRect myRect) : base(myRect)
		{	
			//Initialize ();		
		}
			
		#endregion

		#region Public Properties

        public Action<UIView> ActionOnTouch
        {
            set
            {
                this._actionOnTouch = value;
            }
        }

        public bool InterceptTouch
        {
            set
            {
                this._bInterceptTouch = value;
            }

        }


		#endregion
		
		#region Private Members

		private void Initialize ()
		{
            this._bInterceptTouch = false;
		}

		protected virtual void IveBeenTouched (GlobalTouchEventArgs e)
		{
			GlobalTouchEventHandler handler = SomeonesTouchingMeInMySpecialPlace;
			if (handler != null)
			{
				handler(this, e);
			} 
		}

        private void HandleNSSetTouches(NSObject obj, ref bool stop)
        {
            var y = (UITouch)obj;

            if (y != null)
            {
                this._actionOnTouch.Invoke(y.View);
            }
            // We only need the first, stop enumerating after we have it
            stop = true;
        }

        #endregion

		#region Public Members

        //public override UIView HitTest(CGPoint point, UIEvent uievent)
        //{
        //    return base.HitTest(point, uievent);
        //}

		//public override void SendEvent (UIEvent evt)
		//{
  //         //         if (evt.Type == UIEventType.Touches && this._bInterceptTouch)
  //      			//{
  //         //             //if (this._actionOnTouch != null)
  //         //             {
  //         //                 // Interesting to note that Enumerate on NSSets is a Xamarin momotouch link to - enumerateObjectsUsingBlock: 
  //         //                 evt.AllTouches.Enumerate(HandleNSSetTouches);

  //         //                 // First attempt before I learnt a little about NSSet
  //         //                 //var y = evt.AllTouches.ToArray<UITouch>();
  //         //                 //this._actionOnTouch.Invoke(y[0].View);

  //         //                 // Reset as this could be called 2-3 times on one touch event
  //         //                 this._bInterceptTouch = false;
  //         //             }

  //         //             //				GlobalTouchEventArgs args = new GlobalTouchEventArgs();
  //         //             //				args.TouchSet = evt.AllTouches;
  //         //             //
  //         //             //				foreach (UITouch t in evt.AllTouches)
  //         //             //				{
  //         //             //                    if (t.GestureRecognizers != null)
  //         //             //                    {
  //         //             //                        args.gestureCount = t.GestureRecognizers.Length;
  //         //             //                        args.numTaps = t.TapCount;
  //         //             //                        args.UITouchObj = t;
  //         //             //                        args.TimeReached = t.Timestamp;
  //         //             //                        args.strGestureType = "No gesture";
  //         //             //                    }
  //         //             //
  //         //             //					//if (args.gestureCount == 1)
  //         //             //					//{
  //         //             ////					foreach (UIGestureRecognizer g in t.GestureRecognizers)
  //         //             ////					{
  //         //             ////						var myType = g.GetType();
  //         //             ////						if (myType.Name.Length > 0)
  //         //             ////						{
  //         //             ////							args.strGestureType = myType.Name;						
  //         //             ////						}
  //         //             ////					}
  //         //             //					//}	 
  //         //             //
  //         //             //
  //         //             //					IveBeenTouched(args);
  //         //             //				}

  //      			//}
  //          // MUST! be called
		//	base.SendEvent (evt);
		//}

		#endregion	
		
		#region Overrides

		//public override void SubviewAdded (UIView uiview)
		//{
		//	base.SubviewAdded (uiview);
		//	//this.ViewWithTag (666).Frame = new RectangleF (PointF.Empty, this.Frame.Size);
		//}

		#endregion	
	}	
	
	public class AspyGesture : UIGestureRecognizer
	{
		#region Constructors
		
		public AspyGesture () : base ()
		{
		
		}
		// Sys .ctor
		public AspyGesture (IntPtr handle) : base(handle)
		{
		
		}
		
		public AspyGesture (Action myAction) : base(myAction)
		{	
					
		}
			
		#endregion	
	}

}
