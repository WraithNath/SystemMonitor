using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WraithNath.SystemMonitor.Controls
{
    /// <summary>
    /// Console to display a progress bar
    /// </summary>
    class ProgressBar : SadConsole.Console
    {
        #region Members

        private float _percentage = 0f;
        private string _externalLabel = string.Empty;
        private string _internalLabel = string.Empty;
        private float? _peakPercent = null;
        private bool _showPeakIndicator = false;

        #endregion Members

        #region Properties

        /// <summary>
        /// Gets or Sets the External Label
        /// </summary>
        public string ExternalLabel
        {
            get
            {
                return _externalLabel;
            }
            set
            {
                _externalLabel = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Internal Label
        /// </summary>
        public string InternalLabel
        {
            get
            {
                return _internalLabel;
            }
            set
            {
                _internalLabel = value;
            }
        }

        /// <summary>
        /// Gets or Sets whether to show the peak indicator
        /// </summary>
        public bool ShowPeakIndicator
        {
            get
            {
                return _showPeakIndicator;
            }
            set
            {
                _showPeakIndicator = value;
            }
        }

        /// <summary>
        /// Gets or Sets the percentage
        /// </summary>
        public float Percentage
        {
            get
            {
                return _percentage;
            }
            set
            {
                _percentage = value;

                if (_peakPercent == null || _peakPercent < value)
                    _peakPercent = value;
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        public ProgressBar(int width, int height) : base(width, height)
        {
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Draws the Console
        /// </summary>
        /// <param name="timeElapsed">The elapsed time</param>
        public override void Draw(TimeSpan timeElapsed)
        {
            base.Draw(timeElapsed);

            int x = 0;
            int y = 0;

            if (this.ExternalLabel != string.Empty)
            {
                this.Print(x, y, this.ExternalLabel, Color.CadetBlue);
                x += this.ExternalLabel.Length;
                x += 1;
            }

            this.SetGlyph(x, y, '[', Color.White);

            x += 1;

            //Clear background
            this.DrawBox(new Rectangle(x, y, this.Width - 2, 1), new SadConsole.Cell(this.DefaultForeground, this.DefaultBackground));

            //Print Peak Percent
            if (this.ShowPeakIndicator && _peakPercent.HasValue)
            {
                float peak = (((float)this.Width / 100f) * _peakPercent.Value);
                this.Print((int)peak, y, ">", Color.Gray);
            }

            //Print the Internal Label
            if (this.InternalLabel != string.Empty)
                this.Print(this.Width - 1 - this.InternalLabel.Length, y, this.InternalLabel, Color.DimGray);

            float width = (((float)(this.Width - x - 1) / 100f) * this.Percentage);

            //Print Progress Bar
            if (width > 0)
                this.Print(x, y, string.Empty.PadRight((int)width, '|'), Color.MediumSeaGreen);

            this.SetGlyph(this.Width - 1, y, ']', Color.White);
        }

        #endregion Methods
    }
}
