using System;


namespace NathansWay.Numeracy.Shared
{
    public interface IGlobalShared
    {		
		/// <summary>
		/// Path to the library folder
		/// </summary>
        string GS__FolderNameLibrary { get; }
		/// <summary>
		/// Path to the ImageData folder
		/// </summary>
        string GS__FolderNameImageData { get; }
		/// <summary>
		/// The name of the version file.
		/// </summary>
        string GS__VersionFileName { get; }
        /// <summary>
        /// Global Database Name.
        /// </summary>
        string GS__DatabaseName { get; }
        /// <summary>
        /// iOS Database File Path.
        /// </summary>
        string GS__iOSDatabasePath { get; }
        /// <summary>
        /// Windows Database File Path.
        /// </summary>
        string GS__WinDatabasePath { get; }
        /// <summary>
        /// Android Database File Path.
        /// </summary>
        string GS__AndroidDatabasePath { get; }

    }
}