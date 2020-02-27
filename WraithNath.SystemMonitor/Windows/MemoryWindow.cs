using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WraithNath.SystemMonitor.Consoles;

namespace WraithNath.SystemMonitor.Windows
{
    /// <summary>
    /// Window for showing Memory info
    /// </summary>
    class MemoryWindow : WindowBase
    {
        #region Members

        private MemoryConsole _console = null;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Cosntructor
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        public MemoryWindow(int width, int height) : base(width, height, "Memory")
        {
            _console = new MemoryConsole(width - 3, height - 2);
            _console.Position = new Point(2, 1);

            this.Children.Add(_console);
        }

        #endregion Constructor
    }
}
