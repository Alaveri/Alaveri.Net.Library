using System.Text;
using System.Xml.Serialization;

namespace ACL.Configuration
{
    public interface IAppConfiguration
    {
        string AppName { get; }
        string ConfigurationExtension { get; }
        string ConfigurationFilename { get; }
        string ConfigurationPath { get; }
        Encoding Encoding { get; }
        AppConfigurationFormat Format { get; }
        string OrganizationName { get; }
        string? XmlDefaultNamespace { get; }
        IEnumerable<Type>? XmlExtraTypes { get; }
        XmlSerializerNamespaces? XmlNamespaces { get; }
    }
}