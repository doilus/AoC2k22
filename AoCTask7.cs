using System;
using System.Data.SqlTypes;
using System.Reflection;

namespace AocTasks
{
    public class AoCTask7
    {
        const int MAX = 100000;
        const int FREE = 30000000;
        const int TOTAL_MEMORY = 70000000;

        public void Resolve()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Inputs/commands.txt");
            string[] lines = File.ReadAllLines(path);

            Dictionary<string, List<string>> catalogs = new Dictionary<string, List<string>>();
            Dictionary<string, int> catalogsValue = new Dictionary<string, int>();
            List<string> usedCatalogs = new List<string>();

            List<string> catalogsPath = new List<string>();
            string currentCatalog = "";
            string combinedPath = "";

            foreach (string line in lines)
            {
                if (line.Contains("$ cd .."))
                {
                    catalogsPath.RemoveAt(catalogsPath.Count - 1);
                    currentCatalog = catalogsPath.Last();
                    continue;
                }

                if (line.Contains("$ cd "))
                {
                    currentCatalog = line.Substring(5, line.Length - 5);
                    catalogsPath.Add(currentCatalog);
                    combinedPath = string.Join("/", catalogsPath.ToArray());

                    if (!catalogs.ContainsKey(currentCatalog))
                        catalogs.Add(combinedPath, new List<string>());
                    continue;
                }

                if (!line.Contains('$'))
                {
                    catalogs[combinedPath].Add(line);
                }
            }

            while (catalogsValue.Count() < catalogs.Count())
            {
                foreach (KeyValuePair<string, List<string>> catalog in catalogs)
                {
                    int amount = 0;
                    bool isNotCountedCatalog = false;

                    foreach (string s in catalog.Value)
                    {
                        if (s.Contains("dir"))
                        {
                            string dir = s.Substring(4, s.Length - 4);
                            string key = catalog.Key + "/" + dir;

                            if (catalogsValue.ContainsKey(key))
                            {
                                amount += catalogsValue[key];
                            }
                            else
                            {
                                isNotCountedCatalog = true;
                            }
                        }

                        if (!s.Contains("dir "))
                        {
                            string[] value = s.Split(' ');
                            amount += Int32.Parse(value[0]);
                        }
                    }

                    if (!isNotCountedCatalog && !catalogsValue.ContainsKey(catalog.Key))
                    {
                        catalogsValue.Add(catalog.Key, amount);
                    }
                }
            }

            long count = 0;
            int freeMemory = FREE - (TOTAL_MEMORY - catalogsValue["/"]);

            List<int> lessThanFree = new List<int>();
            //count all values
            foreach (KeyValuePair<string, int> value in catalogsValue)
            {
                if (value.Value <= MAX)
                {
                    count += value.Value;
                }

                if (value.Value >= freeMemory)
                {
                    lessThanFree.Add(value.Value);
                }
            }

            Console.WriteLine("Total value: " + count);
            Console.WriteLine("Min to delete: " + lessThanFree.Min());
        }
    }
}

