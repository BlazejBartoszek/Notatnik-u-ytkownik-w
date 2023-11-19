using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notatnik_użytkowników.Models
{
    [Table("Users")]
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [DisplayName("Imię")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [MaxLength(50)]
        public string Name { get; set; }

        [DisplayName("Nazwisko")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [MaxLength(150)]
        public string Surname { get; set; }

        [DisplayName("Data urodzenia")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime DateOfBirth { get; set; }

        [DisplayName("Płeć")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public Gender Gender { get; set; }

        public string Title { get; set; }

        public int Age { get; set; }

        [DisplayName("Dodatkowe informacje")]
        [MaxLength(200)]
        public string AdditionalAttribute { get; set; }
        [DisplayName("Raport:")]
        public bool UserRaport { get; set; }

        public int SetAge()
        {
            return DateTime.Today.Year - DateOfBirth.Year;
        }
    }
}