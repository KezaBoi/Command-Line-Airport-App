using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// Presents menus to the screen for input and output.
    /// 
    /// RESPONSIBILITY: High-level user interaction.
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Display a header for the program
        /// </summary>
        void DisplayHeader();

        /// <summary>
        /// Display a menu to the user
        /// </summary>
        /// <param name="header">menu header</param>
        /// <param name="menu">list containing menu options</param>
        void DisplayMenu(string header, IReadOnlyList<string> menu);

        /// <summary>
        /// Retrieve menu choice
        /// </summary>
        /// <param name="menuLength">length of menu displayed to user</param>
        /// <returns>menu choice</returns>
        int GetMenuOption(int menuLength);

        /// <summary>
        /// Log a user into their account
        /// </summary>
        /// <param name="userDatabaseClass">database of users</param>
        /// <returns>account logged into</returns>
        User LoginUser(IUserDatabase userDatabaseClass);

        /// <summary>
        /// Print an objects details to a user
        /// </summary>
        /// <param name="item">object to display details off</param>
        void PrintDetails(IDisplayable item);
    }
}
