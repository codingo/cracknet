using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/***
 * CrackMe Challenge made for the Brisbane SecTalks 2017 CTF Challenge
 * Created by Michael 'codingo' Skelton (michael@codingo.com.au)
 * 
 * Please compile using the debug manifest to ensure 'string' text is included within the binary.
 * No dependancies besides .net installed need to be present. Just the cracknet.exe can be included in the
 * ctf release. Name is intended to be a hint.
 * 
 * Solution: The intended solution to this challenge is to either reverse engineer the binary in a decompiler
 * such as dnSpy to print out the flag (intentionally made a bit harder by the timer write location and freq) or by
 * finding both the AES key, and salt. Salt is intentionally stored within the Cypto class so the first discovery of the AES
 * decrypt string leads to a false-positive result. Class has been kept verbose for this reason.
 * **/

namespace ctf.sectalks_bne.crackme
{
    class Program
    {
        private static void Main(string[] args)
        {
            // Use this to produce a new flag. Make sure the outputted result is added to the flag output
            //var encryptedString = Crypto.EncryptStringAES("flag{7h3_f0rc3_15_w17h_y0u}", "73 65 63 74 61 6c 6b 73");
            //Console.WriteLine($"New flag string: {encryptedString}");
            //Console.ReadKey();

            // use to produce a new passphrase. Ensure in lower case as input is lowered in routine.
            //var passphrase = Crypto.EncryptStringAES("the high ground", "73 65 63 74 61 6c 6b 73");
            //Console.WriteLine($"New passphrase: {passphrase}");
            //Console.ReadKey();

            // pointless string inclusion to show up if strings is used to reverse binary
            const string pointless = "flag{Not a real flag. Strings would be too easy";
            Debug.WriteLine(pointless);

            Program.PrintBanner();

            var guesses = 5;

            while (true)
            {
                if (guesses < 1) PrintGameOver();

                PrintTimer(3);
                PrintGuesses(guesses);

                Console.Write("Enter password: ");
                var input = Console.ReadLine();

                var password =
                Crypto.DecryptStringAES("EAAAAOkz8XiBpPhe0j3CnxGt4D5Qb0H2vh9/IeXrt1w4r313");
                
                if (input != null && input.ToLower().Equals(password))
                {
                    Console.WriteLine($"Success! Flag: {Crypto.DecryptStringAES("EAAAAB+ljfnegBraKanx/SJLBfrGhIDfffz8MOc922hrm0aK44KwgXmu9GHrIU+LjyBwmQ==")}!");

                    StarWars();
                    Environment.Exit(0);
                }

                guesses--;
                
                Console.WriteLine($"Incorrect! Please wait to try again.");

                Console.Beep(350, 250);
                Console.Beep(300, 500);
            }
        }

        public static void PrintBanner()
        {
            Console.WriteLine("\n                            __                  __   ");
            Console.WriteLine("  ________________    ____ |  | __ ____   _____/  |_ ");
            Console.WriteLine("_/ ___\\_  __ \\__  \\ _/ ___\\|  |/ //    \\_/ __ \\   __\\");
            Console.WriteLine("\\  \\___|  | \\// __ \\  \\___|    <|   |  \\  ___/|  |  ");
            Console.WriteLine(" \\___  >__|  (____  /\\___  >__|_ \\___|  /\\___  >__|  ");
            Console.WriteLine("     \\/           \\/     \\/     \\/    \\/     \\/      ");
        }

        private static void PrintGameOver()
        {
            Console.WriteLine("      _____          __  __ ______    ______      ________ _____  _ ");
            Console.WriteLine("     / ____|   /\\   |  \\/  |  ____|  / __ \\ \\    / /  ____|  __ \\| |");
            Console.WriteLine("    | |  __   /  \\  | \\  / | |__    | |  | \\ \\  / /| |__  | |__) | |");
            Console.WriteLine("    | | |_ | / /\\ \\ | |\\/| |  __|   | |  | |\\ \\/ / |  __| |  _  /| |");
            Console.WriteLine("    | |__| |/ ____ \\| |  | | |____  | |__| | \\  /  | |____| | \\ \\|_|");
            Console.WriteLine("     \\_____/_/    \\_\\_|  |_|______|  \\____/   \\/   |______|_|  \\_(_)");
            Console.WriteLine(" ________________________________________________________________________ ");
            Console.WriteLine("|________________________________________________________________________|");

            Mario();

            Environment.Exit(0);
        }

        public static void PrintTimer(int seconds)
        {
            for (var i = seconds; i >= 0; --i)
            {
                var originalLeft = Console.CursorLeft;
                var originalTop = Console.CursorTop;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.CursorLeft = 0;
                Console.CursorTop = 0;

                Console.Write("You can make another guess in: {0}", i);

                Console.CursorLeft = originalLeft;
                Console.CursorTop = originalTop;

                Thread.Sleep(1000);
            }
        }

        public static void PrintGuesses(int remainingGuesses)
        {
            var originalLeft = Console.CursorLeft;
            var originalTop = Console.CursorTop;

            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            Console.ForegroundColor = ConsoleColor.White;

            if (remainingGuesses == 1)
            {
                var originalColour = Console.BackgroundColor;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write($"You only have one guess remaining!");
                Console.BackgroundColor = originalColour;
            }
            else
            {
                Console.Write($"You have {remainingGuesses}/5 guesses remaining!!");
            }
            
            Console.CursorLeft = originalLeft;
            Console.CursorTop = originalTop;
        }

        private static void StarWars()
        {
            Console.Beep(300, 500);
            Thread.Sleep(50);
            Console.Beep(300, 500);
            Thread.Sleep(50);
            Console.Beep(300, 500);
            Thread.Sleep(50);
            Console.Beep(250, 500);
            Thread.Sleep(50);
            Console.Beep(350, 250);
            Console.Beep(300, 500);
            Thread.Sleep(50);
            Console.Beep(250, 500);
            Thread.Sleep(50);
            Console.Beep(350, 250);
            Console.Beep(300, 500);
            Thread.Sleep(50);
        }

        private static void Mario()
        {
            Console.Beep(659, 125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(659, 125);
            Thread.Sleep(167);
            Console.Beep(523, 125);
            Console.Beep(659, 125);
            Thread.Sleep(125);
            Console.Beep(784, 125);
            Thread.Sleep(375);
            Console.Beep(392, 125);
        }
    }
}
