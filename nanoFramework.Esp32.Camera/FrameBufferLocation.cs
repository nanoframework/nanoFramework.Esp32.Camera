// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// Memory where the frame buffers are allocated.
    /// </summary>
    /// <remarks>
    /// Equivalent to <c>camera_fb_location_t</c>.
    /// </remarks>
    public enum FrameBufferLocation
    {
        /// <summary>
        /// Frame buffers are allocated in external PSRAM.
        /// </summary>
        Psram = 0,

        /// <summary>
        /// Frame buffers are allocated in internal DRAM.
        /// </summary>
        Dram,
    }
}
