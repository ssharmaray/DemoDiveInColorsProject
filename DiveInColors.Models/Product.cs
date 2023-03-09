using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DiveInColors.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Dimension { get; set; }
        
        public string Description { get; set; }

        public string Artist { get; set; }

        [Required]
        [Display(Name = "List Price")]
        [Range(1, 10000, ErrorMessage = "List Price order must be between 1 and 10000 only!")]       
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 10000, ErrorMessage = "List Price order must be between 1 and 10000 only!")]        
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 51-100")]
        [Range(1, 10000, ErrorMessage = "List Price order must be between 1 and 10000 only!")]        
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 10000, ErrorMessage = "List Price order must be between 1 and 10000 only!")]
        public double Price100 { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public bool InStock { get; set; }


        [ValidateNever]
        public string ImageUrl { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [Required]
        [Display(Name ="Cover Type")]
        public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]
        [ValidateNever]
        public CoverType CoverType { get; set; }

    }
}
