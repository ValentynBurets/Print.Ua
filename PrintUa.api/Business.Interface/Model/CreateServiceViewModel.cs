using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Contract.Model
{
    public class CreateServiceViewModel
    {
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public string MaterialName { get; set; }
        public string FormatName { get; set; }
    }
}
