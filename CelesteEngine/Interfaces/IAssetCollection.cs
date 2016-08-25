using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace CelesteEngine
{
    /// <summary>
    /// An interface for providing a certain platforms implementation for retrieving the content files we wish to load
    /// </summary>
    public interface IAssetCollection
    {
        /// <summary>
        /// A custom function to obtain all of the data paths from the inputted directory.
        /// Needs to be implemented because different platforms handle files differently, but also allows us to screen certain ones if necessary.
        /// FILES IN THE OUTPUT SHOULD BE RELATIVE TO THE CONTENT ROOT DIRECTORY
        /// Output should be a list of files of the form "\\Sprites\\Logo.xnb"
        /// </summary>
        /// <param name="content">The ContentManager we will use to load our content</param>
        /// <param name="directoryPath">The path of the directory containing the assets we wish to load</param>
        /// <returns>Returns the list of all assets we wish to load relative to the directoryPath and including extensions</returns>
        List<string> GetAllXnbFilesInDirectory(ContentManager content, string directoryPath);
    }
}
