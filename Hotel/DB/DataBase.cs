using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DB
{
    public class DataBase
    {
        private static User21Context Instance;
        public static User21Context GetInstance()
        {
            if (Instance == null)
                Instance = new User21Context();
            return Instance;
        }
    }
}
