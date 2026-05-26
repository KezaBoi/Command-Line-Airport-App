using System;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PST2
{
    /// <summary>
    /// This class represents all users at BNE, specific user types inherit from it
    /// 
    /// RESPONSIBILITY: Hold information relating to any user at BNE
    /// </summary>
    public abstract class User : IDisplayable
	{
        // Fields
		protected string name;
		protected string emailAddress;
		protected string password;
		protected int age;
		protected string phoneNumber;


		// Constructor
        /// <summary>
        /// Contstructor for a standard user at BNE
        /// </summary>
        /// <param name="name">Users name</param>
        /// <param name="age">Users age</param>
        /// <param name="phoneNumber">Users phone number</param>
        /// <param name="emailAddress">Users email address</param>
        /// <param name="password">Users password</param>
        public User(string name, int age, string phoneNumber, string emailAddress, string password)
        {
            this.name = name;
            this.age = age;
            this.phoneNumber = phoneNumber;
            this.emailAddress = emailAddress;
            this.password = password;
        }
        
     
		// Properties
        /// <summary>
        /// Getter for users name
        /// </summary>
		public string Name
		{
			get { return name; }
		}

        /// <summary>
        /// Getter for users email address
        /// </summary>
        public string EmailAddress
        {
            get { return emailAddress; }
        }

        /// <summary>
        /// Gettter and private setter for users password
        /// </summary>
        public string Password
        {
            get { return password; }
            private set { password = value; }
        }

        /// <summary>
        /// Getter for users age
        /// </summary>
        public int Age
        {
            get { return age; }
        }

        /// <summary>
        /// Getter for users phone number
        /// </summary>
        public string PhoneNumber
        {
            get { return phoneNumber; }
        }

        // Methods
        /// <summary>
        /// Retrive account details for a user
        /// </summary>
        /// <returns>formatted account details</returns>
        public virtual string GetDetails()
        {
            return  $"Your details.\n" +
                    $"Name: {Name}\n" +
                    $"Age: {Age}\n" +
                    $"Mobile phone number: {PhoneNumber}\n" +
                    $"Email: {EmailAddress}";
        }

        /// <summary>
        /// Change the password for a user of the system
        /// </summary>
        /// <param name="userInterface">UI used in program</param>
        public void ChangePassword(IUserInterface userInterface)
        {
            while (true)
            {
                userInterface.DisplayString("Please enter your current password.");
                string currentPassword = userInterface.GetString();
                if (currentPassword != Password)
                {
                    userInterface.DisplayInputError("Entered password does not match existing password");
                }
                else { break; }
            }

            while (true)
            {
                userInterface.DisplayString("Please enter your new password.");
                string newPassword = userInterface.GetString();
                Password = newPassword;
                if (Password == newPassword)
                {
                    return;
                }
            }
        }

    }
}