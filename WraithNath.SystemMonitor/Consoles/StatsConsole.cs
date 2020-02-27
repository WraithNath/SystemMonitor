using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WraithNath.SystemMonitor.Controls;

namespace WraithNath.SystemMonitor.Consoles
{
    /// <summary>
    /// Stats Console
    /// </summary>
    class StatsConsole : SadConsole.Console
    {
        #region Members

        private PerformanceCounter _uptimeCounter;
        private TimeSpan _delta = TimeSpan.Zero;
        private Field _uptimeField = null;
        private Field _hostnameField = null;
        private Field _ipAddressField = null;

        #endregion Members

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">The Console Width</param>
        /// <param name="height">The Console Height</param>
        public StatsConsole(int width, int height) : base(width, height)
        {
            _uptimeCounter = new PerformanceCounter("System", "System Up Time");
            _uptimeCounter.NextValue();

            _hostnameField = new Field(this.Width, 1, "Hostname");
            _hostnameField.Position = new Microsoft.Xna.Framework.Point(0, 0);
            _hostnameField.FieldValue = Dns.GetHostName();
            this.Children.Add(_hostnameField);

            _ipAddressField = new Field(this.Width, 1, "IP Address");
            _ipAddressField.Position = new Microsoft.Xna.Framework.Point(0, 1);
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                _ipAddressField.FieldValue = endPoint.Address.ToString();
            }
            
            this.Children.Add(_ipAddressField);

            _uptimeField = new Field(this.Width, 1, "Uptime");
            _uptimeField.Position = new Microsoft.Xna.Framework.Point(0, 2);
            this.Children.Add(_uptimeField);
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
                float uptime = _uptimeCounter.NextValue();

                _uptimeField.FieldValue = TimeSpan.FromSeconds(uptime).ToString(@"dd\.hh\:mm\:ss");
                
                _delta = TimeSpan.Zero;
            }

            base.Update(timeElapsed);
        }

        #endregion Methods
    }
}
