// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// Connection settings used to initialize the camera.
    /// </summary>
    /// <remarks>
    /// Equivalent to <c>camera_config_t</c>. Use -1 for pins that aren't connected.
    /// </remarks>
    public sealed class CameraConnectionSettings
    {
        private int _pinPowerDown = -1;
        private int _pinReset = -1;
        private int _pinXclk = -1;
        private int _pinSccbSda = -1;
        private int _pinSccbScl = -1;
        private int _pinD7 = -1;
        private int _pinD6 = -1;
        private int _pinD5 = -1;
        private int _pinD4 = -1;
        private int _pinD3 = -1;
        private int _pinD2 = -1;
        private int _pinD1 = -1;
        private int _pinD0 = -1;
        private int _pinVsync = -1;
        private int _pinHref = -1;
        private int _pinPclk = -1;
        private int _xclkFreqHz = 20_000_000;
        private FrameSize _frameSize = FrameSize.Vga;
        private int _jpegQuality = 12;
        private int _frameBufferCount = 1;
        private FrameBufferLocation _frameBufferLocation = FrameBufferLocation.Psram;
        private GrabMode _grabMode = GrabMode.WhenEmpty;
        private int _sccbI2cPort = -1;

        /// <summary>
        /// Gets or sets the GPIO connected to the power down line, or -1 if not connected.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_pwdn</c>.
        /// </remarks>
        public int PinPowerDown { get => _pinPowerDown; set => _pinPowerDown = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the reset line, or -1 to use a software reset.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_reset</c>.
        /// </remarks>
        public int PinReset { get => _pinReset; set => _pinReset = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the XCLK line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_xclk</c>.
        /// </remarks>
        public int PinXclk { get => _pinXclk; set => _pinXclk = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the SCCB SDA line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_sccb_sda</c>.
        /// Set this and <see cref="PinSccbScl"/> to -1 to use an I2C port that is already configured, set in <see cref="SccbI2cPort"/>.
        /// </remarks>
        public int PinSccbSda { get => _pinSccbSda; set => _pinSccbSda = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the SCCB SCL line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_sccb_scl</c>.
        /// </remarks>
        public int PinSccbScl { get => _pinSccbScl; set => _pinSccbScl = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the D7 line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_d7</c>.
        /// </remarks>
        public int PinD7 { get => _pinD7; set => _pinD7 = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the D6 line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_d6</c>.
        /// </remarks>
        public int PinD6 { get => _pinD6; set => _pinD6 = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the D5 line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_d5</c>.
        /// </remarks>
        public int PinD5 { get => _pinD5; set => _pinD5 = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the D4 line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_d4</c>.
        /// </remarks>
        public int PinD4 { get => _pinD4; set => _pinD4 = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the D3 line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_d3</c>.
        /// </remarks>
        public int PinD3 { get => _pinD3; set => _pinD3 = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the D2 line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_d2</c>.
        /// </remarks>
        public int PinD2 { get => _pinD2; set => _pinD2 = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the D1 line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_d1</c>.
        /// </remarks>
        public int PinD1 { get => _pinD1; set => _pinD1 = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the D0 line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_d0</c>.
        /// </remarks>
        public int PinD0 { get => _pinD0; set => _pinD0 = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the VSYNC line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_vsync</c>.
        /// </remarks>
        public int PinVsync { get => _pinVsync; set => _pinVsync = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the HREF line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_href</c>.
        /// </remarks>
        public int PinHref { get => _pinHref; set => _pinHref = value; }

        /// <summary>
        /// Gets or sets the GPIO connected to the PCLK line.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>pin_pclk</c>.
        /// </remarks>
        public int PinPclk { get => _pinPclk; set => _pinPclk = value; }

        /// <summary>
        /// Gets or sets the frequency of XCLK, in Hz.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>xclk_freq_hz</c>.
        /// </remarks>
        public int XclkFreqHz { get => _xclkFreqHz; set => _xclkFreqHz = value; }

        /// <summary>
        /// Gets or sets the frame size used to initialize the camera.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>frame_size</c>.
        /// The frame buffers are sized for this frame size, so this is the largest value that <see cref="Esp32Camera.FrameSize"/> can be set to.
        /// </remarks>
        public FrameSize FrameSize { get => _frameSize; set => _frameSize = value; }

        /// <summary>
        /// Gets or sets the JPEG quality used to initialize the camera, from 0 (highest quality) to 63 (lowest quality).
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>jpeg_quality</c>.
        /// </remarks>
        public int JpegQuality { get => _jpegQuality; set => _jpegQuality = value; }

        /// <summary>
        /// Gets or sets the number of frame buffers to allocate.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>fb_count</c>.
        /// </remarks>
        public int FrameBufferCount { get => _frameBufferCount; set => _frameBufferCount = value; }

        /// <summary>
        /// Gets or sets the memory where the frame buffers are allocated.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>fb_location</c>.
        /// </remarks>
        public FrameBufferLocation FrameBufferLocation { get => _frameBufferLocation; set => _frameBufferLocation = value; }

        /// <summary>
        /// Gets or sets how the driver fills the frame buffers.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>grab_mode</c>.
        /// </remarks>
        public GrabMode GrabMode { get => _grabMode; set => _grabMode = value; }

        /// <summary>
        /// Gets or sets the I2C port to use for SCCB when <see cref="PinSccbSda"/> and <see cref="PinSccbScl"/> are -1.
        /// </summary>
        /// <remarks>
        /// Equivalent to <c>sccb_i2c_port</c>.
        /// </remarks>
        public int SccbI2cPort { get => _sccbI2cPort; set => _sccbI2cPort = value; }

        internal CameraConnectionSettings Clone()
        {
            return (CameraConnectionSettings)MemberwiseClone();
        }
    }
}
