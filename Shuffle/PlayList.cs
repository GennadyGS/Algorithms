using System;
using System.Collections.Generic;

namespace Shuffle
{
    public class PlayList
    {
        public List<Song> Songs { get; set; }

        public IEnumerable<Song> Shuffle()
        {
            if (Songs == null)
            {
                yield break;
            }
            Song[] songsCopy = Songs.ToArray();
            var random = new Random();
            for (int i = songsCopy.Length - 1; i >= 0; i--)
            {
                int j = random.Next(i + 1);
                yield return songsCopy[j];
                songsCopy[j] = songsCopy[i];
            }
        }
    }
}