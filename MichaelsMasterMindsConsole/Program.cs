﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MichaelsMasterMindsConsole
{
    class Program
    {
        /// <summary>
        /// Asks the user for a digit; reads the digit and checks if it's between 0-9  
        /// </summary>
        /// <param name="message">Displays message 4 times</param>
        /// <returns>digit between 0 and 9</returns>
        static int getNumber(string message, char min = '0', char max = '9')
        {
            Console.Write(message);
            int number = -1;

            //ASCII values for numbers 0 through 9 are 48 through 57
            while (number == -1)
            {
                char pressedKey = Console.ReadKey(true).KeyChar;

                if (pressedKey <= max && pressedKey >= min)
                {
                    number = int.Parse(pressedKey.ToString());
                }
            }

            return number;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            while (true)
            {
                //Setting the background to DarkCyan
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.Clear();

                //Display a menu
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("MasterMinds game");
                Console.WriteLine("--------------------");
                Console.WriteLine("1) Text version");
                Console.WriteLine("2) Graphical version");
                Console.WriteLine("3) Exit");
                Console.WriteLine();
                int choice = getNumber("Enter selection => ", '1', '3');

                Console.Clear();

                switch (choice)
                {
                    case 1:
                        textMasterMinds();
                        break;

                    case 2:
                        drawSquares();
                        Console.WriteLine("\nWork in progress... Press any key to return to main menu...");
                        Console.ReadKey(true);
                        break;

                    case 3:
                        return;
                }
            }
        }

        private static void textMasterMinds()
        {
            string emptyLine = new string(' ', 60);

            int tries = 0;
            Console.ForegroundColor = ConsoleColor.White;

            MasterMindsNumber computerNumber = new MasterMindsNumber();
            computerNumber.CreateNumber();

            String[] numberNames = { "first", "second", "third", "fourth" };
            bool didUserWin = false;

            do
            {
                tries++;
                MasterMindsNumber userInput = new MasterMindsNumber();

                //Clearing lines so new numbers could be entered
                Console.SetCursorPosition(0, 1);
                Console.WriteLine(emptyLine);
                Console.WriteLine(emptyLine);

                for (int i = 0; i < 4; i++)
                {
                    //Just setting it to the top (0, 0)
                    Console.SetCursorPosition(0, 0);
                    int number = getNumber(String.Format("Please give me {0} number\n", numberNames[i]));
                    bool isAddSuccessful = userInput.AddDigit(number);

                    if (!isAddSuccessful)
                    {
                        //Setting 2 lines down
                        Console.SetCursorPosition(0, 2);
                        Console.WriteLine("It looks like we already have the number {0}; please try again.", number);
                        i--;
                    }
                    else
                    {
                        //Setting it to the position of the current number
                        Console.SetCursorPosition(i, 1);
                        Console.WriteLine(number);
                        Console.WriteLine(emptyLine);
                    }
                }

                //Compare the two MasterMindsNumnber objects
                didUserWin = userInput.Check(computerNumber);


                //This is the checking setting it 5 lines down
                Console.SetCursorPosition(0, 5);
                Console.ForegroundColor = ConsoleColor.Yellow;
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine(emptyLine);
                }
                //This is making the checking results Yellow and then once were ready to continue it becomes gray
                Console.SetCursorPosition(0, 5);
                Console.WriteLine("Checking... here are the results:");
                Console.WriteLine(userInput);
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Press any key to try again...");
                Console.ReadKey(true);

                //This is just making the checking gray by rewriting it on the same lines but gray
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(0, 5);
                Console.WriteLine("Checking... here are the results:");
                Console.WriteLine(userInput);
                Console.ForegroundColor = ConsoleColor.White;

                //Erases the "Press any key to try again" 
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(emptyLine);

            } while (!didUserWin);

            Console.WriteLine(String.Format("Congratulations! You win! It took you {0} tries.", tries));
            Console.ReadKey();
        }

        private static void drawSquares()
        {
            //Save for later
            //Console.WriteLine($"0x2580: {(char)0x2580}\n");
            //Console.WriteLine($"0x2584: {(char)0x2584}\n");

            char block = (char)9608; //0x2588;
            string blockLine = new string(block, 3);
            string emptyLine = new string(' ', 60);

            string[] texture = { blockLine, blockLine };
            int lineSpacing = texture.Length + 1;

            Square[] squares = new Square[8];
            int x = 70;
            int y = 0;

            squares[0] = new Square(texture, new Point(x, y), ConsoleColor.Black, 'K');
            squares[0].LetterPosition = new Point(x - 2, y);
            squares[0].IsLetterVisible = true;
            y += lineSpacing;

            for (int i = 9; i < 16; i++)
            {
                //using console colors 9-16 which is 7 numbers (because we already have one so it's 8 total)
                int squareNumber = i - 8;
                ConsoleColor color = (ConsoleColor)i;

                Square square = new Square(texture, new Point(x, y), color, color.ToString()[0]);
                square.LetterPosition = new Point(x - 2, y);
                square.IsLetterVisible = true;
                squares[squareNumber] = square;
                y += lineSpacing;
            };

            foreach (Square square in squares)
            {
                square.Draw(); 
            }

        }
    }
}
