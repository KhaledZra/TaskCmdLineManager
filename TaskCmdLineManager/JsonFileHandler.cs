using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskCmdLineManager
{
    internal class JsonFileHandler
    {
        private string _fileName;
        public string FileName
        {
            get { return _fileName;}
            set { _fileName = value;}
        }

        public JsonFileHandler(string fileName)
        {
            this.FileName = fileName;
        }
        public JsonFileHandler()
        {
            this.FileName = "Default";
        }

        public void ShowFile()
        {
            Console.WriteLine($"| File name: {FileName}");
        }
    }
}
