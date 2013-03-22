using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hero.IntegrationTest.Models
{
    public class ToDo
    {
        public string Name { get; set; }
        public IEnumerable<string> Items { get; set; }
    }
}