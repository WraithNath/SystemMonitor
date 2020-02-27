using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SadConsole.Controls;
using SadConsole.Themes;
using System;
using WraithNath.SystemMonitor.Windows;

namespace WraithNath.SystemMonitor
{
    /// <summary>
    /// Main Entry point
    /// </summary>
    class Program
    {
        #region Members

        public const int Width = 80;
        public const int Height = 25;
        private static TimeSpan _interval = TimeSpan.FromMilliseconds(500);

        #endregion Members

        #region Properties

        /// <summary>
        /// Gets or Sets the Interval
        /// </summary>
        public static TimeSpan Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                _interval = value;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Main Entry Point
        /// </summary>
        /// <param name="args">args</param>
        static void Main(string[] args)
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(Width, Height);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        /// <summary>
        /// Inits Sad Console
        /// </summary>
        private static void Init()
        {
            //Init Theme
            Colors colors = SadConsole.Themes.Library.Default.Colors;

            colors.ControlHostBack = Color.Black;
            colors.TitleText = Color.White;
            colors.Appearance_ControlNormal.Background = Color.Black;
            colors.Appearance_ControlNormal.Foreground = Color.White;

            ButtonTheme buttonTheme = new ButtonTheme();
            buttonTheme.EndCharacterLeft = '[';
            buttonTheme.EndCharacterRight = ']';

            Library.Default.SetControlTheme(typeof(Button), buttonTheme);
            Library.Default.WindowTheme.BorderLineStyle = SadConsole.CellSurface.ConnectedLineThin;

            var startingConsole = SadConsole.Global.CurrentScreen;

            CPUWindow cpu = new CPUWindow(Width, 3);
            cpu.IsVisible = true;

            MemoryWindow mem = new MemoryWindow(Width, 3);
            mem.Position = new Point(0, 3);
            mem.IsVisible = true;

            startingConsole.Children.Add(cpu);
            startingConsole.Children.Add(mem);
        }

        #endregion Methods
    }
}
