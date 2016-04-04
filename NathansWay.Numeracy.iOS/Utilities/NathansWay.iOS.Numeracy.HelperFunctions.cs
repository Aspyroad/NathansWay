using System;
using CoreGraphics;

namespace NathansWay.iOS.Numeracy
{
	public static class HelperFunctions
	{

		public static CGRect StringToRectF(string _strRect)
		{
			// x, y, width, height
			var strArray = _strRect.Split (',');
			CGRect tmpRect = 
				new CGRect (
					nfloat.Parse (strArray [0]),
					nfloat.Parse (strArray [1]),
					nfloat.Parse (strArray [2]),
					nfloat.Parse (strArray [3])
				);
			return tmpRect;

		}

		public static string RectFToString(CGRect _rctString)
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

