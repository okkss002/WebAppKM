using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppKM.Models
{
    [Table("OcsbDoc")]
    public class OcsbDoc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int DocID { get; set; }

        [StringLength(4)]
        public string DocType { get; set; }

        [StringLength(4)]
        public string DocGroup { get; set; }

        [StringLength(400)]
        public string DocName { get; set; }

        public string Details { get; set; }

        public string Keyword { get; set; }

        [StringLength(400)]
        public string FileName { get; set; }

        public string Links { get; set; }

        public bool Status { get; set; }

    }
}