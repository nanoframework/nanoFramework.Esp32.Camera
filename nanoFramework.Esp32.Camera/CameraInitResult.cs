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

    /// <summary>
    /// Provides diagnostic messages for camera initialization results.
    /// </summary>
    public static class CameraInitResultExtensions
    {
        /// <summary>
        /// Gets a descriptive diagnostic message for an initialization result.
        /// </summary>
        /// <param name="result">The initialization result.</param>
        /// <returns>A diagnostic message describing the result.</returns>
        public static string GetMessage(this CameraInitResult result)
        {
            switch (result)
            {
                case CameraInitResult.Success:
                    return "Success: The camera was initialized.";
                case CameraInitResult.AlreadyInitialized:
                    return "AlreadyInitialized: A camera is already initialized.";
                case CameraInitResult.InvalidPin:
                    return "InvalidPin: A camera pin is invalid or missing.";
                case CameraInitResult.PinUnavailable:
                    return "PinUnavailable: A camera pin is already in use.";
                case CameraInitResult.InvalidSccbConfiguration:
                    return "InvalidSccbConfiguration: Set both SCCB pins, or set both to -1 and provide a valid I2C port.";
                case CameraInitResult.InvalidXclkFrequency:
                    return "InvalidXclkFrequency: The XCLK frequency is invalid.";
                case CameraInitResult.InvalidFrameSize:
                    return "InvalidFrameSize: The frame size is invalid.";
                case CameraInitResult.InvalidJpegQuality:
                    return "InvalidJpegQuality: The JPEG quality must be between 0 and 63.";
                case CameraInitResult.InvalidFrameBufferCount:
                    return "InvalidFrameBufferCount: The frame buffer count is invalid.";
                case CameraInitResult.InvalidFrameBufferLocation:
                    return "InvalidFrameBufferLocation: The frame buffer location is invalid.";
                case CameraInitResult.InvalidGrabMode:
                    return "InvalidGrabMode: The grab mode is invalid.";
                case CameraInitResult.PsramUnavailable:
                    return "PsramUnavailable: Frame buffers require PSRAM, but PSRAM is unavailable.";
                case CameraInitResult.ClockUnavailable:
                    return "ClockUnavailable: The LEDC timer or channel used for XCLK is already in use.";
                case CameraInitResult.SensorNotDetected:
                    return "SensorNotDetected: Check the camera wiring, power, and SCCB configuration.";
                case CameraInitResult.SensorNotSupported:
                    return "SensorNotSupported: The sensor is unsupported or doesn't support JPEG output.";
                case CameraInitResult.FailedToSetFrameSize:
                    return "FailedToSetFrameSize: The sensor failed to set the frame size.";
                case CameraInitResult.FailedToSetPixelFormat:
                    return "FailedToSetPixelFormat: The sensor failed to set the pixel format.";
                case CameraInitResult.OutOfMemory:
                    return "OutOfMemory: There isn't enough memory for the frame buffers or driver.";
                case CameraInitResult.DriverError:
                    return "DriverError: The camera driver failed to initialize.";
                default:
                    return "Unknown camera initialization result (" + (int)result + ").";
            }
        }
    }
}
