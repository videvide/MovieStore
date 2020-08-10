using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Lib.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get;  set; }
        [Required]
        [MaxLength(100)]
        public string Director { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(100)]
        public string Rated { get; set; }
        [Required]
        [MaxLength(100)]
        public string Genre { get; set; }
        [Required]
        [MaxLength(255)]
        public string Plot { get; set; }
        [Required]
        [MaxLength(255)]
        public string Poster { get; set; }
        [Required]
        public float ImdbRating { get; set; }
        [Required]
        public string ImdbID { get; set; }
    }
}