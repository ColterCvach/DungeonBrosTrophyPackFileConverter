using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBrosTrophyPackFileConverter
{
    class LanguageIndexer
    {
        string _language;
        int _index;

        public string Language { get { return _language; } }
        public int Index { get { return _index; } }

        public LanguageIndexer(string s, int i)
        {
            _language = s;
            _index = i; 
        }
    }
}
