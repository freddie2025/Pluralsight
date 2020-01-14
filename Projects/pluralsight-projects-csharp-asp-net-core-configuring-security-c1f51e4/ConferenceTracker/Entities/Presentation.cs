using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConferenceTracker.Entities
{
    public class Presentation
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }

        [ForeignKey("Speaker")]
        public int SpeakerId { get; set; }
        public virtual Speaker Speaker { get; set; }
    }
}
