# nanoFramework.Esp32.Camera

JPEG camera capture for nanoFramework ESP32 and ESP32-S3 devices using Espressif's [`esp32-camera`](https://github.com/espressif/esp32-camera) driver.

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
        Debug.WriteLine($"Camera initialization failed: {result}");
        return;
    }

    camera.FrameSize = FrameSize.Qvga;
    camera.Quality = 12;

    if (camera.TryCapture(out byte[] jpeg))
    {
        // use the image
    }
}
```

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

## Board presets

- `CameraBoardPresets.AiThinkerEsp32Cam()`
- `CameraBoardPresets.Esp32S3Eye()`
- `CameraBoardPresets.XiaoEsp32S3Sense()`

Presets describe boards rather than sensors because the driver detects the sensor (OV2640, OV3660, OV5640, GC0308, etc.), which is available in `Esp32Camera.Sensor`. Verify a preset against the schematic of the board revision being used.

## Connection settings and sensor settings

`CameraConnectionSettings` is the equivalent of `camera_config_t`: pins, XCLK frequency, frame size, JPEG quality, frame buffer count and location, grab mode and SCCB I2C port. The camera keeps a copy of the settings taken in the constructor, so later changes to the settings object have no effect, and `Esp32Camera.ConnectionSettings` returns a copy.

The sensor settings are properties of `Esp32Camera`, equivalent to the `sensor_t` setters: `FrameSize` (`set_framesize`), `Quality` (`set_quality`), `Brightness` (`set_brightness`), `VFlip` (`set_vflip`) and `HMirror` (`set_hmirror`). The values are read back from the sensor status.

The frame buffers are allocated on initialization, so `Esp32Camera.FrameSize` can be reduced at runtime but can't exceed `CameraConnectionSettings.FrameSize`. JPEG quality ranges from 0 (highest quality, largest image) to 63.

Use at least two frame buffers with `GrabMode.Latest` to reduce stale frames during continuous capture. A single frame buffer with `GrabMode.WhenEmpty` uses less memory and is suitable for bring-up.

## Memory

PSRAM is shared with the managed heap. The frame buffers and the managed copy of the image must fit at the same time. Start with QQVGA or QVGA and a single frame buffer when validating a board.

`FrameBufferLocation.Dram` can be used for small frame sizes with a single frame buffer on devices without PSRAM. Larger frame sizes require PSRAM.
