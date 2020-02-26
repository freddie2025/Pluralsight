using Paladin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paladin.ViewModels
{
    public class Addresses
    {
        public AddressVM MainAddress { get; set; }
        public AddressVM MailingAddress { get; set; }
    }
}