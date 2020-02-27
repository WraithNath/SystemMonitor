using Microsoft.Xna.Framework;
using SadConsole.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace WraithNath.SystemMonitor.Windows
{
    /// <summary>
    /// Base Class for Windows
    /// </summary>
    class WindowBase : SadConsole.Window
    {
        #region Members

        private Button _closeButton = null;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        /// <param name="title">The Window Title</param>
        public WindowBase(int width, int height, string title = "") : base(width, height)
        {
            if (!String.IsNullOrWhiteSpace(title))
                this.Title = $" {title} ";

            this.CanDrag = true;

            _closeButton = new Button(3, 1);
            _closeButton.Text = "X";
            _closeButton.Click += _closeButton_Click;
            _closeButton.Position = new Point(this.Width - _closeButton.Width - 2, 0);

            this.Add(_closeButton);
        }

        #endregion Constructor

        #region Events

        /// <summary>
        /// Hides the Window
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">args</param>
        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        #endregion Events
    }
}
