using Microsoft.Extensions.Internal;
using System;

namespace Microsoft.DotNet.ProjectModel.Graph
{
    public class LockFileSubtarget
    {
        public string RuntimeIdentifier { get; }
        public LockFileTargetLibrary Definition { get; }

        public LockFileSubtarget(string runtimeIdentifier, LockFileTargetLibrary definition)
        {
            RuntimeIdentifier = runtimeIdentifier;
            Definition = definition;
        }

        public override bool Equals(object obj)
        {
            var other = obj as LockFileSubtarget;
            return other != null &&
                string.Equals(RuntimeIdentifier, other.RuntimeIdentifier, StringComparison.OrdinalIgnoreCase) &&
                Equals(Definition, other.Definition);
        }

        public override int GetHashCode()
        {
            var combiner = new HashCodeCombiner();
            combiner.Add(RuntimeIdentifier.ToLowerInvariant());
            combiner.Add(Definition);
            return combiner.CombinedHash;
        }
    }
}