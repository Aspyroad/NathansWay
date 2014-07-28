using System;


namespace NathansWay.Shared.Global
{
    public interface ISharedGlobal
    {		
		/// <summary>
		/// Path to the library folder
		/// </summary>
        string GS__FolderNameLibrary { get; set; }
		/// <summary>
		/// Path to the ImageData folder
		/// </summary>
        string GS__FolderNameImageData { get; set; }
		/// <summary>
		/// The name of the version file.
		/// </summary>
        string GS__VersionFileName { get; set; }
        /// <summary>
        /// Global Database Name.
        /// </summary>
        string GS__DatabaseName { get; set; }
        /// <summary>
        /// iOS Database File Path.
        /// </summary>
        string GS__iOSDatabasePath { get; set; }
        /// <summary>
        /// Windows Database File Path.
        /// </summary>
        string GS__WinDatabasePath { get; set; }
        /// <summary>
        /// Android Database File Path.
        /// </summary>
        string GS__AndroidDatabasePath { get; set; }
        /// <summary>
        /// User Documents Path.
        /// </summary>
        string GS__DocumentsPath { get; set; }
        /// <summary>
        /// Full database Path.
        /// </summary>
        string GS__FullDbPath { get; set; }
    }
}