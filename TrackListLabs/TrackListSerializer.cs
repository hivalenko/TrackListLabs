using System;
using System.Collections.Generic;
using System.IO;
namespace TrackListLabs
{
    public static class TrackListSerializer
    {
        public static void loadFromPls(String filePath, List<Track> trackList)
        {
            using(StreamReader sr = new StreamReader(filePath))
            {

                String line;
                int counter = 0;
                while (!sr.EndOfStream)
                {
                    String[] info;
                    try{
                        line = sr.ReadLine();
                        info = line.Split("=");
                        String codeName = info[0];
                        String path = info[1];

                        if (codeName.Split("_")[0] == "File")
                        {
                            String trackName = sr.ReadLine().Split("=")[1];
                            try
                            {
                                info = trackName.Split("-");
                                trackList.Add(new Track(info[0],info[1], path));   
                            }
                            catch (Exception)
                            {
                                trackList.Add(new Track("Unknown", trackName, path));
                            }
                            counter++;
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
        }

        public static void saveToPls(String filePath, List<Track> trackList)
        {

            using(StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("[playlist]");
                int counter = 1;
                foreach (Track track in trackList)
                {
                    sw.WriteLine("File" + "_" + counter + "=" + track.FilePath);
                    sw.WriteLine("Title" + "_" + counter + "=" + track.Author + "-" + track.Name);
                    sw.WriteLine();
                    counter++;
                }

                sw.WriteLine();
                sw.WriteLine("NumberOfEntries=" + counter);
                sw.WriteLine("Version=2");
            }
            
        }
    }
}