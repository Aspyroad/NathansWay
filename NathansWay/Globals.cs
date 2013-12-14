using System;
using AspyRoad.iOSCore;

namespace NathansWay.iOS.Numeracy
{
	public sealed class Globals : AspyGlobals
    {
		static readonly Globals _instance = new Globals ();
		
		private Globals()
		{
		}
		
		public static Globals Instance
		{
			get { return _instance; }
	    }

		/// <summary>
		/// Path to the library folder
		/// </summary>
		private const string FolderNameLibrary = "Library";

		/// <summary>
		/// Path to the ImageData folder
		/// </summary>
		private const string FolderNameImageData = "ImageData";

		/// <summary>
		/// The name of the version file.
		/// </summary>
		private const string VersionFileName = "version.dat";

    }
}

