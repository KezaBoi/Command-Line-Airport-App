using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// Database for users 
    /// 
    /// RESPONSIBILITY: Save account information for users
    /// </summary>
    public interface IUserDatabase
    {
        /// <summary>
        /// Retrieve a read only version of the database, closed to modification
        /// </summary>
        /// <returns>read only user database</returns>
        IReadOnlyDictionary<int, User> GetReadOnlyDatabase();

        /// <summary>
        /// Add a new user to the database
        /// </summary>
        /// <param name="newUser">user to be added to the database</param>
        void AddUser(User newUser);

        /// <summary>
        /// Check if database contains an email address already
        /// </summary>
        /// <param name="email">email address to check</param>
        /// <returns>true if email exists in database, false otherwise</returns>
        bool Contains(string email);

        /// <summary>
        /// Retrieve a count of the users currently in the database
        /// </summary>
        /// <returns>count of users in database</returns>
        int Count();

        /// <summary>
        /// Get the account associated with an email address
        /// </summary>
        /// <param name="hashedEmail">hashcode of email address</param>
        /// <returns>account associated with email address</returns>
        User GetUser(int hashedEmail);
    }
}
