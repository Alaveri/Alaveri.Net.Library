using System.Text.RegularExpressions;

namespace ACL.Versioning
{
    public static class VersionManager
    { 
        public static int Main()
        {
            try
            {
#if DEBUG
                var dev = "-dev.";
                var build = Environment.GetEnvironmentVariable("ACL.BuildNumber", EnvironmentVariableTarget.User) ?? dev + "0";
                var buildNumber = Convert.ToInt32(Regex.Match(build, @"\d+").Value);
                buildNumber++;
                Environment.SetEnvironmentVariable("ACL.BuildNumber", dev + buildNumber.ToString(), EnvironmentVariableTarget.User);
#else
                Environment.SetEnvironmentVariable("ACL.BuildNumber", string.Empty, EnvironmentVariableTarget.User);
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
