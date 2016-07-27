using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBrosTrophyPackFileConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Simba\\Desktop\\localized trophies.tsv";
            string writePath = "C:\\Users\\Simba\\Desktop\\NewXML's\\";
            FileConsumer.ConsumeTSVFile(filePath,writePath); 
        }
    }
}
