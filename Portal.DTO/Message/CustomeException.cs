using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Message
{
    public class CustomeException:Exception
    {
        public string Errors { get; set; }
        public CustomeException()
        {
                
        }
    }

}
