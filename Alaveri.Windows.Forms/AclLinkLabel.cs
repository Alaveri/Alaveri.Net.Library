using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alaveri.Windows.Forms
{
    public class AclLinkLabel : LinkLabel, IActionAware
    {
        private Action? action;

        /// <summary>Gets or sets the action options.</summary>
        /// <value>The action options.</value>
        public ActionOptions ActionOptions { get; set; } = ActionOptions.BindText & ActionOptions.BindShowShortcut & ActionOptions.BindShortcutKeys & ActionOptions.BindShortCutKeyDisplay;

        /// <summary>Gets or sets the Action to associate with this item.</summary>
        /// <value>The action.</value>
        public Action? Action
        {
            get => action;
            set
            {
                if (value == null)
                    return;
                action = value;
                action.BindProperties(this);
            }
        }
    }
}
