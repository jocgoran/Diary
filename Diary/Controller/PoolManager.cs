using System;
using System.ComponentModel; 
using System.Collections; 
using System.Threading;

namespace Diary.Controller
{
    /// <summary>
    /// A class to manage objects in a pool. 
    ///The class is sealed to prevent further inheritence
    /// and is based on the Singleton Design.
    /// </summary>
    public sealed class PoolManager
    {
        private Hashtable objPool = new Hashtable();
        private const int POOL_SIZE = 10;
        private int objCount = 0;
        private static PoolManager poolInstance = null;

        /// <summary>
        /// Private constructor to prevent instantiation
        /// </summary>
        private PoolManager()
        {
        }

        /// <summary>
        /// Static constructor that gets 
        ///called only once during the application's lifetime.
        /// </summary>
        static PoolManager()
        {
            poolInstance = new PoolManager();
        }

        /// <summary>
        /// Static property to retrieve the instance of the Pool Manager
        /// </summary>

        public static PoolManager Instance
        {
            get
            {
                if (poolInstance != null)
                {
                    return poolInstance;
                }

                return null;
            }
        }

        /// <summary>
        /// Creates objects and adds them in the pool
        /// </summary>
        /// <param name="obj">The object type</param>
        public void CreateObjects(object obj)
        {
            object _obj = obj;
            objCount = 0;
            objPool.Clear();

            for (int objCtr = 0; objCtr < POOL_SIZE; objCtr++)
            {
                _obj = new object();
                objPool.Add(_obj, _obj);
                objCount++;
            }
        }

        /// <summary>
        /// Adds an object to the pool
        /// </summary>
        /// <param name="obj">Object to be added</param>
        /// <returns>True if success, false otherwise</returns>
        public bool AddObject(string ObjectName, dynamic obj)
        {
            if (objCount == POOL_SIZE) return false;

            objPool.Add(ObjectName, obj);
            objCount++;
            return true;
        }

        /// <summary>
        /// Releases an object from the pool
        /// </summary>
        /// <param name="obj">Object to remove from the pool</param>
        /// <returns>The object if success, null otherwise</returns>
        public void GetObject(string ObjectName, ref dynamic obj)
        {
            if (objCount == 0) obj=null;

            obj=objPool[ObjectName];    
            return;
        }

        /// <summary>
        /// Property that represents the current no of objects in the pool
        /// </summary>
        public int CurrentObjectsInPool
        {
            get
            {
                return objCount;
            }
        }

        /// <summary>
        /// Property that represents the maximum no of objects in the pool
        /// </summary>
        public int MaxObjectsInPool
        {
            get
            {
                return POOL_SIZE;
            }
        }

    } // end class
} // end namespace