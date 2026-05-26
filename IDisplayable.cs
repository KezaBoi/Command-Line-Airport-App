using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PST2
{
    /// <summary>
    /// An object whos contents is displayable to the user
    /// </summary>
    public interface IDisplayable
    {
        /// <summary>
        /// Return a string of the objects details to be printed to the user
        /// </summary>
        /// <returns>string of details</returns>
        string GetDetails();
    }
}
