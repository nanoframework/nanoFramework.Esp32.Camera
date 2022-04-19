using System;
using System.Runtime.CompilerServices;

namespace NanoFramework.Camera
{
    public class Camera : IDisposable
    {
        // this is used as the lock object 
        // a lock is required because multiple threads can access the device
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly object _syncLock;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private readonly CameraConnectionSettings _cameraConnectionSettings;

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private bool _disposed;

        /// <summary>
        /// The connection settings of a device. The connection settings are immutable after the device is created
        /// so the object returned will be a clone of the settings object.
        /// </summary>
        public CameraConnectionSettings ConnectionSettings { get => _cameraConnectionSettings; }

        public byte[] GrabJPEG()
        {
            lock (_syncLock)
            {
                return NativeGrabJPEG();
            }
        }

        /// <summary>
        /// Create the Camera Device
        /// </summary>
        /// <param name="settings">Camera Connection settings</param>
        public Camera(CameraConnectionSettings settings)
        {
            _cameraConnectionSettings = settings;

            // create the lock object
            _syncLock = new object();

            // call native init to allow HAL/PAL inits related with I2C hardware
            NativeInit();
        }

        #region IDisposable Support
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                NativeDispose();

                _disposed = true;
            }
        }

        ~Camera()
        {
            Dispose(false);
        }

        /// <summary>
        /// <inheritdoc cref="IDisposable.Dispose"/>
        /// </summary>
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

        #endregion

        #region external calls to native implementations

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeInit();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern byte[] NativeGrabJPEG();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeDispose();

        #endregion
    }
}
