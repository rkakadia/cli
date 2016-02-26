// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Linq;
using Microsoft.Extensions.PlatformAbstractions;
using Xunit;

namespace Microsoft.DotNet.Tools.Test.Utilities
{
    public class PlatformSkipTheoryAttribute : TheoryAttribute
    {
        public PlatformSkipTheoryAttribute(params Platform[] skipPlatforms)
        {
            var currentPlatform = PlatformServices.Default.Runtime.OperatingSystemPlatform;
            if (skipPlatforms.Any(platform => platform == currentPlatform))
            {
                this.Skip = $"This test skips on {currentPlatform}";
            }
        }
    }
}