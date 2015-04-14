using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Diary
{
    /// <summary>
    /// Contains global variables for project.
    /// </summary>
    public static class GlobalVar
    {

        /// <summary>
        /// Static value protected by access routine.
        /// </summary>
        static DataSet _dataSet;

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static DataSet DataSet
        {
            get
            {
                return _dataSet;
            }
            set
            {
                _dataSet = value;
            }
        }
    }
}
