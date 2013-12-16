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
		private const string _folderNameLibrary = "Library";
		/// <summary>
		/// Path to the ImageData folder
		/// </summary>
		private const string _folderNameImageData = "ImageData";
		/// <summary>
		/// The name of the version file.
		/// </summary>
		private const string _versionFileName = "version.dat";

    }
}

