

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sylphyr
{
    class SingleTon <T> where T : class, new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    return instance = new T();
                }
            }
        }
    }
}
