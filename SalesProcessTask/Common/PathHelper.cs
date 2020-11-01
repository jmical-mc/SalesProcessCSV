using System;
using System.IO;

namespace SalesProcessTask.Common
{
    public static class PathHelper
    {
        public static string GetPathToFile(string fileName)
        {
            var projectDirPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\"));

            var filesPath = "Files";

            return Path.Combine(projectDirPath, filesPath, fileName);
        }
    }
}