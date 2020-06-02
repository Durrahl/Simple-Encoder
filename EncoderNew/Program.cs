using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EncoderNew
{
    class EncoderMain
    {
        // The Alphabet set used for encryption
        char[] alphabet = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', ' '};

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome To Encoder");
            EncoderMain encMain = new EncoderMain();
            encMain.StartEncoder();
        }


        // Main method
        void StartEncoder()
        {

            string input;
            string passcode;

            while (true)
            {
                Console.WriteLine("Please input string to be encoded: ");
                input = Console.ReadLine();

                // Validation
                if (input != null && input != "")
                {
                    break;
                }

                Console.Clear();
            }

            while (true)
            {
                Console.WriteLine("Please passcode string to encode with: ");
                passcode = Console.ReadLine();

                // Validation
                if (passcode != null && passcode != "")
                {
                    break;
                }

                Console.Clear();
            }

            // QOL
            Console.Clear();


            // Encoding call
            string output = Encode(input, passcode);


            // Output
            Console.WriteLine("Original: " + input + " from the code: " + passcode + "\n");
            Console.WriteLine("Output from that string is: " + output);
        }


        // Encoding Process
        string Encode(string toBeEncoded, string passcode)
        {
            // Holds converted message
            char[] result = toBeEncoded.ToCharArray();

            // Holds string for return
            string convertedString = "";

            // Convert passKey to individual numbers
            char[] individualCodes = passcode.ToCharArray();

            // Holds pass code characters
            List<int> codes = new List<int>();


            for (int i = 0; i < individualCodes.Length; i++)
            {
                // Parse codes from chars
                try
                {
                    codes.Add(int.Parse(individualCodes[i].ToString()));
                }
                catch (Exception e)
                { 
                
                }
            }

            // Used to hold count of pass code access length in wrap-around
            int toAdd = 0;


            // Replace characters in char array
            for (int i = 0; i < result.Length; i++)
            {
                toAdd = i;
                while (individualCodes.Length <= toAdd)
                {
                    toAdd -= individualCodes.Length;
                }

                int modifier = individualCodes[toAdd];

                if (toAdd > 0)
                {
                    modifier = (individualCodes[toAdd] * individualCodes[toAdd - 1]);
                }


                result[i] = GetInAlphabet(FindInKey(result[i]) + modifier);
                
            }


            // Convert string from char array
            for (int i = 0; i < result.Length; i++)
            {
                convertedString += result[i].ToString();
            }


            return convertedString;
        }


        // Used to find a matching char in the alphabet key
        int FindInKey(char charToFind)
        {
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (charToFind == alphabet[i])
                {
                    return i;
                }
            }

            return 0;
        }


        // Get an element from the alphabet array, this method is used to allow for wrap-around.
        char GetInAlphabet(int toGet)
        {
            // Lowers till in bounds
            while (toGet >= alphabet.Length)
            {
                toGet -= alphabet.Length;
            }

            return alphabet[toGet];
        }

  
    }
}
