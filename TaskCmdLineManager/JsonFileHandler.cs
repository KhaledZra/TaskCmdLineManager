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
        public string FileName { get; set; }

        public JsonFileHandler(string fileName)
        {
            this.FileName = fileName;
        }
    }
}
