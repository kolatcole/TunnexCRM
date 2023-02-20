using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains.Core
{
   public  class SMSMessageData
    {
        public string Message { get; set; }
        public List<Recipient> Recipients { get; set; }
    }

    public class Recipient
    { 
        public int statusCode1 { get; set; }
        public string number { get; set; }
        public string status { get; set; }
        public string cost{ get; set; }
        public string messageId{ get; set; }
    }

    public class RequestData
    { 
        public string username { get; set; }
        public string to { get; set; }
        public string message { get; set; }
    }

}
