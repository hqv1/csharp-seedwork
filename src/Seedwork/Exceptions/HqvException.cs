using System;

namespace Hqv.Seedwork.Exceptions
{    
    public class HqvException : Exception
    {        
        public HqvException()
        {            
        }

        public HqvException(string message) : base(message)
        {            
        }

        public HqvException(string message, Exception inner) : base(message, inner)
        {
        }
        
        public bool IsTransient { get; set; }
    }
}