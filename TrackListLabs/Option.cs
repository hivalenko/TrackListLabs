using System;

namespace TrackListLabs
{
    public class Option
    {
        public string Name { get; private set; }
        public Action Callback { get; private set; }

        public Option(string name, Action callback)
        {
            Name = name;
            Callback = callback;
        }
    }
}