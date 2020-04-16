using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hook.Data.Models
{
    public class WorkItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Product { get; set; }
        //public DeveloperType Developer { get; set; }
        public string Developer { get; set; }
    }
}
