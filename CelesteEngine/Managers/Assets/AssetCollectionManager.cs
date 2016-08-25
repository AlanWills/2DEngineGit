namespace CelesteEngine
{
    /// <summary>
    /// General manager in charge of custom asset collection API
    /// </summary>
    public static class AssetCollectionManager
    {
        /// <summary>
        /// The implementation of the interface used to obtain our files to load
        /// </summary>
        public static IAssetCollection AssetCollectionTechnique { get; set; }
    }
}
