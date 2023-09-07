// Author : https://github.com/seekeroftheball   https://gist.github.com/seekeroftheball
// Version : 1.2
// Updated : March 2023
// Vam Vam Template Modification September 2023

namespace NewFolderWizard
{
    /// <summary>
    /// All file paths for directories and menu items.
    /// </summary>
    struct FilePaths
    {
        private const string RootDirectoryFileName = "GameRootFolder";

        public const string PathToAssetsFolder = "Assets";
        public const string ResourceFolderRelativePathToDirectories = "Directories/";
        public const string ResourceFolderRelativePathToRootDirectory = ResourceFolderRelativePathToDirectories + RootDirectoryFileName;        
        public const string MenuItemPath = "Window/Plugins/New Folder Wizard/";
    }
}