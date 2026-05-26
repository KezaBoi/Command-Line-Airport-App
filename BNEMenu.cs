using System;
using System.Text.RegularExpressions;

namespace PST2
{
    /// <summary>
    /// Presents a menu to the screen for input and output.
    /// 
    /// RESPONSIBILITY: High-level user interaction.
    /// </summary>
    /// <param name="userInterface">UI used throughout program</param>
    public class BNEMenu (IUserInterface userInterface) : IMenu
    {
        // Methods
        /// <inheritdoc/>
        public void DisplayHeader()
        {
            userInterface.DisplayString("==========================================");
            userInterface.DisplayString("=  Welcome to Brisbane Domestic Airport  =");
            userInterface.DisplayString("==========================================");
        }

        /// <inheritdoc/>
        public void DisplayMenu(string header, IReadOnlyList<string> menu)
        {
            userInterface.DisplayString(header);

            for (int i = 0; i < menu.Count; i++)
            {
                userInterface.DisplayString($"{i + 1}. {menu[i]}");
            }
        }
        
        /// <inheritdoc/>
        public int GetMenuOption(int menuLength)
        {
            while(true)
            {
                userInterface.DisplayString($"Please enter a choice between 1 and {menuLength}:");
                try
                {
                    int option = userInterface.GetInt(1, menuLength);
                    return option - 1;
                } 
                catch (ArgumentException ex)
                {
                    userInterface.DisplayInputError(ex.Message);
                }
            }
        }

        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">No registered accounts to log into</exception>
        public User LoginUser(IUserDatabase userDatabaseClass)
        {
            string email, password;
            int hashedEmail;

            userInterface.DisplayString("Login Menu.");

            if (userDatabaseClass.Count() == 0)
            {
                throw new InvalidOperationException("There are no people registered");
            }

            while (true)
            {
                email = userInterface.GetValidString("email", BNEConsts.EmailPattern);
                hashedEmail = email.GetHashCode();

                if (userDatabaseClass.Contains(email))
                {
                    break;
                }
                userInterface.DisplayAuthenticationError("Email is not registered");
            }

            User user = userDatabaseClass.GetUser(hashedEmail);
            while (true)
            {
                password = userInterface.GetValidString("password", BNEConsts.PasswordPattern);
                if (password == user.Password)
                {
                    break;
                }
                userInterface.DisplayAuthenticationError("Incorrect Password");
            }

            userInterface.DisplayString($"Welcome back {user.Name}.\n");

            return user;
        }

        /// <inheritdoc/>
        public void PrintDetails(IDisplayable item)
        {
            userInterface.DisplayString(item.GetDetails());
        }
    }
}
