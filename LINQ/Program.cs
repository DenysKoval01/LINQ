using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;

namespace LINQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Write a lambda expression that will return the next number after the provided integer
            Console.WriteLine("==========1==========");
            int inputNumber = 10;
            Func<int, int> getNextNumber = x => x + 1;
            int nextNumber = getNextNumber(inputNumber);
            Console.WriteLine($"The next number after {inputNumber} is {nextNumber}.");


            //Using LINQ query syntax, write a method that will take IEnumerable<string> values and a string pattern,
            //filters the values that contain the pattern, and return the sorted result.

            IEnumerable<string> testedString = new[] { "cat", "dog", "animal", "car", "house","hr","one","care" };
            string pattern = "ca";
            IEnumerable<string> foundWordsLINQ = GetWordsLINQ(testedString, pattern);
            Console.WriteLine("==========2==========");
            Console.WriteLine("Words with pattern using LINQ : ");
            foreach (string word in foundWordsLINQ){Console.WriteLine(word);}



            //Do the same thing, but now using method syntax.
            Console.WriteLine("==========3==========");
            IEnumerable<string> foundWords = GetWordsMethod(testedString, pattern);
            Console.WriteLine("Words with pattern : ");
            foreach (string word in foundWords) {Console.WriteLine(word);}

            //Write a method that takes a collection of elements, and returns the 3rd, 4th, and 5th items of the provided sequence.
            Console.WriteLine("==========4==========");
            GetItems(testedString);

            //Write a method that returns all words in the sequence between "start" (inclusive) and "end" (non-inclusive). For example, if given { "One", "start", "more", "end", "thing" }
            //... this method should return { "start", "more" }
            Console.WriteLine("==========5==========");
            List<string> testedWords = new() { "One", "start", "more", "end", "thing" };
            List<string> filteredWords = GetWordsBetweenStartAndEnd(testedWords, "start", "end");
            foreach (string word in filteredWords) { Console.WriteLine(word); }


            //Write a method that returns all distinct words that have less than four letters in them.
            Console.WriteLine("==========6==========");
            IEnumerable<string> distinctWords = GetDistinctWords(testedString);
            foreach (string word in distinctWords) { Console.WriteLine(word); }

            //Create a class Name with 3 properties: First, Middle, Last. Write a method that returns the provided list of names, ordered by Last, in descending order.
            Console.WriteLine("==========7==========");
            List<Name> testedName = new() {
                new Name { First = "Denys", Middle = "Yuriovych", Last = "Koval" },
                new Name { First = "Svitlana", Middle = "Kostyantynivna", Last = "Petrova" },
                new Name { First = "Taras", Middle = "Hryhorovych", Last = "Radulenko" }};
            IEnumerable<Name> orderedByLast = GetNameOrderedByLast(testedName);
            foreach (var item in orderedByLast)
            {
                Console.WriteLine($"First Name {item.First} - Middle Name {item.Middle} - LastName {item.Last}.");
            }

            //Using the same Name class as before, write a method that returns the provided list of names, ordered by Last, then Middle, then First in descending order.
            Console.WriteLine("==========8==========");
            IEnumerable<Name> orderedByLastMiddleFirst = GetNameOrderedByLastMiddleFirst(testedName);
            foreach (var item in orderedByLast)
            {
                Console.WriteLine($"First Name {item.First} - Middle Name {item.Middle} - LastName {item.Last}.");
            }

            //Write a method that returns the number of strings in the provided sequence that begin with the provided startString.
            Console.WriteLine("==========9==========");
            Console.WriteLine("Number of strings : " + CountStringsWithPrefix(testedString,"ca"));

            //Write a method that returns the length of the shortest word
            Console.WriteLine("==========10==========");
            Console.WriteLine("Length of the shortest word : " + GetLengthOfTheShortestWord(testedString));

            //Write a method that returns the total number of characters in all words in the source sequence
            Console.WriteLine("==========11==========");
            Console.WriteLine("Total number of characters in all words : " + GetTotalNumberOfCharactersInAllWords(testedString));

            //Write a method that returns display strings in the form of "<Last>, <First>" for each provided name. Use Name class from above.
            Console.WriteLine("==========12==========");
            IEnumerable<string> displayStrings = DisplayStrings(testedName);
            foreach (var item in displayStrings)
            {
                Console.WriteLine(item);
            }

            //Given a sequence of words, get rid of any that don't have the character 'e' in them, then sort the remaining words alphabetically,
            //then return the following phrase using only the final word in the resulting sequence: -> "The last word is <word>"
            //If there are no words with the character 'e' in them, then return null.
            Console.WriteLine("==========13==========");

            Console.WriteLine($"The last word is : {GetSequenceOfWords(testedString, "e")}" );




        }

        private static string GetSequenceOfWords(IEnumerable<string> testedString, string character)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return (from name in testedString where name.Contains(character) orderby name select name).LastOrDefault("null");
#pragma warning restore CS8603 // Possible null reference return.
        }

        private static IEnumerable<string> DisplayStrings(List<Name> testedName)
        {
            return from name in testedName
                   select $"{name.Last} , {name.First}";
        }

        private static int GetTotalNumberOfCharactersInAllWords(IEnumerable<string> testedString)
        {
            return testedString.Sum(s => s.Length);

        }

        private static int GetLengthOfTheShortestWord(IEnumerable<string> testedString)
        {
            return testedString.Min(s => s.Length); 
        }

        public static int CountStringsWithPrefix(IEnumerable<string> testedString, string prefix)
        {
            return testedString.Count(word => word.StartsWith(prefix));
        }

        private static IEnumerable<Name> GetNameOrderedByLastMiddleFirst(List<Name> testedName)
        {
            return from name in testedName orderby name.Last descending, name.Middle descending, name.First descending select name;

        }

        private static IEnumerable<Name> GetNameOrderedByLast(List<Name> testedName)
        {
            return from name in testedName orderby name.Last descending select name;
        }

        private static IEnumerable<string> GetDistinctWords(IEnumerable<string> testedString)
        {
            return from word in testedString where word.Length < 4 select word;
        }

        private static void GetItems(IEnumerable<string> testedString)
        {

            IEnumerable<string> skipped = testedString.Skip(2).Take(3);
            foreach (string word in skipped)
            {
                Console.WriteLine(word);
            }
        }

        private static IEnumerable<string> GetWordsMethod(IEnumerable<string> testedString, string pattern)
        {
            return testedString.Where(x => x.Contains(pattern));
        }

        private static IEnumerable<string> GetWordsLINQ(IEnumerable<string> testedString, string pattern)
        {
            return from word in testedString where word.Contains(pattern) select word;
        }

        public static List<string> GetWordsBetweenStartAndEnd(List<string> words, string start, string end)
        {
            return words
                .SkipWhile(item => item != start)
                .TakeWhile(item => item != end).ToList();
        }
    }
}