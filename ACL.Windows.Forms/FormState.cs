using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ACL.Windows.Forms
{
    /// <summary>
    /// Represents the state of a Form, including position, location and WindowState.
    /// </summary>
    public class FormState
    {
        /// <summary>
        /// The form linked to this state.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public Form? Form { get; set; }

        /// <summary>
        /// The location of the form.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// The size of the form.
        /// </summary>
        public Size Size { get; set; }
        
        /// <summary>
        /// The form's window state.
        /// </summary>
        public FormWindowState WindowState { get; set; }

        /// <summary>
        /// True if this FormWindowState has been initialized.
        /// </summary>
        public bool Initialized { get; set; }

        /// <summary>
        /// Populates the FormState's properties from the Form's properties.
        /// </summary>
        public void PopulateFromForm()
        {
            if (Form == null)
                return;
            Initialized = true;
            if (Form.WindowState != FormWindowState.Minimized)
            {
                Size = Form.Size;
                Location = Form.Location;
                WindowState = Form.WindowState;
            }
        }

        /// <summary>
        /// Sets the form's properties from the FormState's properties.
        /// </summary>
        public void SetFormState()
        {
            if (Form == null)
                throw new InvalidOperationException("Form property is not set.");
            if (!Initialized)
                Form.StartPosition = FormStartPosition.WindowsDefaultLocation;
            else
                Form.Location = Location;
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            Form.WindowState = WindowState;
            Form.Size = Size;
        }

        /// <summary>
        /// Initializes a new instance of the FormState class using the specified form.
        /// </summary>
        /// <param name="form">The form to link to this state.</param>
        public FormState(Form form)
        {
            Form = form;
        }

        public FormState()
        {
        }
    }
}
