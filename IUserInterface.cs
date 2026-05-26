using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// Write and read user input and output. 
    /// 
    /// RESPONSIBILITY: User interaction.
    /// </summary>
    public interface IUserInterface
    {
        /// <summary>
        /// Show a blank line on the UI
        /// </summary>
        void DisplayString();

        /// <summary>
        /// Displays a message to the console screen
        /// </summary>
        /// <param name="msg">The message to display</param>
        void DisplayString(string msg);

        /// <summary>
        /// Get user input from console
        /// </summary>
        /// <returns>user input</returns> 
        string? GetInput();

        /// <summary>
        /// Get an integer from user
        /// </summary>
        /// <returns>integer input</returns>
        int GetInt();

        /// <summary>
        /// Get a bounded integer from a user (inclusive)
        /// </summary>
        /// <param name="lowerBound">lower bound for int</param>
        /// <param name="upperBound">upper bound for int</param>
        /// <returns>integer input</returns>
        int GetInt(int lowerBound, int upperBound);

        /// <summary>
        /// Get a bounded integer from a user (inclusive) with a request message
        /// </summary>
        /// <param name="item">value requesting integer for</param>
        /// <param name="lowerBound">lower bound for int</param>
        /// <param name="upperBound">upper bound for int</param>
        /// <returns>integer input</returns>
        int GetValidInt(string item, int lowerBound, int upperBound);


        /// <summary>
        /// Get a string from user
        /// </summary>
        /// <returns>input string</returns>
        string GetString();

        /// <summary>
        /// Get a validated string from a user
        /// </summary>
        /// <param name="item">value requesting string for</param>
        /// <param name="regexPattern">regex pattern for value</param>
        /// <param name="description">optional instructions to be included</param>
        /// <returns>validated input string</returns>
        string GetValidString(string item, string regexPattern, string? description = null);

        /// <summary>
        /// Displays an error to the UI, and requests user to try again.
        /// </summary>
        /// <param name="errorMsg">The string.</param>
        void DisplayInputError(string errorMsg);

        /// <summary>
        /// Diplays error without retry request
        /// </summary>
        /// <param name="errorMsg">error message</param>
        void DisplayAuthenticationError(string errorMsg);

    }
}
