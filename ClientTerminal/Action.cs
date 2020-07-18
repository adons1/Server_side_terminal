using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace ClientTerminal
{
    public class Action
    {
        public int idAction { get; set; }
        public int ActionId { get; set; }
        public string ActionDescription { get; set; }
        public DateTime Date { get; set; }
        public int PersonId { get; set; }
    }
}