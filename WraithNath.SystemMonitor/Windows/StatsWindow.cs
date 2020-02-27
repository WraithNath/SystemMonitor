using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WraithNath.SystemMonitor.Consoles;

namespace WraithNath.SystemMonitor.Windows
{
    /// <summary>
    /// Window for showing CPU info
    /// </summary>
    class StatsWindow : WindowBase
    {
        #region Members

        private StatsConsole _console = null;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Cosntructor
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        public StatsWindow(int width, int height) : base(width, height, "Status")
        {
            _console = new StatsConsole(width - 3, height - 2);
            _console.Position = new Point(2, 1);

            this.Children.Add(_console);
        }

        #endregion Constructor
    }
}
