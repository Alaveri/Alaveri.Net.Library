using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Alaveri.Windows.Forms
{
    /// <summary>
    /// Represents a list of executable actions.
    /// </summary>
    public class ActionList: Component, IComponent, IDisposable
    {
        /// <summary>
        /// The form associated with this action list.
        /// </summary>
        protected Form? Form 
        { 
            get
            {
                var parent = GetType().GetProperty("Parent")?.GetValue(this) as Form;
                while (parent != null)
                {
                    if (parent is Form form)
                        return form;
                }
                return null;
            }
        }

        /// <summary>
        /// Updates the properties of all IActionAware controls with the properties of the control's Action property.
        /// </summary>
        public void UpdateProperties()
        {
            foreach (var action in Actions)
            {
                var control = Form?.Controls.Cast<Control>().OfType<IActionAware>().FirstOrDefault(item => item.Action == action);
                if (control != null)
                    action.BindProperties(control);
            }
        }

        /// <summary>Gets or sets the Actions.</summary>
        /// <value>The Actions.</value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(ActionCollectionEditor), typeof(UITypeEditor))]
        public ActionCollection Actions = new();

        /// <summary>Initializes a new instance of the <see cref="ActionList" /> class.</summary>
        public ActionList()
        {
        }

    }
}
