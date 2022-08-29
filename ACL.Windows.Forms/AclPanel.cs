using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACL.Windows.Forms
{
    /// <summary>
    /// Represents a customized AclPanel.
    /// </summary>
    public class AclPanel: Panel
    {
        /// <summary>
        /// The border color of the panel.
        /// </summary>
        public Color BorderColor { get; set; } = SystemColors.WindowFrame;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
        }
    }
}
