using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using ResoniteModLoader;

namespace BlockX
{
    public class ListUtil
    {
        private List<string> _cleanList;

        public void RefreshList(string listUrl)
        {
            bool success = GetList(listUrl);

            string blPath = BlFilePathGenerator();

            if (success)
            {
                if (File.Exists(blPath))
                    File.Delete(blPath);
                
                UpdateLocalBlCache(blPath);
            }
            else
            {
                if (!File.Exists(blPath))
                {
                    ResoniteMod.Error("Could not fetch BlockList and no locally cached version");
                    return;
                }

                _cleanList.Clear();
                using (StreamReader reader = new StreamReader(blPath))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        _cleanList.Add(line);
                    }
                }
            }
            
            ResoniteMod.Msg($"List refreshed, {_cleanList.Count} rules loaded.");
        }

        public bool CheckIfBlocked(string matcher)
        {
            return _cleanList.Any(matcher.Contains);
        }

        private void UpdateLocalBlCache(string blPath)
        {
            using (TextWriter writer = new StreamWriter(blPath))
            {
                foreach (string line in _cleanList)
                {
                    writer.Write(line + Environment.NewLine);
                }
            }
        }

        private string BlFilePathGenerator()
        {
            string assemblyDirectory =
                Path.GetDirectoryName(
                    Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            if (assemblyDirectory != null)
            {
                string path = Path.Combine(assemblyDirectory, "blockXBL.txt");

                return path;
            }

            return null;
        }
        
        private bool GetList(string listUrl)
        {
            WebClient client = new WebClient();

            using (Stream stream = client.OpenRead(listUrl))
                if (stream != null)
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string line;
                        var validLines = new List<string>();

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (!line.TrimStart().StartsWith("#") && !string.IsNullOrEmpty(line.TrimStart()))
                                validLines.Add(line);
                        }

                        _cleanList = validLines;
                        return true;
                    }

            return false;
        }
    }
}