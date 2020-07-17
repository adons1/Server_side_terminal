using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Server_side.Models
{
    public class Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idAction { get; set; }
        public int ActionId { get; set; }
        public string ActionDescription { get; set; }
        public DateTime Date { get; set; }
        public int PersonId { get; set; }
    }
}