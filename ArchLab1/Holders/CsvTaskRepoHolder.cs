using System.Configuration;
using System.Collections.Specialized;
using ArchLab1.Model;
using ArchLab1Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvHelper.Configuration;
using System.IO;

namespace ArchLab1.Holders
{
    internal class CsvTaskRepoHolder
    {
        private static readonly Lazy<CsvRepository<TaskEntity, long>> _instance =
            new Lazy<CsvRepository<TaskEntity, long>>(createInstance);
        public static CsvRepository<TaskEntity, long> GetInstance => _instance.Value;
        

        private static CsvRepository<TaskEntity, long> createInstance()
        {
            string path = (string)(ConfigurationManager.AppSettings.Get("csvUri") ?? throw new Exception());
            Console.WriteLine(Path.GetExtension(path));
            if (!Path.GetExtension(path).Equals(".csv")) throw new Exception("not csv");
            return new CsvRepository<TaskEntity, long>(path);
        }
    }
}
