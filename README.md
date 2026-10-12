[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_nanoFramework.Esp32.Camera&metric=alert_status)](https://sonarcloud.io/dashboard?id=nanoframework_nanoFramework.Esp32.Camera) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_nanoFramework.Esp32.Camera&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=nanoframework_nanoFramework.Esp32.Camera) [![NuGet](https://img.shields.io/nuget/dt/nanoFramework.Esp32.Camera.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Esp32.Camera/) [![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md) [![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)

![nanoFramework logo](https://raw.githubusercontent.com/nanoframework/Home/main/resources/logo/nanoFramework-repo-logo.png)

-----

### Welcome to the .NET **nanoFramework** ESP32 Camera Library repository

## Build status

| Component | Build Status | NuGet Package |
|:-|---|---|
[![Build Status](https://dev.azure.com/nanoframework/nanoFramework.Esp32.Camera/_apis/build/status%2FnanoFramework.Esp32.Camera?repoName=nanoframework%2FnanoFramework.Esp32.Camera&branchName=refs%2Fpull%2F14%2Fmerge)](https://dev.azure.com/nanoframework/nanoFramework.Esp32.Camera/_build/latest?definitionId=131&repoName=nanoframework%2FnanoFramework.Esp32.Camera&branchName=refs%2Fpull%2F14%2Fmerge)

JPEG camera capture for nanoFramework ESP32, ESP32-S2 and ESP32-S3 devices using Espressif's [`esp32-camera`](https://github.com/espressif/esp32-camera) driver.

## Usage

```csharp
using nanoFramework.Esp32.Camera;

CameraConnectionSettings settings = CameraBoardPresets.AiThinkerEsp32Cam();
settings.FrameSize = FrameSize.Vga;
settings.FrameBufferCount = 1;

using (var camera = new Esp32Camera(settings))
{
    CameraInitResult result = camera.Initialize();

    if (result != CameraInitResult.Success)
    {
        Debug.WriteLine($"Camera initialization failed: {GetMessage(result)}");
        return;
    }

    camera.FrameSize = FrameSize.Qvga;
    camera.Quality = 12;

    if (camera.TryCapture(out byte[] jpeg))
    {
        // use the image
    }
}

/// <summary>
/// Gets a descriptive diagnostic message for an initialization result.
/// </summary>
/// <param name="result">The initialization result.</param>
/// <returns>A diagnostic message describing the result.</returns>
public static string GetMessage(CameraInitResult result)
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

```

This example initializes the camera with VGA-sized frame buffers and then reduces the sensor output to QVGA. Setting the larger size before `Initialize()` allows the application to switch between QVGA and VGA later without reinitializing the camera. If the application will only capture QVGA images, set `settings.FrameSize` to `FrameSize.Qvga` and omit the later `camera.FrameSize` assignment to reduce memory use.

`Initialize()` returns a `CameraInitResult` with the reason for a failure, such as an invalid configuration, a pin already in use, PSRAM not available or the sensor not being detected. The driver supports a single camera, so a second call will return `AlreadyInitialized`.

For repeated capture, reuse a buffer to avoid allocating a large array for every frame:

```csharp
byte[] buffer = new byte[64 * 1024];

if (camera.TryCapture(buffer, out int length))
{
    // use the first length bytes of buffer
}
```

`TryCapture` returns `false` when the driver doesn't get a frame from the sensor (the driver times out after about 4 seconds). `TryCapture(byte[], out int)` throws an `ArgumentException` when the buffer is too small for the image.

If a thread is aborted while it's waiting in `TryCapture`, the following captures can return the image from a previous capture. To recover, dispose the camera, then create and initialize a new one.

### Board presets

- `CameraBoardPresets.AiThinkerEsp32Cam()`
- `CameraBoardPresets.Esp32S3Eye()`
- `CameraBoardPresets.XiaoEsp32S3Sense()`

Presets describe boards rather than sensors because the driver detects the sensor (OV2640, OV3660, OV5640, GC0308, etc.), which is available in `Esp32Camera.Sensor`. Verify a preset against the schematic of the board revision being used.

### Configuring a board without a preset

Create a `CameraConnectionSettings` instance and map each camera signal to its ESP32 GPIO according to the board schematic.

```csharp
var settings = new CameraConnectionSettings
{
    PinPowerDown = POWER_DOWN_GPIO, // Use -1 when not connected.
    PinReset = RESET_GPIO,          // Use -1 for software reset.
    PinXclk = XCLK_GPIO,
    PinSccbSda = SCCB_SDA_GPIO,
    PinSccbScl = SCCB_SCL_GPIO,
    PinD7 = D7_GPIO,
    PinD6 = D6_GPIO,
    PinD5 = D5_GPIO,
    PinD4 = D4_GPIO,
    PinD3 = D3_GPIO,
    PinD2 = D2_GPIO,
    PinD1 = D1_GPIO,
    PinD0 = D0_GPIO,
    PinVsync = VSYNC_GPIO,
    PinHref = HREF_GPIO,
    PinPclk = PCLK_GPIO,
    XclkFreqHz = 20_000_000,
    FrameSize = FrameSize.Qvga,
    JpegQuality = 12,
    FrameBufferCount = 1,
    FrameBufferLocation = FrameBufferLocation.Psram,
    GrabMode = GrabMode.WhenEmpty,
};
```

For example, if the board schematic shows that the camera's D7 signal is connected to ESP32 GPIO 5, XCLK is connected to GPIO 15, and PCLK is connected to GPIO 13, those entries would be written as:

```csharp
var settings = new CameraConnectionSettings
{
    PinD7 = 5,
    PinXclk = 15,
    PinPclk = 13,
    // Set every other required signal to its GPIO in the same way.
};
```

These GPIO numbers are illustrative only and are not a complete configuration. Use the values from the schematic for the exact board and hardware revision being configured.

The data-pin names must follow the camera schematic's D0 through D7 labels; do not reorder them by GPIO number. `PinPowerDown` and `PinReset` can be `-1` when those signals are not connected.

For SCCB, either set both `PinSccbSda` and `PinSccbScl`, or set both to `-1` and assign `SccbI2cPort` to an I2C bus that has already been configured. Do not mix a GPIO-based SCCB setting with an existing I2C-port setting.

For initial board bring-up, use a small frame size, one frame buffer, and `GrabMode.WhenEmpty`. Select `FrameBufferLocation.Psram` only when PSRAM is present and enabled in the device firmware; otherwise use `FrameBufferLocation.Dram` with a small frame size. If initialization fails, log `result.GetMessage()` and verify the pin mapping, camera power supply, XCLK frequency, and SCCB wiring.

### Connection settings and sensor settings

`CameraConnectionSettings` is the equivalent of `camera_config_t`: pins, XCLK frequency, frame size, JPEG quality, frame buffer count and location, grab mode and SCCB I2C port. The camera keeps a copy of the settings taken in the constructor, so later changes to the settings object have no effect, and `Esp32Camera.ConnectionSettings` returns a copy.

The sensor settings are properties of `Esp32Camera`, equivalent to the `sensor_t` setters: `FrameSize` (`set_framesize`), `Quality` (`set_quality`), `Brightness` (`set_brightness`), `VFlip` (`set_vflip`) and `HMirror` (`set_hmirror`). The values are read back from the sensor status.

The frame buffers are allocated on initialization, so `CameraConnectionSettings.FrameSize` establishes the largest supported runtime frame size. `Esp32Camera.FrameSize` can reduce the capture size after initialization and can later increase it again, but it can't exceed the initialized size.

This maximum-size behavior is specific to `FrameSize`. `CameraConnectionSettings.JpegQuality` sets the initial JPEG quality, and `Esp32Camera.Quality` can change it at runtime from 0 (highest quality, largest image) to 63 (lowest quality, smallest image). Changing quality does not change the configured maximum frame size, but a higher quality can produce a larger JPEG and may require a larger buffer when using `TryCaptureBuffer`.

GPIO assignments, XCLK frequency, SCCB configuration, frame buffer count, frame buffer location, and grab mode are initialization settings and can't be changed through `Esp32Camera`. `Brightness`, `VFlip`, and `HMirror`, like `FrameSize` and `Quality`, are sensor settings that can be changed after initialization.

Use at least two frame buffers with `GrabMode.Latest` to reduce stale frames during continuous capture. A single frame buffer with `GrabMode.WhenEmpty` uses less memory and is suitable for bring-up.

### Memory

PSRAM is shared with the managed heap. The frame buffers and the managed copy of the image must fit at the same time. Start with QQVGA or QVGA and a single frame buffer when validating a board.

`FrameBufferLocation.Dram` can be used for small frame sizes with a single frame buffer on devices without PSRAM. Larger frame sizes require PSRAM.

## Feedback and documentation

For documentation, providing feedback, issues and finding out how to contribute please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The **nanoFramework** Class Libraries are licensed under the [MIT license](LICENSE.md).

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behaviour in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

### .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
