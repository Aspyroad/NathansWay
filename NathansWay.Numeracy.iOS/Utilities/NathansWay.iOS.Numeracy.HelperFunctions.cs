using System;
using System.Drawing;

namespace NathansWay.iOS.Numeracy
{
	public static class HelperFunctions
	{
		public HelperFunctions ()
		{
		}

		public static RectangleF StringToRectF(string _strRect)
		{
			// x, y, width, height
			var strArray = _strRect.Split (',');
			RectangleF tmpRect = 
				new RectangleF (
					float.Parse (strArray [0]),
					float.Parse (strArray [1]),
					float.Parse (strArray [2]),
					float.Parse (strArray [3])
				);
			return tmpRect;

		}

		public static string RectFToString(RectangleF _rctString)
		{
			//Convert a RectF to Text (csv)
			string tmpStrRect = 
				_rctString.X.ToString () + ',' +
				_rctString.Y.ToString () + ',' +
				_rctString.Width.ToString () + ',' +
				_rctString.Height.ToString ();
			return tmpStrRect;

		}
	}
}

