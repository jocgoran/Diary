using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace Diary
{
    // Contains global variables for project.
   public static class GlobalVar
    {

        public static Dictionary<string, byte[]> dicToken = new Dictionary<string, byte[]>();

        // Static value protected by access routine.
        static DataSet _dataSet;

        // Access routine for global variable.
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
