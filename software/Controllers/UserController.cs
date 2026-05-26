using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// Class for managing processes related to users.
    /// Currently only registration.
    /// 
    /// RESPONSIBILITY: manages user processess
    /// </summary>
    /// <param name="userInterface">UI used throughout program</param>
    /// <param name="menu">Menu used throughout program</param>
    /// <param name="userDatabase">User database used by program</param>
    public class UserController (IUserInterface userInterface, IMenu menu, IUserDatabase userDatabase) : IUserController
    {
        // Fields
        enum UserType { Standard_Traveller, Frequent_Flyer, Flight_Manager }

        // Methods
        /// <inheritdoc/>
        /// <exception cref="InvalidOperationException">Invalid user option recieved</exception>
        public void RegisterUser()
        {
            User newUser;

            menu.DisplayMenu(BNEConsts.REGISTER_HEADER, BNEConsts.REGISTER_MENU);
            int userType = menu.GetMenuOption(BNEConsts.REGISTER_MENU.Count);

            switch (userType)
            {
                case (int)UserType.Standard_Traveller:
                    newUser = RegisterStandardTraveller();
                    break;

                case (int)UserType.Frequent_Flyer:
                    newUser = RegisterFrequentFlyer();
                    break;

                case (int)UserType.Flight_Manager:
                    newUser = RegisterFlightManager();
                    break;

                default:
                    throw new InvalidOperationException("Not a valid user choice");
            }
            userDatabase.AddUser(newUser);
        }

        /// <summary>
        /// Process to register a standard user of the system
        /// </summary>
        /// <param name="name">at least 1 letter</param>
        /// <param name="age">between 0 and 99</param>
        /// <param name="phoneNumber">9 digits and leading 0</param>
        /// <param name="emailAddress">formatted email</param>
        /// <param name="password">validated password</param>
        private void RegisterStandardUser(out string name, out int age, out string phoneNumber, out string emailAddress, out string password)
        {
            name = userInterface.GetValidString("name", BNEConsts.NamePattern);

            age = userInterface.GetValidInt("age", 0, 99);

            phoneNumber = userInterface.GetValidString("mobile number", BNEConsts.PhoneNumberPattern);

            while (true)
            {
                string unverifiedEmailAddress = userInterface.GetValidString("email", BNEConsts.EmailPattern);
                if (!userDatabase.Contains(unverifiedEmailAddress))
                {
                    emailAddress = unverifiedEmailAddress;
                    break;
                }
                userInterface.DisplayInputError("Email already registered");
            }

            password = userInterface.GetValidString("password", BNEConsts.PasswordPattern, BNEConsts.passwordDescription);
        }

        /// <summary>
        /// Get details and create a new standard traveller
        /// </summary>
        /// <returns>registered standard traveller</returns>
        private User RegisterStandardTraveller()
        {
            userInterface.DisplayString("Registering as a traveller.");
            RegisterStandardUser(out string name, out int age, out string phoneNumber, out string emailAddress, out string password);
            User newUser = new StandardTraveller(name, age, phoneNumber, emailAddress, password);
            userInterface.DisplayString($"Congratulations {name}. You have registered as a traveller.\n");
            return newUser;
        }

        /// <summary>
        /// Get details and create a new frequent flyer
        /// </summary>
        /// <returns>registered frequent flyer</returns>
        private User RegisterFrequentFlyer()
        {
            userInterface.DisplayString("Registering as a frequent flyer.");
            RegisterStandardUser(out string name, out int age, out string phoneNumber, out string emailAddress, out string password);
            int frequentFlyerNumber = userInterface.GetValidInt("frequent flyer number", 100000, 999999);
            int frequentFlyerPoints = userInterface.GetValidInt("current frequent flyer points", 0, 1000000);

            User newUser = new FrequentFlyer(name, age, phoneNumber, emailAddress, password, frequentFlyerNumber, frequentFlyerPoints);
            userInterface.DisplayString($"Congratulations {name}. You have registered as a frequent flyer.\n");
            return newUser;
        }

        /// <summary>
        /// Get details and create a new flight manager
        /// </summary>
        /// <returns>registered flight manager</returns>
        private User RegisterFlightManager()
        {
            userInterface.DisplayString("Registering as a flight manager.");
            RegisterStandardUser(out string name, out int age, out string phoneNumber, out string emailAddress, out string password);
            int staffID = userInterface.GetValidInt("staff id", 1000, 9000);

            User newUser = new FlightManager(name, age, phoneNumber, emailAddress, password, staffID);
            userInterface.DisplayString($"Congratulations {name}. You have registered as a flight manager.\n");
            return newUser;
        }
    }
}
