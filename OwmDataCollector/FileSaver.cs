using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector
{
    class FileSaver
    {
        public static void SaveToFile(string data, string filePath)
        {
            FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            TextWriter writer = new StreamWriter(stream);
            writer.NewLine = "\n";
            writer.Write(data);
            writer.Close();
            stream.Close();
        }
    }
}
