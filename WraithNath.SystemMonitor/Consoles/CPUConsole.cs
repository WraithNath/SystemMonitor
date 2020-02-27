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

        private PerformanceCounter _allCpuCounter;
        private PerformanceCounterCategory _allCPUCategory;
        private TimeSpan _delta = TimeSpan.Zero;
        private Dictionary<string, CounterSample> _counterSamples = null;
        private Dictionary<string, ProgressBar> _progressBars = null;
        private string[] _instances = null;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">The Console Width</param>
        /// <param name="height">The Console Height</param>
        public CPUConsole(int width, int height) : base(width, height)
        {
            _allCpuCounter = new PerformanceCounter("Processor Information", "% Processor Time");
            _allCPUCategory = new PerformanceCounterCategory("Processor Information");

            _instances = _allCPUCategory.GetInstanceNames();

            _counterSamples = new Dictionary<string, CounterSample>();
            _progressBars = new Dictionary<string, ProgressBar>();

            int postition = 0;
            foreach (string instance in _instances)
            {
                _allCpuCounter.InstanceName = instance;
                _counterSamples.Add(instance, _allCpuCounter.NextSample());

                ProgressBar progressBar = new ProgressBar(width - 1, 1);
                progressBar.Position = new Point(0, postition);
                progressBar.ExternalLabel = instance;

                _progressBars.Add(instance, progressBar);
                this.Children.Add(progressBar);

                postition++;
            }
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
                foreach (string instance in _instances)
                {
                    _allCpuCounter.InstanceName = instance;

                    float pc = Calculate(_counterSamples[instance], _allCpuCounter.NextSample());

                    _progressBars[instance].Percentage = pc;
                    _progressBars[instance].InternalLabel = $"{pc:0.0}%";

                    _counterSamples[instance] = _allCpuCounter.NextSample();
                }

                _delta = TimeSpan.Zero;
            }

            base.Update(timeElapsed);
        }

        private float Calculate(CounterSample oldSample, CounterSample newSample)
        {
            float difference = newSample.RawValue - oldSample.RawValue;
            float timeInterval = newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec;
            if (timeInterval != 0) return 100 * (1 - (difference / timeInterval));
            return 0;
        }

        #endregion Methods
    }
}
