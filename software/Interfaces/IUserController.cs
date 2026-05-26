using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// Control users that use the system
    /// 
    /// RESPONSIBILITY: control users
    /// </summary>
    public interface IUserController
    {
        /// <summary>
        /// Process to register a new user to the system
        /// </summary>
        void RegisterUser();
    }
}
