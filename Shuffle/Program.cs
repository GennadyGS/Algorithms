using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shuffle
{
    static class Program
    {
        static void Main(string[] args)
        {
            var playList = new PlayList()
            {
                Songs = new List<Song>
                {
                    new Song {Name = "Song 1" },
                    new Song {Name = "Song 2" },
                    new Song {Name = "Song 3" },
                    new Song {Name = "Song 4" },
                    new Song {Name = "Song 5" },
                    new Song {Name = "Song 6" },
                    new Song {Name = "Song 7" },
                    new Song {Name = "Song 8" },
                    new Song {Name = "Song 9" },
                    new Song {Name = "Song 10" }
                }
            };
            foreach(var song in playList.Shuffle())
            {
                Console.WriteLine(song);
            }
            Console.ReadLine();
        }
    }
}
