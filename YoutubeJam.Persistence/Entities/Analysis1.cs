using System;
using System.ComponentModel.DataAnnotations;

namespace YoutubeJam.Persistence.Entities
{
    public class Analysis1
    {
        [Key]
        public int Anal1ID { get; set; }

        public Creator Creatr { get; set; }
        public Video Vid { get; set; }

        public decimal SentAve { get; set; }
        public DateTime AnalDate { get; set; }
    }
}