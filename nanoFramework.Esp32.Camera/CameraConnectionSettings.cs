using System;

namespace nanoFramework.Esp32.Camera
{
    /// <summary>
    /// Connection Settings for the Camera Module...
    /// </summary>
    public class CameraConnectionSettings
    {
        /// <summary>
        /// GPIO pin for camera power down line set to -1 if not required...
        /// </summary>
        public int CAM_PIN_PWDN { get; set; } = -1;
        /// <summary>
        /// GPIO pin for camera reset line set to -1 if software reset will be performed
        /// </summary>
        public int CAM_PIN_RESET { get; set; } = -1;
        /// <summary>
        /// GPIO pin for camera XCLK line
        /// </summary>
        public int CAM_PIN_XCLK { get; set; } = 21;
        /// <summary>
        /// GPIO pin for camera SDA line
        /// </summary>
        public int CAM_PIN_SIOD { get; set; } = 26;
        /// <summary>
        /// GPIO pin for camera SCL line
        /// </summary>
        public int CAM_PIN_SIOC { get; set; } = 27;
        /// <summary>
        /// GPIO pin for camera D7 line
        /// </summary>
        public int CAM_PIN_D7 { get; set; } = 35;
        /// <summary>
        /// GPIO pin for camera D6 line
        /// </summary>
        public int CAM_PIN_D6 { get; set; } = 34;
        /// <summary>
        /// GPIO pin for camera D5 line
        /// </summary>
        public int CAM_PIN_D5 { get; set; } = 39;
        /// <summary>
        /// GPIO pin for camera D4 line
        /// </summary>
        public int CAM_PIN_D4 { get; set; } = 36;
        /// <summary>
        /// GPIO pin for camera D3 line
        /// </summary>
        public int CAM_PIN_D3 { get; set; } = 19;
        /// <summary>
        /// GPIO pin for camera D2 line
        /// </summary>
        public int CAM_PIN_D2 { get; set; } = 18;
        /// <summary>
        /// GPIO pin for camera D1 line
        /// </summary>
        public int CAM_PIN_D1 { get; set; } = 5;
        /// <summary>
        /// GPIO pin for camera D0 line
        /// </summary>
        public int CAM_PIN_D0 { get; set; } = 4;
        /// <summary>
        /// GPIO pin for camera VSYNC line
        /// </summary>
        public int CAM_PIN_VSYNC { get; set; } = 25;
        /// <summary>
        /// GPIO pin for camera HREF line
        /// </summary>
        public int CAM_PIN_HREF { get; set; } = 23;
        /// <summary>
        /// GPIO pin for camera PCLK line
        /// </summary>
        public int CAM_PIN_PCLK { get; set; } = 22;
    }
}
