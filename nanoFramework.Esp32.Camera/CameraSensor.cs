// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// Camera sensors supported by the esp32-camera driver, identified by their product ID.
    /// </summary>
    /// <remarks>
    /// Equivalent to <c>camera_pid_t</c>.
    /// </remarks>
    public enum CameraSensor
    {
        /// <summary>Unknown sensor.</summary>
        Unknown = 0,

        /// <summary>OmniVision OV9650.</summary>
        Ov9650 = 0x96,

        /// <summary>OmniVision OV7725.</summary>
        Ov7725 = 0x77,

        /// <summary>OmniVision OV2640.</summary>
        Ov2640 = 0x26,

        /// <summary>OmniVision OV3660.</summary>
        Ov3660 = 0x3660,

        /// <summary>OmniVision OV3640.</summary>
        Ov3640 = 0x3640,

        /// <summary>OmniVision OV5640.</summary>
        Ov5640 = 0x5640,

        /// <summary>OmniVision OV7670.</summary>
        Ov7670 = 0x76,

        /// <summary>NT99141.</summary>
        Nt99141 = 0x1410,

        /// <summary>GalaxyCore GC2145.</summary>
        Gc2145 = 0x2145,

        /// <summary>GalaxyCore GC032A.</summary>
        Gc032a = 0x232a,

        /// <summary>GalaxyCore GC0308.</summary>
        Gc0308 = 0x9b,

        /// <summary>BYD BF3005.</summary>
        Bf3005 = 0x30,

        /// <summary>BYD BF20A6.</summary>
        Bf20a6 = 0x20a6,

        /// <summary>SmartSens SC101IOT.</summary>
        Sc101iot = 0xda4a,

        /// <summary>SmartSens SC030IOT.</summary>
        Sc030iot = 0x9a46,

        /// <summary>SmartSens SC031GS.</summary>
        Sc031gs = 0x0031,

        /// <summary>ArduCAM Mega.</summary>
        MegaCcm = 0x039e,

        /// <summary>Himax HM1055.</summary>
        Hm1055 = 0x0955,

        /// <summary>Himax HM0360.</summary>
        Hm0360 = 0x0360,
    }
}
