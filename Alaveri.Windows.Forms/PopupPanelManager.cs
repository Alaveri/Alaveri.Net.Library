using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaveri.Windows.Forms
{
    /// <summary>
    /// Manager for controlling display of popup panels.
    /// </summary>
    public class PopupPanelManager
    {
        /// <summary>
        /// The list of popup panels maintained by the PopupPanelManager.
        /// </summary>
        public IList<PopupPanel> PopupPanels { get; set; } = new List<PopupPanel>();

        /// <summary>
        /// Shows a popup panel and hides all other popup panels.
        /// </summary>
        /// <param name="panel">The panel to show.</param>
        public void ShowPopupPanel(Panel panel)
        {
            foreach (var popup in PopupPanels.Where(item => item.Panel.Name != panel.Name))
              popup.Panel.Hide();
            var thisPanel = PopupPanels.First(item => item.Panel == panel);
            var control = thisPanel.Panel;
            control.BringToFront();
            switch (thisPanel.Position)
            {
                case PopupPanelPosition.Center:
                    control.Left = (control.Parent.Width - control.Width) / 2;
                    control.Top = (control.Parent.Height - control.Height) / 2;
                    break;
            }
            thisPanel.Panel.Show();
        }

        /// <summary>
        /// Hides all popup panels.
        /// </summary>
        public void HideAllPopupPanels()
        {
            foreach (var panel in PopupPanels)
            {
                panel.Panel.Hide();
            }
        }

    }
}
