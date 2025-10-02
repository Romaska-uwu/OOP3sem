using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    public delegate string Del(int a);

    class Cl1
    {
        public event Del Event;
        public string Metod(int a)
        {
            return Event?.Invoke(a);
        }

        


    }
}
