using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorDemo.Structural
{
    public abstract class Mediator
    {
        public abstract void Send(string message, Colleague colleague);
    }
}
