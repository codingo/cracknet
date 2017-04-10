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
            // Use this to produce a new flag. Make sure the outputted result is added to dectryptedString
            //var encryptedString = Crypto.EncryptStringAES("flag{bef46838-057b-4a1}", "73 65 63 74 61 6c 6b 73");

            const string pointless = "Strings would be too easy";
            Debug.WriteLine(pointless);

            var guesses = 5;

            Program.PrintBanner();

            while (true)
            {
                if (guesses < 1)
                {
                    Console.WriteLine("Too many guesses!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                Program.ShowTimer();

                Console.Write("Enter key: ");
                var input = Console.ReadLine();

                var decryptedString =
                Crypto.DecryptStringAES("EAAAAHuwWDEsiAOhz4xXW32MJGXHUKQDhRXARZFL+5dz90aeH+/c6yadJINEne6U4IKVZA==");

                if (input != null && input.Equals(decryptedString))
                {
                    Console.WriteLine($"Success! Flag: {decryptedString}!");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                guesses--;
                Console.WriteLine($"Incorrect! {guesses} guesses remain.");
            }
        }

        public static void PrintBanner()
        {
            Console.WriteLine("                            __                  __   ");
            Console.WriteLine("  ________________    ____ |  | __ ____   _____/  |_ ");
            Console.WriteLine("_/ ___\\_  __ \\__  \\ _/ ___\\|  |/ //    \\_/ __ \\   __\\");
            Console.WriteLine("\\  \\___|  | \\// __ \\  \\___|    <|   |  \\  ___/|  |  ");
            Console.WriteLine(" \\___  >__|  (____  /\\___  >__|_ \\___|  /\\___  >__|  ");
            Console.WriteLine("     \\/           \\/     \\/     \\/    \\/     \\/      ");
        }

        public static void ShowTimer()
        {
            for (var i = 5; i >= 0; --i)
            {
                var l = Console.CursorLeft;
                var t = Console.CursorTop;
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                Console.Write("Can make a guess in: {0}", i);
                Console.CursorLeft = l;
                Console.CursorTop = t;
                Thread.Sleep(1000);
            }
        }
    }
}
