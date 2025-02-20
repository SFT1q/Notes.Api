using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.DataBase
{
    public class DbInitialize
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
