using System;

namespace PST2
{
    /// <summary>
    /// A set of constants that can be used within the program.
    ///
    /// RESPONSIBILITY: Holds the system constants.
    /// </summary>
    public static class BNEConsts
    {
        // Menu Headers
        public const string MAIN_HEADER = "\nPlease make a choice from the menu below:";

        public const string REGISTER_HEADER = "Which user type would you like to register?";

        public const string STANDARD_TRAVELLER_HEADER = $"Traveller Menu.\nPlease make a choice from the menu below:";

        public const string FREQUENT_FLYER_HEADER = "Frequent Flyer Menu.\nPlease make a choice from the menu below:";

        public const string FLIGHT_MANAGER_HEADER = "Flight Manager Menu.\nPlease make a choice from the menu below:";

        public const string AIRLINE_HEADER = "Please enter the airline:";

        public const string CITIES_HEADER = "Please enter the {0} city:";


        // Menus
        public readonly static IReadOnlyList<string> MAIN_MENU = new List<string>() {   "Login as a registered user.",
                                                                        "Register as a new user.",
                                                                        "Exit." };

        public readonly static IReadOnlyList<string> REGISTER_MENU = new List<string>() {   "A standard traveller.",
                                                                            "A frequent flyer.",
                                                                            "A flight manager." };

        public readonly static IReadOnlyList<string> STANDARD_TRAVELLER_MENU = new List<string>() { "See my details.",
                                                                                    "Change password.",
                                                                                    "Book an arrival flight.",
                                                                                    "Book a departure flight.",
                                                                                    "See flight details.",
                                                                                    "Logout." };

        public readonly static IReadOnlyList<string> FREQUENT_FLYER_MENU = new List<string>() { "See my details.",
                                                                                "Change password.",
                                                                                "Book an arrival flight.",
                                                                                "Book a departure flight.",
                                                                                "See flight details.",
                                                                                "See frequent flyer points.",
                                                                                "Logout." };

        public readonly static IReadOnlyList<string> FLIGHT_MANAGER_MENU = new List<string>() { "See my details.",
                                                                                "Change password.",
                                                                                "Create an arrival flight.",
                                                                                "Create a departure flight.",
                                                                                "Delay an arrival flight.",
                                                                                "Delay a departure flight.",
                                                                                "See the details of all flights.",
                                                                                "Logout." };

        public readonly static IReadOnlyList<string> AIRLINES_MENU = new List<string>() {   "Jetstar",
                                                                            "Qantas",
                                                                            "Regional Express",
                                                                            "Virgin",
                                                                            "Fly Pelican" };

        public readonly static IReadOnlyList<string> CITIES_MENU = new List<string>() { "Sydney",
                                                                        "Melbourne",
                                                                        "Rockhampton",
                                                                        "Adelaide",
                                                                        "Perth" };


        // Flight Info
        public readonly static Dictionary<string, string> AIRLINE_CODES = new Dictionary<string, string>()
        {
            {"Jetstar", "JST" },
            {"Qantas", "QFA" },
            {"Regional Express", "RXA" },
            {"Virgin", "VOZ" },
            {"Fly Pelican", "FRE" }
        };

        public readonly static Dictionary<string, int> CITY_POINTS = new Dictionary<string, int>()
        {
            {"Sydney", 1200 },
            {"Melbourne", 1750 },
            {"Rockhampton", 1400 },
            {"Adelaide", 1950 },
            {"Perth", 3375 }
        };

        public const string dateTimeFormat = "HH:mm dd/MM/yyyy";



        // Regex Patterns
        public const string NamePattern = @"^(?=.*[a-zA-Z])[a-zA-Z '-]*$", PhoneNumberPattern = @"^0\d{9}$",
                            EmailPattern = @"^.+@.+$", PasswordPattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$",
                            SeatColumnPattern = @"^[A-Da-d]{1}$";

        public const string passwordDescription =   "Your password must:\n" +
                                                    "-be at least 8 characters long \n" +
                                                    "-contain a number\n" +
                                                    "-contain a lowercase letter\n" +
                                                    "-contain an uppercase letter";

        // Standard values
        public const int INVALID_INT = -9999;
        public const string INVALID_STRING = "INVALID STRING";
        public const int START_INDEX = 0;
    }
}