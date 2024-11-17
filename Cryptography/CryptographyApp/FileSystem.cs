using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyApp
{
    public static class FileSystem
    {
        private static readonly string _currentDirectory = GetCurrentDir();

        public static string CurrentDirectory => _currentDirectory;

        public static string GetCurrentDir()
        {
            string relativePath = Assembly.GetExecutingAssembly().Location;
            List<string> filePathList = relativePath.Split(@"\").ToList();
            filePathList.RemoveAll(x => filePathList.IndexOf(x) > filePathList.IndexOf("CryptographyApp"));
            StringBuilder sb = new();
            filePathList.ForEach(x => sb.Append(@$"{x}\"));
            return sb.ToString();
        }
    }
}
