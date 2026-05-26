using System;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace PST2
{
    /// <summary>
    /// Controls the rest of the program. Acts as an
    /// intermediary to the user interface and the backend data.
    ///
    /// RESPONSIBILITY: Controls the program via connecting the UI with the backend. 
    /// </summary>
    /// <param name="userInterface">UI used throughout program</param>
    /// <param name="BNEMenu">Menu used throughout program</param>
    /// <param name="flightsDatabase">Flight database program uses</param>
    /// <param name="userDatabase">User database program uses</param>
    /// <param name="userController">Controller for registering users</param>
    /// <param name="bookingService">Service for booking users on flights</param>
    /// <param name="managmentService">Service for managing flights</param>
    public class BNEController (IUserInterface userInterface, IMenu BNEMenu, IFlightsDatabase flightsDatabase, IUserDatabase userDatabase, IUserController userController, IBookFlights bookingService, IManageFlights managmentService)
	{
        // Methods
        /// <summary>
        /// Runs the entire program
        /// </summary>
        public void Run()
        {
            const int LOGIN_USER = 0, REGISTER_USER = 1, EXIT = 2;
            bool appRunning = true;


            BNEMenu.DisplayHeader();

            while (appRunning)
            {
                BNEMenu.DisplayMenu(BNEConsts.MAIN_HEADER, BNEConsts.MAIN_MENU);
                int option = BNEMenu.GetMenuOption(BNEConsts.MAIN_MENU.Count);


                switch (option)
                {
                    case LOGIN_USER:
                        try
                        {
                            User currentUser = BNEMenu.LoginUser(userDatabase);
                            ProcessCurrentUser(currentUser);
                        }
                        catch (InvalidOperationException ex) 
                        { 
                            userInterface.DisplayAuthenticationError(ex.Message); 
                        }
                        break;

                    case REGISTER_USER:
                        try 
                        { 
                            userController.RegisterUser(); 
                        }
                        catch (InvalidOperationException ex) 
                        { 
                            userInterface.DisplayInputError(ex.Message); 
                        }
                        break;

                    case EXIT:
                        appRunning = false;
                        break;

                    default:
                        userInterface.DisplayInputError("Not a main menu choice");
                        break;
                }
            }



            userInterface.DisplayString("Thank you. Safe travels.");
        }

        /// <summary>
        /// Method to process the current user based on their type
        /// </summary>
        /// <param name="currentUser">account currently being processed</param>
        private void ProcessCurrentUser(User currentUser)
        {
            bool processingUser = true;
            while (processingUser)
            {
                try
                {
                    if (currentUser is StandardTraveller standardTraveller)
                    {
                        processingUser = ProcessStandardTraveller(standardTraveller);
                    }
                    else if (currentUser is FrequentFlyer frequentFlyer)
                    {
                        processingUser = ProcessFrequentFlyer(frequentFlyer);
                    }
                    else if (currentUser is FlightManager flightManager)
                    {
                        processingUser = ProcessFlightManager(flightManager);
                    }
                    else
                    {
                        throw new InvalidOperationException("Current user does not have an implemented processing function");
                    }
                }
                catch (Exception ex)
                {
                    userInterface.DisplayAuthenticationError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Method to process a standard travellers menu
        /// </summary>
        /// <param name="standardTraveller">user being processed</param>
        /// <returns>false if user logged out, true otherwise</returns>
        private bool ProcessStandardTraveller(StandardTraveller standardTraveller)
        {
            const int SEE_DETAILS = 0, CHANGE_PASSWORD = 1, BOOK_ARRIVAL = 2, BOOK_DEPARTURE = 3, FLIGHT_DETAILS = 4, LOGOUT = 5;

            BNEMenu.DisplayMenu(BNEConsts.STANDARD_TRAVELLER_HEADER, BNEConsts.STANDARD_TRAVELLER_MENU);
            int menuChoice = BNEMenu.GetMenuOption(6);

            switch (menuChoice)
            {
                case SEE_DETAILS:
                    BNEMenu.PrintDetails(standardTraveller);
                    break;

                case CHANGE_PASSWORD:
                    standardTraveller.ChangePassword(userInterface);
                    break;

                case BOOK_ARRIVAL:
                    while (true)
                    {
                        try
                        {
                            string successMessage = standardTraveller.BookFlight(true, bookingService);
                            userInterface.DisplayString(successMessage);
                            break;
                        }
                        catch (ArgumentException ex)
                        {
                            userInterface.DisplayInputError(ex.Message);
                        }
                    }
                    break;
                case BOOK_DEPARTURE:
                    while (true)
                    {
                        try
                        {
                            string successMessage = standardTraveller.BookFlight(false, bookingService);
                            userInterface.DisplayString(successMessage);
                            break;
                        }
                        catch (ArgumentException ex)
                        {
                            userInterface.DisplayInputError(ex.Message);
                        }
                    }
                    break;

                case FLIGHT_DETAILS:
                    string premadeStringFlightDetails = standardTraveller.GetFlightDetails();
                    userInterface.DisplayString(premadeStringFlightDetails);
                    break;

                case LOGOUT:
                    return false;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// Method to process a frequent flyers menu
        /// </summary>
        /// <param name="frequentFlyer">user being processed</param>
        /// <returns>false if user logged out, true otherwise</returns>
        private bool ProcessFrequentFlyer(FrequentFlyer frequentFlyer)
        {
            const int SEE_DETAILS = 0, CHANGE_PASSWORD = 1, BOOK_ARRIVAL = 2, BOOK_DEPARTURE = 3, FLIGHT_DETAILS = 4, SEE_POINTS = 5, LOGOUT = 6;

            BNEMenu.DisplayMenu(BNEConsts.FREQUENT_FLYER_HEADER, BNEConsts.FREQUENT_FLYER_MENU);
            int menuChoice = BNEMenu.GetMenuOption(7);

            switch (menuChoice)
            {
                case SEE_DETAILS:
                    BNEMenu.PrintDetails(frequentFlyer);
                    break;

                case CHANGE_PASSWORD:
                    frequentFlyer.ChangePassword(userInterface);
                    break;

                case BOOK_ARRIVAL:
                    while (true)
                    {
                        try
                        {
                            string successMessage = frequentFlyer.BookFlight(true, bookingService);
                            userInterface.DisplayString(successMessage);
                            break;
                        }
                        catch (ArgumentException ex)
                        {
                            userInterface.DisplayInputError(ex.Message);
                        }
                    }
                    break;

                case BOOK_DEPARTURE:
                    while (true)
                    {
                        try
                        {
                            string successMessage = frequentFlyer.BookFlight(false, bookingService);
                            userInterface.DisplayString(successMessage);
                            break;
                        }
                        catch (ArgumentException ex)
                        {
                            userInterface.DisplayInputError(ex.Message);
                        }
                    }
                    break;

                case FLIGHT_DETAILS:
                    string premadeStringFlightDetails = frequentFlyer.GetFlightDetails();
                    userInterface.DisplayString(premadeStringFlightDetails);
                    break;

                case SEE_POINTS:
                    string premadeStringPointsDetails = frequentFlyer.GetPointsDetails();
                    userInterface.DisplayString(premadeStringPointsDetails);
                    break;

                case LOGOUT:
                    return false;

                default:
                    break;

            }
            return true;
            
        }

        /// <summary>
        /// Method to process a flight managers menu
        /// </summary>
        /// <param name="flightManager">user being processed</param>
        /// <returns>false if user logged out, true otherwise</returns>
        private bool ProcessFlightManager(FlightManager flightManager)
        {
            const int SEE_DETAILS = 0, CHANGE_PASSWORD = 1, CREATE_ARRIVAL = 2, CREATE_DEPARTURE = 3, DELAY_ARRIVAL = 4, DELAY_DEPARTURE = 5, FLIGHT_DETAILS = 6, LOGOUT = 7;
            string successMessage;
            BNEMenu.DisplayMenu(BNEConsts.FLIGHT_MANAGER_HEADER, BNEConsts.FLIGHT_MANAGER_MENU);
            int menuChoice = BNEMenu.GetMenuOption(8);

            switch (menuChoice)
            {
                case SEE_DETAILS:
                    BNEMenu.PrintDetails(flightManager);
                    break;

                case CHANGE_PASSWORD:
                    flightManager.ChangePassword(userInterface);
                    break;

                case CREATE_ARRIVAL:
                    successMessage = flightManager.CreateFlight(managmentService, flightsDatabase, true);
                    userInterface.DisplayString(successMessage);
                    break;

                case CREATE_DEPARTURE:
                    successMessage = flightManager.CreateFlight(managmentService, flightsDatabase, false);
                    userInterface.DisplayString(successMessage);
                    break;

                case DELAY_ARRIVAL:
                    managmentService.DelayFlight(true);
                    break;

                case DELAY_DEPARTURE:
                    managmentService.DelayFlight(false);
                    break;

                case FLIGHT_DETAILS:
                    BNEMenu.PrintDetails(flightsDatabase);
                    break;

                case LOGOUT:
                    return false;

                default:
                    break;
            }
            return true;

        }


    }
}