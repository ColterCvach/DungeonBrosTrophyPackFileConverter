using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBrosTrophyPackFileConverter
{
    class TrophyInfo
    {
        int _id;
        public int ID { get { return _id; } }
        Dictionary<int, string> nameInAllLanguages = new Dictionary<int, string>();
        Dictionary<int, string> detailsInAllLanguages = new Dictionary<int, string>(); 

        public TrophyInfo(int id)
        {
            this._id = id; 
        }

        public void AddNameForAllLanguages(string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                nameInAllLanguages.Add(i, names[i]); 
            }

        }

        public void AddDetailsForAllLanguages(string[] details)
        {
            for (int i = 0; i < details.Length; i++)
            {
                detailsInAllLanguages.Add(i, details[i]); 
            }
        }

        public string GetName(int index)
        {
            return nameInAllLanguages[index];
        }

        public string GetDetails(int index)
        {
            return detailsInAllLanguages[index];
        }
        public void PrintDebugTrohpy()
        {
            Console.WriteLine(string.Format("{0}, {1}", ID, detailsInAllLanguages[1]));
        }
    }
}
