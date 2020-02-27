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
    class CPUWindow : WindowBase
    {
        #region Members

        private CPUConsole _console = null;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Cosntructor
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        public CPUWindow(int width, int height) : base(width, height, "CPU")
        {
            _console = new CPUConsole(width - 3, height - 2);
            _console.Position = new Point(2, 1);

            this.Children.Add(_console);
        }

        #endregion Constructor
    }
}
