using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Alaveri.Windows.Forms
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
        /// The normal size of the form.
        /// </summary>
        public Size NormalSize { get; set; }

        /// <summary>
        /// The normal location of the form.
        /// </summary>
        public Point NormalLocation { get; set; }

        /// <summary>
        /// The form's window state.
        /// </summary>
        public FormWindowState WindowState { get; set; }

        /// <summary>
        /// True if this FormWindowState has been initialized.
        /// </summary>
        public bool Initialized { get; set; }

        /// <summary>
        /// Stores the Form's state.
        /// </summary>
        public void StoreFormState()
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
        /// Restores the Form's state.
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
