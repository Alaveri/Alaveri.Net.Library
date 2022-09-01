using Newtonsoft.Json;
using System.Windows.Forms;

namespace ACL.Versioning
{
    public static class VersionManager
    { 
        public static int Main()
        {
            try
            {
#if DEBUG
                var buildNumber = Convert.ToInt32(Environment.GetEnvironmentVariable("ACL.BuildNumber", EnvironmentVariableTarget.User) ?? "0");
                buildNumber++;
                Environment.SetEnvironmentVariable("ACL.BuildNumber", buildNumber.ToString(), EnvironmentVariableTarget.User);
#endif
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
    }
    public class VersionInformation
    {
        public int BuildNumber { get; set; } = 0;
    }


}
