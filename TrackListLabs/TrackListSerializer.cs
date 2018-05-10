using System;
using System.Collections.Generic;
using System.IO;
namespace TrackListLabs
{
    public static class TrackListSerializer
    {
        public static void loadFromPls(String filePath, List<Track> trackList)
        {
            StreamReader sr = new StreamReader(filePath);

            String line;
            int counter = 0;
            try
            {
                while (!sr.EndOfStream)
                {
                    String[] info;
                    try
                    {
                        line = sr.ReadLine();
                        info = line.Split("=");
                        String codeName = info[0];
                        String author = info[1];
                        
                        if (codeName.Split("_")[0] == "File")
                        {
                            String trackName = sr.ReadLine().Split("=")[1];
                            trackList.Add(new Track("Unknown",trackName, author));
                            counter++;
                        }

                    }
                    catch
                    {

                    }
                }
                sr.Close();
            }
            catch
            {
                throw new InvalidInputException("Incorrect information in file");
            }
        }

        public static void saveToPls(String filePath, List<Track> trackList)
        {
            
            StreamWriter sw = new StreamWriter(filePath);
            try
            {
                
                sw.WriteLine("[playlist]");
                int counter = 1;
                foreach (Track track in trackList)
                {
                    sw.WriteLine("File" + counter + "=" + track.FilePath);
                    sw.WriteLine("Title" + counter + "=" + track.Name);
                    sw.WriteLine();
                    counter++;
                }

                sw.WriteLine();
                sw.WriteLine("NumberOfEntries=" + counter);
                sw.WriteLine("Version=2");
            }
            catch (Exception)
            {
                throw new InvalidInputException("Incorrect filePath!");
            }
            finally
            {
                sw.Close();
            }
        }
    }
}