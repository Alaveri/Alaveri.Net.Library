using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alaveri.Windows.Forms
{
    [Flags]
    public enum ActionOptions
    {
        BindText,
        BindShowShortcut,
        BindShortcutKeys,
        BindShortCutKeyDisplay,
        BindLongText,
        BindClick,
        BindEnabled,
        BindVisible
    }

    /// <summary>
    /// Represents a generic action that can be linked to controls.
    /// </summary>
    public class Action: Component
    {
        private string? _longText;

        //TODO: Implement enabled and visible

        /// <summary>Gets or sets the long text.</summary>
        /// <value>The long text.</value>
        public string? LongText 
        {
            get => _longText;
            set
            {
                _longText = value;

            }
        }

        /// <summary>Gets or sets the text.</summary>
        /// <value>The text.</value>
        public string? Text { get; set; }

        /// <summary>Gets or sets a value indicating whether to show shortcut keys.</summary>
        /// <value>
        ///   <c>true</c> if [show shortcut keys]; otherwise, <c>false</c>.</value>
        public bool ShowShortcutKeys { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether this <see cref="Action" /> is enabled.</summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; } = true;

        /// <summary>Gets or sets a value indicating whether this <see cref="Action" /> is visible.</summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; set; } = true;

        /// <summary>Gets or sets the shortcut key display string.</summary>
        /// <value>The shortcut key display string.</value>
        public string? ShortcutKeyDisplayString { get; set; }

        /// <summary>Gets or sets the action list assiciated with this action.</summary>
        /// <value>The action list.</value>
        public ActionList? ActionList { get; set; }

        /// <summary>
        ///  Gets or sets the shortcut keys associated with the action.
        /// </summary>
        [Localizable(true)]
        [DefaultValue(Keys.None)]       
        public Keys ShortcutKeys { get; set; }

        /// <summary>
        /// The ExecuteAction event can be bound to any EventHandler event for a control.
        /// </summary>
        public event EventHandler? ExecuteAction;

        /// <summary>
        /// Binds the Action properties to the control's properties.
        /// </summary>
        /// <param name="control"></param>
        public void BindProperties(IActionAware control)
        {
            if (control == null)
                return;
            var controlType = control.GetType();
            if (Text != null && control.ActionOptions.HasFlag(ActionOptions.BindText) && !control.ActionOptions.HasFlag(ActionOptions.BindLongText))
                controlType.GetProperty("Text")?.SetValue(control, Text);
            if (Text != null && !control.ActionOptions.HasFlag(ActionOptions.BindText) && control.ActionOptions.HasFlag(ActionOptions.BindLongText))
                controlType.GetProperty("LongText".GetType().Name)?.SetValue(control, LongText);
            if (control.ActionOptions.HasFlag(ActionOptions.BindShowShortcut))
                controlType.GetProperty("ShowShortcutKeys")?.SetValue(control, ShowShortcutKeys);
            if (control.ActionOptions.HasFlag(ActionOptions.BindShortcutKeys))
                controlType.GetProperty("ShortcutKeys")?.SetValue(control, ShortcutKeys);
            if (control.ActionOptions.HasFlag(ActionOptions.BindShortcutKeys))
                controlType.GetProperty("ShortcutKeyDisplayString")?.SetValue(control, ShortcutKeyDisplayString);
            if (control.ActionOptions.HasFlag(ActionOptions.BindEnabled))
                control.Enabled = Enabled;
            if (control.ActionOptions.HasFlag(ActionOptions.BindVisible))
                control.Visible = Visible;
            if (control.ActionOptions.HasFlag(ActionOptions.BindClick))
            {
                var click = controlType.GetProperty("Click");
                if (click != null)
                {
                    EventHandler? handler = (EventHandler?)click.GetValue(control);
                    handler -= Execute;
                    handler += Execute;
                }
            }
        }

        /// <summary>
        /// Executes this action.
        /// </summary>
        public void Execute(object? sender, EventArgs e)
        {
            ExecuteAction?.Invoke(this, e);
        }

        /// <summary>Executes this action.</summary>
        public void Execute()
        {
            ExecuteAction?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Initializes a new instance of the <see cref="Action" /> class.</summary>
        public Action()
        {
        }
    }
}
