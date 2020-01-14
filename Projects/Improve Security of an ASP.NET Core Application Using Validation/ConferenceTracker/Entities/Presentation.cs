using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceTracker.Entities
{
    public class Presentation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Session {0} is required.")]
        [StringLength(100,MinimumLength = 3,ErrorMessage = "Name should be at least 3 characters long, but no longer than 100 characters.")]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDateTime { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDateTime { get; set; }
        [Required]
        [StringLength(250)]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [ForeignKey("Speaker")]
        public int SpeakerId { get; set; }
        public virtual Speaker Speaker { get; set; }
    }
}
