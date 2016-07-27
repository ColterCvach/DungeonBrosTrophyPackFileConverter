using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBrosTrophyPackFileConverter
{
    class FileConsumer
    {
        static Dictionary<int, TrophyInfo> _trophies = new Dictionary<int, TrophyInfo>(); 
        public static void ConsumeTSVFile(string readPath, string writePath)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(readPath);


            /*
             * The first line of the file contains the names of each language we are reading in.
             *  We are storing that for later so we can print out all languages at once and name the
             *  new files appropriately.
             * */
            line = file.ReadLine();
            List<LanguageIndexer> allLanguagesToWrite = new List<LanguageIndexer>(); 

            if(line!=null)
            {
                string[] languages = line.Split('\t');
                int count = 0; 
                for(int i=0; i < languages.Length;i++)
                {
                    if(languages[i].Length>0)
                    {
                        LanguageIndexer currentLanguage = new LanguageIndexer(languages[i], count + 1);
                        allLanguagesToWrite.Add(currentLanguage);
                        count++; 
                    }
                }
            }

            /*
             * Next, we need to read in the trophy data into objects
             * 
             * */

            while ((line = file.ReadLine()) != null)
            {
                string[] lineBits = line.Split('\t');
                string number = lineBits[0];
                string[] numberBits = number.Split('.');
                int id = Int32.Parse(numberBits[0]);
                TrophyInfo currentTrophy; 
                if (!_trophies.TryGetValue(id, out currentTrophy))
                {
                    currentTrophy = new TrophyInfo(id);
                    _trophies.Add(currentTrophy.ID, currentTrophy); 

                }

                if (numberBits.Length>1)
                {
                    // Not an Id
                    int actionId = Int32.Parse(numberBits[1]);
                    if(actionId==1)
                    {
                        currentTrophy.AddDetailsForAllLanguages(lineBits);
                    }
                }
                else
                {
                    // An Id
                    currentTrophy.AddNameForAllLanguages(lineBits);
                }
                _trophies[id] = currentTrophy;

            }
            file.Close();

            /*
             * TIME TO WRITE SOME FILES BABYYYYYYYYYY
             */

            foreach (LanguageIndexer indexer in allLanguagesToWrite)
            {
                string newFileName = writePath + indexer.Language + ".sfm";
                System.IO.StreamWriter newFile = new System.IO.StreamWriter(newFileName);

                newFile.WriteLine("<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
                newFile.WriteLine("<trophyconf version=\"1.1\" platform=\"ps4\" policy=\"large\">");
                newFile.WriteLine(" <title-name>Super Dungeon Bros.</title-name>");
                newFile.WriteLine(" <title-detail>Trophy set for Super Dungeon Bros.</title-detail>");


                for (int i=0; i < _trophies.Count;i++)
                {
                    TrophyInfo currentTrophy = _trophies[i];
                    string actualTrophyId = "" + currentTrophy.ID;
                    for(int j=actualTrophyId.Length;j<3;j++)
                    {
                        actualTrophyId = "0" + actualTrophyId; 
                    }
                    newFile.WriteLine(string.Format(" <trophy id=\"{0}\">", actualTrophyId));
                    newFile.WriteLine(string.Format("  <name>{0}</name>", currentTrophy.GetName(indexer.Index)));
                    newFile.WriteLine(string.Format("  <detail>{0}</detail>", currentTrophy.GetDetails(indexer.Index))); 
                    newFile.WriteLine(" </trophy>"); 
                }
                newFile.WriteLine("</trophyconf>");


                newFile.Close(); 
            }
        }
    }
}
