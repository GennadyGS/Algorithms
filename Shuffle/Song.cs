using System;

namespace Shuffle
{
    public class Song
    {
        public String Name { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}