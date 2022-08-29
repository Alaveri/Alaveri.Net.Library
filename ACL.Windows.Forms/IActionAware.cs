using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACL.Windows.Forms
{
    public interface IActionAware
    {
        /// <summary>Gets or sets the Action to associate with this item.</summary>
        /// <value>The action.</value>
        Action? Action { get; set; }

        /// <summary>Gets or sets the action options.</summary>
        /// <value>The action options.</value>
        ActionOptions ActionOptions { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="IActionAware" /> is enabled.</summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool Enabled { get; set; }

        /// <summary>Gets or sets a value indicating whether this <see cref="IActionAware" /> is visible.</summary>
        /// <value>
        ///   <c>true</c> if visible; otherwise, <c>false</c>.</value>
        bool Visible { get; set; }

    }
}
