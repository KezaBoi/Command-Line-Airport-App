using System;

namespace PST2
{
    /// <summary>
    /// Database for users
    /// 
    /// RESPONSIBILITY: Hold accounts
    /// </summary>
    public class UserDatabase : IUserDatabase
    {
        private static Dictionary<int, User> userDatabase = new Dictionary<int, User>();

        /// <summary>
        /// Contstructor for user database
        /// </summary>
        public UserDatabase()
        {
        }

        // Properties


        // Methods
        /// <inheritdoc/>
        public IReadOnlyDictionary<int, User> GetReadOnlyDatabase()
        {
            return userDatabase;
        }

        /// <inheritdoc/>
        public void AddUser(User newUser)
        {
            int hashedEmail = newUser.EmailAddress.GetHashCode();
            userDatabase.Add(hashedEmail, newUser);
        }

        /// <inheritdoc/>
        public bool Contains(string email)
        {
            int hashedEmail = email.GetHashCode();
            if (userDatabase.ContainsKey(hashedEmail))
            {
                return true;
            }
            else { return false; }
        }

        /// <inheritdoc/>
        public int Count()
        {
            return userDatabase.Count();
        }

        /// <inheritdoc/>
        public User GetUser(int hashedEmail)
        {
            return userDatabase[hashedEmail];
        }
    }
}