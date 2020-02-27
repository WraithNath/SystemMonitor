using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using WraithNath.SystemMonitor.Controls;

namespace WraithNath.SystemMonitor.Consoles
{
    /// <summary>
    /// Memory Console
    /// </summary>
    class MemoryConsole : SadConsole.Console
    {
        #region Members

        private PerformanceCounter _memPercentCounter;
        private PerformanceCounter _memAvailableCounter;
        private long _physicalMemory = 0;
        private TimeSpan _delta = TimeSpan.Zero;
        private ProgressBar _memUsageProgressBar = null;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">The Console Width</param>
        /// <param name="height">The Console Height</param>
        public MemoryConsole(int width, int height) : base(width, height)
        {
            _memPercentCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            _memAvailableCounter = new PerformanceCounter("Memory", "Available Bytes");

            GetPhysicallyInstalledSystemMemory(out _physicalMemory);

            _memUsageProgressBar = new ProgressBar(width - 1, 1);

            this.Children.Add(_memUsageProgressBar);
        }

        #endregion Constructor

        #region Methods

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        /// <summary>
        /// Updates the Console
        /// </summary>
        /// <param name="timeElapsed">The elapsed time</param>
        public override void Update(TimeSpan timeElapsed)
        {
            _delta += timeElapsed;

            if (_delta > Program.Interval)
            {
                float memPercent = _memPercentCounter.NextValue();
                float memAvailable = (_physicalMemory / 1024 / 2014) * 2;
                float memCommitted = _memAvailableCounter.NextValue() / 1024 / 1024 / 1024;

                _memUsageProgressBar.Percentage = memPercent;
                _memUsageProgressBar.InternalLabel = $"{memAvailable - memCommitted:0.0}G/{memAvailable:0.0}G";

                _delta = TimeSpan.Zero;
            }

            base.Update(timeElapsed);
        }

        #endregion Methods
    }
}
