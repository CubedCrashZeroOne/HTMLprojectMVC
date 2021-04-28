using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTMLprojectMVC.Models.Context.Entities
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1), MaxLength(100)]
        public string Name { get; set; }
    }
}
