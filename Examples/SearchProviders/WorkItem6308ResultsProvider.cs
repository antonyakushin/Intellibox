/*
Copyright (c) 2010 Stephen P Ward and Joseph E Feser

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the "Software"), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections;
using System.Linq;
using FeserWard.Controls;

namespace Examples.SearchProviders
{
    public class WorkItem6308ResultsProvider : IIntelliboxResultsProvider
    {
        public class AlphabetCharacter : IEquatable<AlphabetCharacter>
        {
            private static int lowerAVal = Convert.ToInt32('a');
            private static int upperAVal = Convert.ToInt32('A');

            public char Uppercase { get; private set; }
            public char Lowercase { get; private set; }

            public AlphabetCharacter(int charnum)
            {
                Uppercase = Convert.ToChar((charnum % 26) + upperAVal);
                Lowercase = Convert.ToChar((charnum % 26) + lowerAVal);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as AlphabetCharacter);
            }

            public bool Equals(AlphabetCharacter other)
            {
                if (other == null)
                    return false;

                return other.Uppercase == this.Uppercase && other.Lowercase == this.Lowercase;
            }

            public override int GetHashCode()
            {
                return Uppercase.GetHashCode();
            }

            public override string ToString()
            {
                return Uppercase + " " + Lowercase;
            }

            public static bool operator ==(AlphabetCharacter a, AlphabetCharacter b)
            {
                if (System.Object.ReferenceEquals(a, b))
                    return true;

                if ((object)a == null && (object)b == null)
                    return true;

                if ((object)a == null)
                    return false;

                return a.Equals(b);
            }

            public static bool operator !=(AlphabetCharacter a, AlphabetCharacter b)
            {
                return !(a == b);
            }
            
        }

        public IEnumerable DoSearch(string searchTerm, int maxResults, object extraInfo)
        {
            // returns a list of all the upper and lower case letters of the alphabet
            return Enumerable.Range(0, 26).Select(i =>
            {
                return new AlphabetCharacter(i);
            });
        }
    }
}
