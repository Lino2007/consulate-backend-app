using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace NSI.Common.Utilities
{
    public static class PathHelper
    {
        private static string GetBasePath()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var regex = new Regex(".+?(?=NSI.Service.Echo)");
            var match = regex.Match(path);

            return match.Value + "NSI.Service.Echo" + Path.DirectorySeparatorChar;
        }

        public static string GetPathFromBase(params string[] subPaths)
        {
            return GetBasePath() + Path.Combine(subPaths);
        }
    }
}
