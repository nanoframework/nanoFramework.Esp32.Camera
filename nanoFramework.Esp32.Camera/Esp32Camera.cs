// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// Controls a camera connected to an ESP32 through Espressif's esp32-camera driver.
    /// </summary>
    /// <remarks>
    /// The ESP32  camera driver is a singleton: only one camera can be initialized at a time.
    /// </remarks>
    public sealed class Esp32Camera : IDisposable
    {
        // this is used as the lock object
        // a lock is required because multiple threads can access the device
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly object _syncLock;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CameraConnectionSettings _connectionSettings;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _disposed;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _initialized;

        /// <summary>
        /// Gets the connection settings of the camera.
        /// </summary>
        /// <remarks>
        /// The connection settings are immutable after the camera is created, so the object returned is a clone of the settings.
        /// </remarks>
        public CameraConnectionSettings ConnectionSettings => _connectionSettings.Clone();

        /// <summary>
        /// Gets the camera sensor detected by the driver.
        /// </summary>
        /// <remarks>
        /// Product ID read by the driver, equivalent to <c>sensor_t.id.PID</c>.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">The camera has been disposed.</exception>
        /// <exception cref="InvalidOperationException">The camera hasn't been initialized.</exception>
        public CameraSensor Sensor
        {
            get
            {
                lock (_syncLock)
                {
                    CheckIfInitialized();

                    return (CameraSensor)NativeGetSensorId();
                }
            }
        }

        /// <summary>
        /// Gets or sets the frame size of the captured images.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>sensor_t.set_framesize</c>.
        /// The frame buffers are allocated during initialization, so this can't be larger than <see cref="CameraConnectionSettings.FrameSize"/>.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">The frame size is larger than the one used to initialize the camera.</exception>
        public FrameSize FrameSize
        {
            get => (FrameSize)GetSensorSetting(SensorSetting.FrameSize);

            set
            {
                if (value < FrameSize.Size96X96 || value > _connectionSettings.FrameSize)
                {
                    throw new ArgumentOutOfRangeException();
                }

                SetSensorSetting(SensorSetting.FrameSize, (int)value);
            }
        }

        /// <summary>
        /// Gets or sets the JPEG quality, from 0 (highest quality, largest image) to 63 (lowest quality, smallest image).
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>sensor_t.set_quality</c>.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">The value is outside the 0 to 63 range.</exception>
        public int Quality
        {
            get => GetSensorSetting(SensorSetting.Quality);

            set
            {
                if (value < 0 || value > 63)
                {
                    throw new ArgumentOutOfRangeException();
                }

                SetSensorSetting(SensorSetting.Quality, value);
            }
        }

        /// <summary>
        /// Gets or sets the brightness, from -2 to 2.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>sensor_t.set_brightness</c>.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">The value is outside the -2 to 2 range.</exception>
        public int Brightness
        {
            get => GetSensorSetting(SensorSetting.Brightness);

            set
            {
                if (value < -2 || value > 2)
                {
                    throw new ArgumentOutOfRangeException();
                }

                SetSensorSetting(SensorSetting.Brightness, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the image is flipped vertically.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>sensor_t.set_vflip</c>.
        /// </remarks>
        public bool VFlip
        {
            get => GetSensorSetting(SensorSetting.VFlip) != 0;

            set => SetSensorSetting(SensorSetting.VFlip, value ? 1 : 0);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the image is mirrored horizontally.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>sensor_t.set_hmirror</c>.
        /// </remarks>
        public bool HMirror
        {
            get => GetSensorSetting(SensorSetting.HMirror) != 0;

            set => SetSensorSetting(SensorSetting.HMirror, value ? 1 : 0);
        }

        /// <summary>
        /// Creates a camera with the specified connection settings.
        /// </summary>
        /// <param name="settings">The connection settings of the camera.</param>
        /// <remarks>
        /// The camera hardware is not accessed until <see cref="Initialize"/> is called.
        /// If a thread is aborted while it's waiting in <see cref="TryCapture(out byte[])"/> or <see cref="TryCapture(byte[], out int)"/>,
        /// the following captures can return the image from a previous capture. To recover, dispose the camera, then create and initialize a new one.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="settings"/> is <see langword="null"/>.</exception>
        public Esp32Camera(CameraConnectionSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException();
            }

            // keep a copy so later changes to the caller's object don't affect the camera
            _connectionSettings = settings.Clone();

            // create the lock object
            _syncLock = new object();
        }

        /// <summary>
        /// Initializes the camera driver with the connection settings provided in the constructor.
        /// </summary>
        /// <returns><see cref="CameraInitResult.Success"/> if the camera was initialized, otherwise the reason for the failure.</returns>
        /// <remarks>
        /// Equivalent to <c>esp_camera_init</c>.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">The camera has been disposed.</exception>
        public CameraInitResult Initialize()
        {
            lock (_syncLock)
            {
                CheckIfDisposed();

                if (_initialized)
                {
                    return CameraInitResult.AlreadyInitialized;
                }

                CameraInitResult result = NativeInit();

                _initialized = result == CameraInitResult.Success;

                return result;
            }
        }

        /// <summary>
        /// Tries to capture a JPEG image into a new array.
        /// </summary>
        /// <param name="image">When this method returns <see langword="true"/>, the JPEG image, otherwise <see langword="null"/>.</param>
        /// <returns><see langword="true"/> if an image was captured, <see langword="false"/> if the driver didn't get a frame from the sensor.</returns>
        /// <remarks>
        /// Equivalent to <c>esp_camera_fb_get</c>.
        /// </remarks>
        /// <exception cref="ObjectDisposedException">The camera has been disposed.</exception>
        /// <exception cref="InvalidOperationException">The camera hasn't been initialized.</exception>
        public bool TryCapture(out byte[] image)
        {
            lock (_syncLock)
            {
                CheckIfInitialized();

                image = NativeCapture();

                return image != null;
            }
        }

        /// <summary>
        /// Tries to capture a JPEG image into the provided buffer.
        /// </summary>
        /// <param name="buffer">The buffer to store the image. It has to be large enough to hold the image.</param>
        /// <param name="length">When this method returns <see langword="true"/>, the number of bytes written to <paramref name="buffer"/>, otherwise 0.</param>
        /// <returns><see langword="true"/> if an image was captured, <see langword="false"/> if the driver didn't get a frame from the sensor.</returns>
        /// <remarks>
        /// Equivalent to <c>esp_camera_fb_get</c>.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="buffer"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="buffer"/> is too small for the image.</exception>
        /// <exception cref="ObjectDisposedException">The camera has been disposed.</exception>
        /// <exception cref="InvalidOperationException">The camera hasn't been initialized.</exception>
        public bool TryCapture(byte[] buffer, out int length)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException();
            }

            lock (_syncLock)
            {
                CheckIfInitialized();

                length = NativeCaptureToBuffer(buffer);

                return length > 0;
            }
        }

        private int GetSensorSetting(SensorSetting setting)
        {
            lock (_syncLock)
            {
                CheckIfInitialized();

                return NativeGetSensorSetting(setting);
            }
        }

        private void SetSensorSetting(SensorSetting setting, int value)
        {
            lock (_syncLock)
            {
                CheckIfInitialized();

                NativeSetSensorSetting(setting, value);
            }
        }

        private void CheckIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException();
            }
        }

        private void CheckIfInitialized()
        {
            CheckIfDisposed();

            if (!_initialized)
            {
                throw new InvalidOperationException();
            }
        }

        #region IDisposable Support

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // only the instance that initialized the camera can release it
                if (_initialized)
                {
                    NativeDispose();

                    _initialized = false;
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Releases the camera driver, frame buffers, GPIO pins and clock.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>esp_camera_deinit</c>.
        /// </remarks>
        public void Dispose()
        {
            lock (_syncLock)
            {
                if (!_disposed)
                {
                    Dispose(true);

                    GC.SuppressFinalize(this);
                }
            }
        }

#pragma warning disable 1591
        ~Esp32Camera()
        {
            Dispose(false);
        }
#pragma warning restore 1591

        #endregion

        #region external calls to native implementations

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern CameraInitResult NativeInit();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern byte[] NativeCapture();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int NativeCaptureToBuffer(byte[] buffer);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int NativeGetSensorId();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int NativeGetSensorSetting(SensorSetting setting);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeSetSensorSetting(SensorSetting setting, int value);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeDispose();

        #endregion
    }
}
