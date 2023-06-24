using Newtonsoft.Json;
using System.DirectoryServices.ActiveDirectory;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Alaveri.Windows.Forms;

/// <summary>
/// Represents the state of a form, including position, size and WindowState.
/// </summary>
public class StoredFormState
{
    /// <summary>
    /// The form linked to this state.
    /// </summary>
    [JsonIgnore]
    [XmlIgnore]
    public Form Form { get; set; }

    /// <summary>
    /// The location of the form.
    /// </summary>
    public Point Location { get; set; } = new Point(int.MaxValue, int.MaxValue);

    /// <summary>
    /// The size of the form.
    /// </summary>
    public Size Size { get; set; }

    /// <summary>
    /// The location of the form when not maximized.
    /// </summary>
    public Point RestoredLocation { get; set; }

    /// <summary>
    /// The size of the form.
    /// </summary>
    public Size RestoredSize { get; set; }

    /// <summary>
    /// The form's window state.
    /// </summary>
    public FormWindowState WindowState { get; set; }

    /// <summary>
    /// True if this state has been initialized.
    /// </summary>
    public bool Initialized { get; set; }

    /// <summary>
    /// Stores the form's state.
    /// </summary>
    public void StoreFormState()
    {
        if (Form.WindowState != FormWindowState.Minimized)
        {
            Location = Form.Location;
            Size = Form.Size;
            switch (Form.WindowState)
            {
                case FormWindowState.Normal:
                    RestoredLocation = Form.Location;
                    RestoredSize = Form.Size;
                    WindowState = FormWindowState.Normal;
                    break;
                case FormWindowState.Maximized:
                    WindowState = FormWindowState.Maximized;
                    break;
                case FormWindowState.Minimized:
                    RestoredLocation = Form.Location;
                    RestoredSize = Form.Size;
                    WindowState = FormWindowState.Normal;
                    break;
            }
        }
    }

    /// <summary>
    /// Restrores the form's state.
    /// </summary>
    public void RestoreFormState()
    {
        if (Location.X != int.MaxValue && Location.Y != int.MaxValue)
        {
            Form.Location = Location;
        }
        Form.Size = Size;
        if (WindowState == FormWindowState.Maximized)
        {
            Form.Location = RestoredLocation;
            Form.Size = RestoredSize;
            Form.WindowState = FormWindowState.Maximized;
        }
    }

    /// <summary>
    /// Initializes a new instance of the StoredFormState class using the specified Form and initial size.
    /// </summary>
    /// <param name="form">The form to link to this state.</param>
    public StoredFormState(Form form, Size initialSize = default)
    {
        Form = form;
        Size = initialSize;
    }

    /// <summary>
    /// Init/ializes a new instance of the WindowState class.
    /// </summary>
    public StoredFormState()
    {
    }
}
