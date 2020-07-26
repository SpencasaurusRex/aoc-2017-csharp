using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;

namespace AOC2017
{
    class Program {
        enum Person {
            Spencer,
            Blonxy
        }

        static Person? ParsePerson(string arg)
        {
            arg = arg.ToLower();
            Person? result = null;
            if (Person.Spencer.ToString().ToLower().StartsWith(arg)) 
                result = Person.Spencer;
            else if (Person.Blonxy.ToString().ToLower().StartsWith(arg) || "blynxy".StartsWith(arg)) 
                result = Person.Blonxy;
            return result;
        }

        static (string puzzleSelect, Person? peronSelect) ParseCommandLineArgs(string[] args)
        {
            string puzzleSelect = null;
            Person? personSelect = null;

            foreach (string arg in args)
            {
                if (double.TryParse(arg, out double num))
                {
                    puzzleSelect = arg;
                }
                else
                {
                    Console.WriteLine($"Unrecognized argument: {arg}");
                }
            }

            return (puzzleSelect, personSelect);
        }

        static void Main(string[] args)
        {
            (string puzzleSelect, Person? personSelect) = ParseCommandLineArgs(args);

            // For unselected parameters, ask user
            if (puzzleSelect == null)
            {
                Console.Write("Please select puzzle: ");
                while (puzzleSelect == null)
                {
                    string input = Console.ReadLine();
                    if (double.TryParse(input, out var num))
                    {
                        puzzleSelect = input;
                    }
                    else Console.Write("Please provide a valid puzzle number:");
                }
            }

            RunPuzzle(puzzleSelect);

            Console.ReadKey();
        }

        static string GetFilePath(string puzzleSelect, Person personSelect) 
        {
            return $"./Puzzles/{personSelect}/{puzzleSelect.Split('.')[0]}.txt";
        }

        static string GetFileInput(string puzzleInput, Person person) => File.ReadAllText(GetFilePath(puzzleInput, person));

        static void RunPuzzle(string puzzleSelect)
        {
            int puzzleNumInput = (int)Double.Parse(puzzleSelect);
            
            int blynxy_out = 0; int spence_out = 0;
            switch(puzzleSelect) {

                case "1":
                    //Puzzle1(fileInput);
                    break;
                case "1.2":
                    //Puzzle1_2(fileInput);
                    break;
                case "2":
                    //if (personSelect == Person.Spencer)
                    //    Puzzle2_Spencer(fileInput);
                    //else if (personSelect == Person.Blonxy)
                    //    Puzzle2_Blonxy(fileInput.ToCharArray());
                    break;
                case "2.2":
                    //Puzzle2_2(fileInput);
                    break;
                case "3":
                    //Puzzle3(fileInput);
                    break;
                case "3.2":
                    //if (personSelect == Person.Blonxy)
                    //    Puzzle3_2Blynxy(fileInput);
                    //else if (personSelect == Person.Spencer)
                    //    Puzzle3_2Spencer(fileInput);
                    break;
                case "4":
                    //Puzzle4(fileInput);
                    // Puzzle4LINQ(filePath);
                    break;
                case "4.2":
                
                    blynxy_out = Puzzle4_2(GetFilePath(puzzleSelect, Person.Blonxy));
                    Console.WriteLine("\n");
                    spence_out = Puzzle4_2(GetFilePath(puzzleSelect, Person.Spencer));
                    Console.WriteLine($"s:{spence_out}  b:{blynxy_out}");
                    break;
                
                case "5":
                    blynxy_out = Puzzle5(GetFilePath(puzzleSelect, Person.Blonxy));
                    Console.WriteLine("\n");
                    spence_out = Puzzle5(GetFilePath(puzzleSelect, Person.Spencer));
                    Console.WriteLine($"s:{spence_out}  b:{blynxy_out}");
                    break;
                case "5.2":
                    blynxy_out = Puzzle5_2(GetFilePath(puzzleSelect, Person.Blonxy));
                    Console.WriteLine("\n");
                    spence_out = Puzzle5_2(GetFilePath(puzzleSelect, Person.Spencer));
                    Console.WriteLine($"s:{spence_out}  b:{blynxy_out}");
                    break;
                case "6":
                    blynxy_out = Puzzle6(GetFilePath(puzzleSelect, Person.Blonxy));
                    Console.WriteLine("\nSpencer Start");
                    spence_out = Puzzle6(GetFilePath(puzzleSelect, Person.Spencer));
                    Console.WriteLine($"s:{spence_out}  b:{blynxy_out}");
                    break;
                case "6.2":
                    blynxy_out = Puzzle6_2(GetFilePath(puzzleSelect, Person.Blonxy));
                    Console.WriteLine("\nSpencer Start");
                    spence_out = Puzzle6_2(GetFilePath(puzzleSelect, Person.Spencer));
                    Console.WriteLine($"s:{spence_out}  b:{blynxy_out}");
                    break;
                case "7":
                    break;
                case "8":
                    break;
                case "9":
                    break;
                case "10":
                    break;
                case "11":
                    break;
                case "12":
                    break;
                case "13":
                    break;
                case "14":
                    break;
                case "15":
                    break;
                case "16":
                    break;
                case "17":
                    break;
                case "18":
                    break;
                case "19":
                    break;
                case "20":
                    break;
                case "21":
                    break;
                default: // cause fibbi boi
                    Console.WriteLine("Non-existent puzzle");
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

        unsafe static void Puzzle2_Blonxy(char[] inputChars) {
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
                }
            }

            Console.WriteLine(total);
        }

        static void Puzzle2_2(string input)
        {
            int total = 0;
            StringBuilder numberChars = new StringBuilder();

            int lineNumber = 1;

            List<int> numbers = new List<int>();
            foreach (char c in input)
            {
                switch (c)
                {
                    case '\r': continue;
                    case '\n':
                    case '\t':
                        if (numberChars.Length > 0)
                        {
                            int num = int.Parse(numberChars.ToString());
                            numbers.Add(num);
                        }
                        numberChars.Clear();
                        break;
                    default:
                        numberChars.Append(c);
                        break;
                }

                if (c == '\n')
                {
                    
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        for (int j = 0; j < numbers.Count; j++)
                        {
                            if (j == i) {
                                //skip
                                continue;
                            }
                            // Check if numbers are evenly divisible
                            if (numbers[i] % numbers[j] == 0)
                            {
                                Console.WriteLine($"line:{lineNumber} {numbers[i]} {numbers[j]} {numbers[i] / numbers[j]}");
                                total += numbers[i] / numbers[j];
                                goto asd;
                            }
                        }
                    }
                    asd:

                    numbers.Clear();
                    lineNumber++;
                }
            }

            Console.WriteLine(total);
        }


        struct Vector2 : IEquatable<Vector2>
        {
            public int X;
            public int Y;

            public Vector2(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Equals(Vector2 other)
            {
                return X == other.X && Y == other.Y;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X * 397) ^ Y;
                }
            }
        }

        static void Puzzle3(string fileInput)
        {
            const bool debug = false;

            Vector2 position = new Vector2();
            int dir = 0; // 0 = right, 1 = up, 2 = left, 3 = down
            int number = 2;
            int length = 1;
            bool skipLengthAdd = true;

            int endingNumber = Int32.Parse(fileInput);

            Dictionary<int, Vector2> dirs = new Dictionary<int, Vector2>
            {
                { 0, new Vector2( 1,  0) },
                { 1, new Vector2( 0,  1) },
                { 2, new Vector2(-1,  0) },
                { 3, new Vector2( 0, -1) },
            };

            while (true)
            {
                for (int i = 0; i < length && number <= endingNumber; i++, number++)
                {
                    position.X += dirs[dir].X;
                    position.Y += dirs[dir].Y;
                    if (debug) Console.WriteLine($"({position.X} {position.Y}) number: {number}");
                }
                if (number >= endingNumber) break;

                if (!skipLengthAdd)
                {
                    length++;
                }
                skipLengthAdd = !skipLengthAdd;
                dir = (dir + 1) % 4;
            }

            Console.WriteLine("Final answer: " + (Math.Abs(position.X) + Math.Abs(position.Y)));
        }

        enum Direction
        {
            Right,
            Up,
            Left,
            Down
        }

        static void Puzzle3_2Blynxy(string fileInput)
        {
            // TODO: Fix this shiz

            const bool debug = false;

            Vector2 position = new Vector2(500,500);
            Direction dir = Direction.Right; // 0 = right, 1 = up, 2 = left, 3 = down
            int number = 0;
            int pos_value = 1;
            int length = 1;
            int line_pos = 0;
            bool skipLengthAdd = true;
            
            
            int[][] data_grid = new int[10000][];
            for (int i = 0; i < 10000; i++)
                data_grid[i] = new int[10000];


            int endingNumber = Int32.Parse(fileInput);

            Dictionary<Direction, Vector2> dirs = new Dictionary<Direction, Vector2>
            {
                { Direction.Right, new Vector2( 1,  0) },
                { Direction.Up, new Vector2( 0,  1) },
                { Direction.Left, new Vector2(-1,  0) },
                { Direction.Down, new Vector2( 0, -1) },
            };

            while (pos_value < endingNumber)
            {
                if (pos_value != 1)
                    pos_value = 0;
                if (dir == Direction.Right) {
                    // add ~left, left-up, up, right-up
                    pos_value += data_grid[position.X + dirs[Direction.Left].X][position.Y + dirs[Direction.Left].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Left].X+dirs[Direction.Up].X][position.Y + dirs[Direction.Left].Y + dirs[Direction.Up].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Up].X][position.Y + dirs[Direction.Up].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Right].X + dirs[Direction.Up].X][position.Y + dirs[Direction.Right].Y + dirs[Direction.Up].Y];
                }
                if (dir == Direction.Up) {
                    // add ~down, down-left, left, left-up
                    pos_value += data_grid[position.X + dirs[Direction.Down].X][position.Y + dirs[Direction.Down].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Left].X + dirs[Direction.Down].X][position.Y + dirs[Direction.Left].Y + dirs[Direction.Down].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Left].X][position.Y + dirs[Direction.Left].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Left].X + dirs[Direction.Up].X][position.Y + dirs[Direction.Left].Y + dirs[Direction.Up].Y];


                }
                if (dir == Direction.Left) {
                    // add ~right, right-down, down, left-down
                    try
                    {
                        pos_value +=
                            data_grid[position.X + dirs[Direction.Right].X][position.Y + dirs[Direction.Right].Y];
                        pos_value +=
                            data_grid[position.X + dirs[Direction.Right].X + dirs[Direction.Down].X][
                                position.Y + dirs[Direction.Right].Y + dirs[Direction.Down].Y];
                        pos_value +=
                            data_grid[position.X + dirs[Direction.Down].X][position.Y + dirs[Direction.Down].Y];
                        pos_value +=
                            data_grid[position.X + dirs[Direction.Left].X + dirs[Direction.Down].X][
                                position.Y + dirs[Direction.Left].Y + dirs[Direction.Down].Y];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(position.X + dirs[Direction.Right].X + " " + position.Y + dirs[Direction.Right].Y);
                    }

                }
                if (dir == Direction.Down) {
                    // add ~up, right-up, right, right-down
                    pos_value += data_grid[position.X + dirs[Direction.Up].X][position.Y + dirs[Direction.Up].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Right].X + dirs[Direction.Up].X][position.Y + dirs[Direction.Right].Y + dirs[Direction.Up].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Right].X][position.Y + dirs[Direction.Right].Y];
                    pos_value += data_grid[position.X + dirs[Direction.Right].X + dirs[Direction.Down].X][position.Y + dirs[Direction.Right].Y + dirs[Direction.Down].Y];


                }

                data_grid[position.X][position.Y] = pos_value;

                if (debug) Console.WriteLine($"({position.X} {position.Y}) number: {number}");
                position.X += dirs[dir].X;
                position.Y += dirs[dir].Y;

                if(number != 0)
                    line_pos++;
                if (line_pos >= length)
                {

                    if (!skipLengthAdd)
                    {
                        length++;
                    }
                    // toggle line length every other line
                    skipLengthAdd = !skipLengthAdd;
                    line_pos = 0;
                    dir = (dir == Direction.Down ? Direction.Right : dir + 1);
                }


                number++;
            } // end while

            
        

            Console.WriteLine("Final answer: " + pos_value);
        }


        static void Puzzle3_2Spencer(string fileInput)
        {
            const bool debug = false;

            Vector2 position = new Vector2();
            int dir = 0; // 0 = right, 1 = up, 2 = left, 3 = down
            int length = 1;
            bool skipLengthAdd = true;

            int inputNum = Int32.Parse(fileInput);

            Dictionary<Vector2, int> values = new Dictionary<Vector2, int>()
            {
                {new Vector2(0, 0), 1}
            };

            Dictionary<int, Vector2> dirs = new Dictionary<int, Vector2>
            {
                {0, new Vector2(1, 0)},
                {1, new Vector2(0, 1)},
                {2, new Vector2(-1, 0)},
                {3, new Vector2(0, -1)},
                {4, new Vector2(-1, -1)},
                {5, new Vector2(1, -1)},
                {6, new Vector2(1, 1)},
                {7, new Vector2(-1, 1)},
            };

            while (true)
            {
                for (int i = 0; i < length; i++)
                {
                    position.X += dirs[dir].X;
                    position.Y += dirs[dir].Y;

                    // Add surrounding tiles
                    int total = 0;
                    for (int d = 0; d < 8; d++)
                    {
                        var delta = dirs[d];
                        var neighborPos = new Vector2(position.X + delta.X, position.Y + delta.Y);
                        if (values.ContainsKey(neighborPos))
                        {
                            total += values[neighborPos];
                        }
                    }

                    values.Add(position, total);
                    if (total > inputNum)
                    {
                        Console.WriteLine(total);
                        return;
                    }

                    if (debug) Console.WriteLine($"({position.X} {position.Y}) {total}");
                }

                if (!skipLengthAdd)
                {
                    length++;
                }

                skipLengthAdd = !skipLengthAdd;

                dir = (dir + 1) % 4;
            }
        }

        static void Puzzle4(string fileInput)
        {
            // read in the next word
            char[] chars = fileInput.ToCharArray();
            HashSet<string> words = new HashSet<string>();
            StringBuilder next_word = new StringBuilder();
            int total_valid = 0;
            bool invalid_code = false;
            int line_number = 1;

            foreach (var next_char in chars)
            {
                if (next_char == '\r') continue;
                if (invalid_code && next_char != '\n') continue;

                // process word if ' ' or end of line
                if ((next_char == ' ' || next_char == '\n') && !invalid_code)
                {
                    // check hash for next_word
                    string word = next_word.ToString();
                    if (words.Contains(word))
                    {
                        invalid_code = true;
                    } 
                    else
                    {
                        words.Add(word);
                    }

                    next_word.Clear();
                }

                // process line if \n
                if (next_char == '\n')
                {
                    if (!invalid_code) total_valid++;
                    


                    if (invalid_code)
                    {
                        Console.WriteLine(line_number + " " + !invalid_code + " " + string.Join(",", words));
                    }
                    line_number++;
                    words.Clear();
                    invalid_code = false;
                    

                    continue;
                }

                // append letter to current_word
                if (next_char != ' ' && next_char != '\n')
                {
                    next_word.Append(next_char);
                }
            }
            
            Console.WriteLine(total_valid);
        }

        static void Puzzle4LINQ(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            int totalValid = 0;
            int linenumber = 1;
            foreach (var line in lines)
            {
                string[] words = line.Split(' ');
                if (words.Distinct().Count() == words.Length)
                {
                    totalValid++;
                }
                else
                {
                    Console.WriteLine(linenumber);
                }

                linenumber++;
            }
            Console.WriteLine(totalValid);
        }

        static int Puzzle4_2(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            int totalValid = 0;
            int linenumber = 1;

            const bool debug = false;

            foreach (var line in lines)
            {
                string[] words = line.Split(' ').Select(str => string.Concat(str.OrderBy(c => c))).ToArray();
                if (words.Distinct().Count() == words.Length)
                {
                    totalValid++;
                }
                else if (debug)
                {
                    Console.WriteLine(linenumber);
                }

                linenumber++;
            }

            return totalValid;
        }
    
        static int Puzzle5(string filePath)
        {
            // maze
            // -1 moves previous instruction
            // 2 skips next one
            // start at index 0
            // follow until out of bound
            // record followed steps

            int followed_steps = 0;
            int current_instruction_index = 0;
            //List<int> instructions = fileInput.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

            List<int> instructions = new List<int>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (line.Trim() == "") continue;
                instructions.Add(Int32.Parse(line));
            }

            //Console.WriteLine(string.Join(", ", instructions));

            while (current_instruction_index < instructions.Count)
            {

                // follow instruction
                int prev_index = current_instruction_index;
                current_instruction_index += instructions[current_instruction_index];

                instructions[prev_index]++;

                followed_steps++;
            }

            return followed_steps;
        }

        static int Puzzle5_2(string filePath)
        {
            int followed_steps = 0;
            int current_instruction_index = 0;
            //List<int> instructions = fileInput.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();

            List<int> instructions = new List<int>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (line.Trim() == "") continue;
                instructions.Add(Int32.Parse(line));
            }

            while (current_instruction_index < instructions.Count)
            {

                // follow instruction
                int prev_index = current_instruction_index;
                current_instruction_index += instructions[current_instruction_index];

                if (instructions[prev_index] >= 3)
                {
                    instructions[prev_index]--;
                }
                else instructions[prev_index]++;

                followed_steps++;
            }

            return followed_steps;
        }

        static int Puzzle6(string filePath)
        {
            // 16 memory banks
            // each bank any number of blocks
            // balance blocks in banks
            
            // find most blocked bank, lowest num bank win
            // takes all blocks from largest, distributes to all starting at +1
            
            // how many redistributions before same number of blocks in banks seen before

            List<int> memory = File.ReadAllText(filePath).Split(new [] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries).Select(str => Int32.Parse(str)).ToList();

            int sum = memory.Sum(i => i);

            HashSet<string> previousStates = new HashSet<string>();
            int heldBlocks = 0;
            int redistributions = 0;

            while (true)
            {
                // Find slot with highest
                int highestIndex = 0;
                for (int i = 1; i < memory.Count; i++)
                {
                    if (memory[i] > memory[highestIndex])
                    {
                        highestIndex = i;
                    }
                }

                // Take all blocks out (increaseBlocks)
                heldBlocks = memory[highestIndex];
                memory[highestIndex] = 0;

                // Redistribute the blocks from highestIndex + 1
                int index = (highestIndex + 1) % memory.Count;
                while (heldBlocks > 0)
                {
                    // add 1 to memory
                    memory[index]++;
                    heldBlocks--;
                    index = (index + 1) % memory.Count;
                }
                redistributions++;
                
                // Check our state again
                string state = string.Join(" ", memory);
                //Console.WriteLine(state);
                if (previousStates.Contains(state))
                {
                    return redistributions;
                }
                else
                {
                    previousStates.Add(state);
                }
            }
        }

        static void PrintState(int index, string memory, int heldBlocks)
        {
            Console.Clear();
            int charIndex = 0;
            for (int i = 0; i < index; i++)
            {
                charIndex = memory.IndexOf(' ', charIndex) + 1;
            }

            Console.WriteLine(memory);
            Console.Write(String.Concat(Enumerable.Repeat(" ", charIndex)));
            Console.WriteLine("^");
            Console.WriteLine("Heldblocks: " + heldBlocks);
        }

        static int Puzzle6_2(string filePath)
        {
            List<int> memory = File.ReadAllText(filePath).Split(new [] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries).Select(str => Int32.Parse(str)).ToList();

            int sum = memory.Sum(i => i);
            
            Dictionary<string ,int> previousStates = new Dictionary<string, int>();
            int heldBlocks = 0;
            int redistributions = 0;

            while (true)
            {
                // Find slot with highest
                int highestIndex = 0;
                for (int i = 0; i < memory.Count; i++)
                {
                    if (memory[i] > memory[highestIndex])
                    {
                        highestIndex = i;
                    }
                }

                // Take all blocks out (increaseBlocks)
                heldBlocks = memory[highestIndex];
                memory[highestIndex] = 0;

                // Redistribute the blocks from highestIndex + 1
                int index = (highestIndex + 1) % memory.Count;
                while (heldBlocks > 0)
                {
                    // add 1 to memory
                    memory[index]++;
                    heldBlocks--;
                    index = (index + 1) % memory.Count;

                    //PrintState(index, string.Join(" ", memory), heldBlocks);
                    //Console.ReadKey();
                }
                heldBlocks = 0;
                redistributions++;
                
                // num_banks, num_blocks, 

                
                // Check our state again
                string state = string.Join(" ", memory);
                //Console.WriteLine(state);
                if (previousStates.ContainsKey(state))
                {
                    return redistributions - previousStates[state];
                }
                else
                {
                    previousStates.Add(state, redistributions);
                }
            }
        }
    }
}
