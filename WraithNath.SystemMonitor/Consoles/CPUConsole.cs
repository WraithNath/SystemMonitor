using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using WraithNath.SystemMonitor.Controls;

namespace WraithNath.SystemMonitor.Consoles
{
    /// <summary>
    /// CPU Console
    /// </summary>
    class CPUConsole : SadConsole.Console
    {
        #region Members

        private PerformanceCounter _cpuCounter;
        private TimeSpan _delta = TimeSpan.Zero;
        private ProgressBar _cpuTotalProgressBar = null;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">The Console Width</param>
        /// <param name="height">The Console Height</param>
        public CPUConsole(int width, int height) : base(width, height)
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            _cpuTotalProgressBar = new ProgressBar(width-1, 1);

            this.Children.Add(_cpuTotalProgressBar);
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Updates the Console
        /// </summary>
        /// <param name="timeElapsed">The elapsed time</param>
        public override void Update(TimeSpan timeElapsed)
        {
            _delta += timeElapsed;

            if (_delta > Program.Interval)
            {
                float cpuTotal = _cpuCounter.NextValue();
                _delta = TimeSpan.Zero;

                _cpuTotalProgressBar.Percentage = cpuTotal;
                _cpuTotalProgressBar.InternalLabel = $"{cpuTotal:0.0}%";
            }

            base.Update(timeElapsed);
        }

        #endregion Methods
    }
}
