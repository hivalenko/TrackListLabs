using System;

namespace TrackListLabs
{
    public class Track
    {
        public String Author { get; private set; }
        public String Name { get; private set; }

        public Track(String author, String name)
        {
            Author = author;
            Name = name;
        }

        public bool Includes(String information, String separator)
        {
            if (Author.Contains(information)
                || Name.Contains(information)
                || (Author + separator + Name).Contains(information))
            {
                return true;
            }

            return false;
            
        }
    }
}