using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bigram
{
    class Program
    {

        static string TextFromFile(string fileLocation)
        {
            var text = string.Empty;
            try
            {
                text = System.IO.File.ReadAllText(fileLocation);
            }
            catch(Exception x)
            {
                Console.WriteLine("Error => " + x.ToString());
                
            }

            return text;


        }

        
        static bool isValidChar(char c) 
        {
            //right now we are assuming letters and digits are good, everything else bad
            return char.IsLetterOrDigit(c);
        }

        static bool isWordSeperator(char c)
        {

            //right now we determine seperator is a space or a punctuation
            return char.IsSeparator(c) || char.IsPunctuation(c);
        }

        static string GetFile()
        {
            string filelocation = string.Empty;

            //Example we are asking the user for location via prompt
            Console.Write("File Location: ");

            filelocation = Console.ReadLine();
            //filelocation = @"s:\tempText.txt";

            return filelocation;
        }

        static Dictionary<string,uint> CalculateHistogram(IEnumerable<string> grams)
        {
            var h = new Dictionary<string, uint>();

            foreach(string gram in grams)
            {
                if (h.ContainsKey(gram))
                    h[gram]++;
                else
                    h.Add(gram, 1);
            }

            return h;
        }

        

        static IEnumerable<string> GenerateNGram(string text, uint size, bool caseSenstive = false)
        {
            List<string> grams = new List<string>();
            var sb = new StringBuilder();
            Queue<int> len = new Queue<int>();
            int count = 0;
            int last = 0;

            if (!caseSenstive)
                text = text.ToLowerInvariant();

            //intiialize if not empty
            if( !string.IsNullOrEmpty(text) && isValidChar(text[0]))
            {
                sb.Append(text[0]);
                last++;
            }

            for(int index = 1; index < text.Length - 1; index++)
            {
                var c = text[index];

                var pre = text[index - 1];
                var post = text[index + 1];


                //form word
                if( isValidChar(c) || ( c != ' ' 
                                         && (isWordSeperator(c) )
                                         && ( isValidChar(pre) && isValidChar(post))
                                       )
                  )
                {
                    sb.Append(c);
                    last++;
                }
                else
                {
                    if (last > 0)
                    {
                        len.Enqueue(last);
                        last = 0;
                        count++;


                        if (count >= size)
                        {
                            grams.Add(sb.ToString());
                            //yield return sb.ToString();
                            sb.Remove(0, len.Dequeue() + 1);
                            count -= 1;
                        }

                        sb.Append(" ");
                    }
                }
            }

            return grams;

        }

        static void Main(string[] args)
        {

            var file = GetFile();
            string fullText = TextFromFile(file);


            var grams = GenerateNGram(fullText, 2);

            var histogram = CalculateHistogram(grams);

            var sortHistogram = histogram; // assume order by histogram is based on word placement

            Console.WriteLine();

            foreach( KeyValuePair<string,uint> h in sortHistogram)
            {
                Console.WriteLine("\"" + h.Key + "\" => " + h.Value);
            }

            Console.Read();

        }

       
    }
}
