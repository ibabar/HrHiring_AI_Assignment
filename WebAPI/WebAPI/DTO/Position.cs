namespace WebAPI.DTO
{
    public class Position
    {
        public long Id { get; set; }
        public string OpenPosition { get; set; } = null!;
        public long? ApprovalOne { get; set; }
        public long? ApprovalTwo { get; set; }
        public long? ApprovedCandidate { get; set; }
    }
}
