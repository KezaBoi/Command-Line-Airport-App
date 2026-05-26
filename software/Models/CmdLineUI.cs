using System;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PST2
{
    /// <summary>
    /// Use the console to write and read user input and output. 
    /// 
    /// RESPONSIBILITY: User interaction.
    /// </summary>
    public class CmdLineUI : IUserInterface
    {
        /// <inheritdoc/>
        public void DisplayString()
        {
            Console.WriteLine();
        }

        /// <inheritdoc/>
        public void DisplayString(string msg)
        {
            Console.WriteLine(msg);
        }

        /// <inheritdoc/>
        public string? GetInput()
        {
            string? input = Console.ReadLine();
            DisplayString();
            return input;
        }

        /// <inheritdoc/>
        public void DisplayInputError(string errorMsg)
        {
            DisplayString("#####");
            DisplayString($"#Error - {errorMsg}.");
            DisplayString("# Please try again.");
            DisplayString("#####");
        }

        /// <inheritdoc/>
        public void DisplayAuthenticationError(string errorMsg)
        {
            DisplayString("#####");
            DisplayString($"# Error - {errorMsg}.");
            DisplayString();
            DisplayString("#####");
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">if entered nothing</exception>
        public string GetString()
        {
            string? input = GetInput();
            if (input != null)
            {
                return input;
            }
            else
            {
                throw new ArgumentNullException("Must enter a value");
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Caught in function, forcing valid input</exception>
        public string GetValidString(string item, string regexPattern, string? description = null)
        {
            while (true)
            {
                try
                {
                    DisplayString($"Please enter in your {item}:");
                    if (description != null)
                    {
                        DisplayString(description);
                    }
                    string input = GetString();

                    if (Regex.IsMatch(input, regexPattern))
                    {
                        return input;
                    }
                    else
                    {
                        throw new ArgumentException ($"Supplied {item} is invalid");
                    }
                }
                catch (Exception ex)
                {
                    DisplayInputError(ex.Message);
                }
            }
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">value entered not an int</exception>
        public int GetInt()
        {
            string? input = GetInput();
            if (!int.TryParse(input, out int i))
            {
                throw new ArgumentException("Supplied value is invalid");
            }
            return i;
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentOutOfRangeException">value entered out of bounds</exception>
        public int GetInt(int lowerBound, int upperBound)
        {
            int number = GetInt();
            if (number < lowerBound || number > upperBound)
            {
                throw new ArgumentOutOfRangeException(null, "Supplied value is out of range");
            }
            return number;
        }

        /// <inheritdoc/>
        public int GetValidInt(string item, int lowerBound, int upperBound)
        {
            while (true)
            {
                try
                {
                    DisplayString($"Please enter in your {item} between {lowerBound} and {upperBound}:");
                    return GetInt(lowerBound, upperBound);
                }
                catch (ArgumentOutOfRangeException)
                {
                    DisplayInputError($"Supplied {item} is invalid");
                }
                catch (ArgumentException ex)
                {
                    DisplayInputError(ex.Message);
                }
            }
        }



    }
}
