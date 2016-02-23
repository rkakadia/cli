using System.Collections.Generic;

namespace Microsoft.DotNet.ProjectModel.Compilation
{
    public class LibraryExportSubtarget
    {
        public string RuntimeIdentifier { get; }

        /// <summary>
        /// Gets a list of fully-qualified paths to MSIL binaries required to run
        /// </summary>
        public IEnumerable<LibraryAsset> RuntimeAssemblies { get; }

        /// <summary>
        /// Gets a list of fully-qualified paths to native binaries required to run
        /// </summary>
        public IEnumerable<LibraryAsset> NativeLibraries { get; }

        public LibraryExportSubtarget(string runtimeIdentifier,
            IEnumerable<LibraryAsset> runtimeAssemblies,
            IEnumerable<LibraryAsset> nativeLibraries)
        {
            RuntimeIdentifier = runtimeIdentifier;
            RuntimeAssemblies = runtimeAssemblies;
            NativeLibraries = nativeLibraries;
        }
    }
}