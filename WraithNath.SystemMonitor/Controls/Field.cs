using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WraithNath.SystemMonitor.Controls
{
    class Field : SadConsole.Console
    {
        private string _labelText = string.Empty;
        private string _fieldValue = string.Empty;

        /// <summary>
        /// Gets or Sets the Label Text
        /// </summary>
        public string LabelText
        {
            get
            {
                return _labelText;
            }
            set
            {
                _labelText = value;
            }
        }

        public string FieldValue
        {
            get
            {
                return _fieldValue;
            }
            set
            {
                _fieldValue = value;
            }
        }

        public Field(int width, int height, string labelText="") : base(width, height)
        {
            this.LabelText = labelText;
        }

        public override void Draw(TimeSpan timeElapsed)
        {
            base.Draw(timeElapsed);

            this.Clear(0, 0, this.Width);
            this.Print(0, 0, $"{this.LabelText}:", Color.CadetBlue);
            this.Print(this.LabelText.Length + 2, 0, this.FieldValue, Color.White);
        }
    }
}
