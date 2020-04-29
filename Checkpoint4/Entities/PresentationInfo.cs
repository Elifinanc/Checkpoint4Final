using System;

namespace Checkpoint4
{
    public class PresentationInfo
    {
        public Guid PresentationInfoId { get; set; }
        public string PresentationId { get; set; }
        public string ShowName { get; set; }
        public DateTime PresentationDate { get; set; }
        public int AvailableSeatsCount { get; set; }
        public int TotalSeatCount { get; set; }
        public int ReservedSeatCount { get; set; }

        public PresentationInfo() { }
    }
}
