using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AOC2017
{
    class Program {
        enum Person {
            Spencer,
            Blonxy
        }


        static void Main(string[] args)
        {
            // TODO: Get with console input or whatever I don't fucking care man I just want to die
            if (args.Length == 0 || !Double.TryParse(args[0], out var puzzleSelect)) {
                Console.WriteLine("You're stupid [[ you didn't pass in jack-split ]]\n");
                return;
            }

            Person person = Person.Spencer;

            string input = File.ReadAllText($"./Puzzles/{person}/{(int)puzzleSelect}.txt");
            char[] charInput = input.ToCharArray();

            Stopwatch stop = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                Puzzle2_Spencer(input);
            }
            stop.Stop();
            Console.WriteLine("Spencer: " + stop.ElapsedMilliseconds);

            stop = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                Puzzle2(charInput); // 94384
                //Puzzle2(input); // 109673
            }
            stop.Stop();
            Console.WriteLine("Blonxy: " + stop.ElapsedTicks);

            return;

            switch(puzzleSelect) {

                case 1:

                    Puzzle1(input);
                    break;
                case 1.2:
                    Puzzle1_2(input);
                    break;
                case 2:
                    if (person == Person.Spencer)
                        Puzzle2_Spencer(input);
                    else if (person == Person.Blonxy)
                        //Puzzle2(input);
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 16:
                    break;
                case 17:
                    break;
                case 18:
                    break;
                case 19:
                    break;
                case 20:
                    break;
                case 21:
                    break;
                default: // cause fibbi boi
                    Console.WriteLine("Nonexistant puzzle");
                    break;
            }

        }

        static void Puzzle1(string puzzleInput)
        {
            int total = 0;
            char[] puzzle = puzzleInput.Trim().ToCharArray();
            char previousChar = puzzle.Last();

            foreach (char c in puzzle)
            {
                total += (c == previousChar)? previousChar-'0' : 0;
                previousChar = c;
            }

            Console.WriteLine(total);
        }
        static void Puzzle1_2(string puzzleInput)
        {
            int total = 0;
            char[] puzzle = puzzleInput.ToCharArray();
            int previousCharPos = puzzle.Length/2;

            for(int c=0; c<puzzle.Length; c++)
            {
                total += (puzzle[c] == puzzle[previousCharPos]) ?  puzzle[c] - '0' : 0;
                previousCharPos = (previousCharPos + 1) % puzzle.Length;
            }

            Console.WriteLine(total);
        }

        unsafe static void Puzzle2(char[] inputChars) { 
        //unsafe static void Puzzle2(string input) { 
            //StringBuilder output = new StringBuilder();
            
            int total = 0;
            int lineHigh = 0; // E L E V E N THIRTY
            int lineLow = -1;
            int currNum = 0;
            int digitMultiplier = 1;
            //char[] inputChars = input.ToCharArray();
            fixed (char* beginning = &inputChars[0])
            fixed (char* c2 = &inputChars[inputChars.Length - 1])
            {
                char* c = c2;
                do
                {
                    if(*c != '\t')
                    //output.Append(*c);
                    if (*c == '\r')
                    {
                        continue;
                    }

                    if (*c != '\n' && *c != '\t' || c == beginning)
                    {
                        //output.Append(".");
                        currNum += (*c - '0') * digitMultiplier;
                        digitMultiplier *= 10;
                    }

                    if (*c == '\t' || *c == '\n' && currNum != 0 || c == beginning)
                    {
                        //output.Append($"[{currNum}] ");
                        lineHigh = (lineHigh < currNum) ? currNum : lineHigh;
                        lineLow = (lineLow > currNum || lineLow < 0) ? currNum : lineLow;
                        currNum = 0;
                        digitMultiplier = 1;
                        //if (c != beginning)
                        //    continue;
                    }

                    if (*c == '\n' || c == beginning)
                    {
                        total += lineHigh - ((lineLow < 0) ? 0 : lineLow);
                        //output.AppendLine($"\nT:{total} | L:{lineLow} | H:{lineHigh} | DIFF:{lineHigh-lineLow}");
                        lineHigh = 0;
                        lineLow = -1;
                        continue; // F A C E
                    }

                } while (--c >= beginning);
            }
            //output.AppendLine($"\n{total}");
            //Console.WriteLine($"\n{total}");

            //File.WriteAllText("./output.txt", output.ToString());
        }

        static int CharArrayToInt(char[] chars)
        {
            int digitMultiplier = (int)Math.Pow(10, chars.Length - 1);
            int finalNumber = 0;
            foreach (var c in chars)
            {
                int digit = c - '0';
                finalNumber += digit * digitMultiplier;
                digitMultiplier /= 10;
            }

            return finalNumber;
        }

        static void Puzzle2_Spencer(string input)
        {
            int total = 0;
            int lineHigh = 0;
            int lineLow = Int32.MaxValue;
            StringBuilder number = new StringBuilder();

            foreach (char c in input)
            {
                switch (c)
                {
                    case '\r': continue;
                    case '\n':
                    case '\t':
                        if (number.Length > 0)
                        {
                            int num = int.Parse(number.ToString());
                            //Console.Write(num + " ");
                            if (num > lineHigh)
                            {
                                lineHigh = num;
                            }
                            else if (num < lineLow)
                            {
                                lineLow = num;
                            }
                        }
                        number.Clear();
                        break;
                    default:
                        number.Append(c);
                        break;
                }

                if (c == '\n')
                {
                    total += lineHigh - lineLow;
                    lineHigh = 0;
                    lineLow = Int32.MaxValue;
                    //Console.WriteLine();
                }
            }

            //Console.WriteLine(total);
            
        }
    }
}
