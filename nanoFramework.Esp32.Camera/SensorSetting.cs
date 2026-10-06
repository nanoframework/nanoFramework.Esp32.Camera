// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

namespace nanoFramework.Esp32.Camera
{
    // sensor settings handled by the native sensor_t setters and read from camera_status_t
    internal enum SensorSetting
    {
        FrameSize = 0,
        Quality,
        Brightness,
        VFlip,
        HMirror,
    }
}
