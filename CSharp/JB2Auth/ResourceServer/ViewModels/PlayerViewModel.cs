using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceServer
{
    public class ProfileViewModel
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Displayname { get; set; }
        public string ProfileUrl { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get; set; }
        public JB2.Bowtie.Economy.JBeanWallet jBeanWallet { get; set; }

    }
}