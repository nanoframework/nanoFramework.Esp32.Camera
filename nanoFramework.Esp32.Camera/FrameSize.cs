// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// Frame sizes supported by the esp32-camera driver.
    /// </summary>
    /// <remarks>
    /// Equivalent to <c>framesize_t</c>. The values must match the version of the esp32-camera component included in the firmware.
    /// </remarks>
    public enum FrameSize
    {
        /// <summary>96x96.</summary>
        Size96X96 = 0,

        /// <summary>160x120.</summary>
        Qqvga,

        /// <summary>128x128.</summary>
        Size128X128,

        /// <summary>176x144.</summary>
        Qcif,

        /// <summary>240x176.</summary>
        Hqvga,

        /// <summary>240x240.</summary>
        Size240X240,

        /// <summary>320x240.</summary>
        Qvga,

        /// <summary>320x320.</summary>
        Size320X320,

        /// <summary>400x296.</summary>
        Cif,

        /// <summary>480x320.</summary>
        Hvga,

        /// <summary>640x480.</summary>
        Vga,

        /// <summary>800x600.</summary>
        Svga,

        /// <summary>1024x768.</summary>
        Xga,

        /// <summary>1280x720.</summary>
        Hd,

        /// <summary>1280x1024.</summary>
        Sxga,

        /// <summary>1600x1200.</summary>
        Uxga,

        /// <summary>1920x1080.</summary>
        Fhd,

        /// <summary>720x1280.</summary>
        PortraitHd,

        /// <summary>864x1536.</summary>
        Portrait3Mp,

        /// <summary>2048x1536.</summary>
        Qxga,

        /// <summary>2560x1440.</summary>
        Qhd,

        /// <summary>2560x1600.</summary>
        Wqxga,

        /// <summary>1080x1920.</summary>
        PortraitFhd,

        /// <summary>2560x1920.</summary>
        Qsxga,

        /// <summary>2592x1944.</summary>
        Size5Mp,
    }
}
