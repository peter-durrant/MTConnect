using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Hdd.Utility
{
    public static class AssemblyHelpers
    {
        public static string AssemblyDirectory(Assembly executingAssembly, params string[] paths)
        {
            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            var codeBase = executingAssembly.CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var directoryName = Path.GetDirectoryName(path);
            return Path.Combine(paths.Prepend(directoryName).ToArray());
        }
    }
}