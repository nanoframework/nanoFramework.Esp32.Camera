// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// Result of the camera initialization.
    /// </summary>
    public enum CameraInitResult
    {
        /// <summary>
        /// The camera was initialized.
        /// </summary>
        Success = 0,

        /// <summary>
        /// A camera is already initialized. The esp32-camera driver supports a single camera.
        /// </summary>
        AlreadyInitialized,

        /// <summary>
        /// One of the pins isn't a valid GPIO, or a required pin is missing.
        /// </summary>
        InvalidPin,

        /// <summary>
        /// One of the pins is already in use.
        /// </summary>
        PinUnavailable,

        /// <summary>
        /// The SCCB configuration is invalid. Set both SCCB pins, or set both to -1 and provide a valid I2C port.
        /// </summary>
        InvalidSccbConfiguration,

        /// <summary>
        /// The XCLK frequency is invalid.
        /// </summary>
        InvalidXclkFrequency,

        /// <summary>
        /// The frame size is invalid.
        /// </summary>
        InvalidFrameSize,

        /// <summary>
        /// The JPEG quality is outside the 0 to 63 range.
        /// </summary>
        InvalidJpegQuality,

        /// <summary>
        /// The frame buffer count is invalid.
        /// </summary>
        InvalidFrameBufferCount,

        /// <summary>
        /// The frame buffer location is invalid.
        /// </summary>
        InvalidFrameBufferLocation,

        /// <summary>
        /// The grab mode is invalid.
        /// </summary>
        InvalidGrabMode,

        /// <summary>
        /// The frame buffers are set to be allocated in PSRAM, but the device doesn't have PSRAM.
        /// </summary>
        PsramUnavailable,

        /// <summary>
        /// The LEDC timer or channel used to generate XCLK is already in use by PWM.
        /// </summary>
        ClockUnavailable,

        /// <summary>
        /// The camera sensor wasn't detected. Check the wiring, power and SCCB configuration.
        /// </summary>
        SensorNotDetected,

        /// <summary>
        /// The camera sensor isn't supported, or doesn't support JPEG output.
        /// </summary>
        SensorNotSupported,

        /// <summary>
        /// The sensor failed to set the frame size.
        /// </summary>
        FailedToSetFrameSize,

        /// <summary>
        /// The sensor failed to set the pixel format.
        /// </summary>
        FailedToSetPixelFormat,

        /// <summary>
        /// There isn't enough memory for the frame buffers or the driver.
        /// </summary>
        OutOfMemory,

        /// <summary>
        /// The driver failed to initialize for another reason.
        /// </summary>
        DriverError,
    }
}
