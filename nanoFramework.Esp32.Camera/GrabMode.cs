// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// How the driver fills the frame buffers.
    /// </summary>
    /// <remarks>
    /// Equivalent to <c>camera_grab_mode_t</c>.
    /// </remarks>
    public enum GrabMode
    {
        /// <summary>
        /// Fills the frame buffers when they are empty. Uses less resources, but the first captured frames might be old.
        /// </summary>
        WhenEmpty = 0,

        /// <summary>
        /// Except when a single frame buffer is used, the queue always contains the latest frames.
        /// </summary>
        Latest,
    }
}
