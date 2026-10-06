// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// Connection settings for common ESP32 camera boards.
    /// </summary>
    /// <remarks>
    /// Verify the preset against the schematic of the board revision being used.
    /// </remarks>
    public static class CameraBoardPresets
    {
        /// <summary>
        /// Creates the connection settings for the AI Thinker ESP32-CAM board.
        /// </summary>
        /// <returns>The connection settings for the board.</returns>
        public static CameraConnectionSettings AiThinkerEsp32Cam()
        {
            return new CameraConnectionSettings
            {
                PinPowerDown = 32,
                PinReset = -1,
                PinXclk = 0,
                PinSccbSda = 26,
                PinSccbScl = 27,
                PinD7 = 35,
                PinD6 = 34,
                PinD5 = 39,
                PinD4 = 36,
                PinD3 = 21,
                PinD2 = 19,
                PinD1 = 18,
                PinD0 = 5,
                PinVsync = 25,
                PinHref = 23,
                PinPclk = 22,
            };
        }

        /// <summary>
        /// Creates the connection settings for the Espressif ESP32-S3-EYE board.
        /// </summary>
        /// <returns>The connection settings for the board.</returns>
        public static CameraConnectionSettings Esp32S3Eye()
        {
            return new CameraConnectionSettings
            {
                PinPowerDown = -1,
                PinReset = -1,
                PinXclk = 15,
                PinSccbSda = 4,
                PinSccbScl = 5,
                PinD7 = 16,
                PinD6 = 17,
                PinD5 = 18,
                PinD4 = 12,
                PinD3 = 10,
                PinD2 = 8,
                PinD1 = 9,
                PinD0 = 11,
                PinVsync = 6,
                PinHref = 7,
                PinPclk = 13,
            };
        }

        /// <summary>
        /// Creates the connection settings for the Seeed Studio XIAO ESP32S3 Sense board.
        /// </summary>
        /// <returns>The connection settings for the board.</returns>
        public static CameraConnectionSettings XiaoEsp32S3Sense()
        {
            return new CameraConnectionSettings
            {
                PinPowerDown = -1,
                PinReset = -1,
                PinXclk = 10,
                PinSccbSda = 40,
                PinSccbScl = 39,
                PinD7 = 48,
                PinD6 = 11,
                PinD5 = 12,
                PinD4 = 14,
                PinD3 = 16,
                PinD2 = 18,
                PinD1 = 17,
                PinD0 = 15,
                PinVsync = 38,
                PinHref = 47,
                PinPclk = 13,
            };
        }
    }
}
