namespace WebAPI.DTO
{
    public class Applicant
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public long VacancyId { get; set; }
        public string VacancyName { get; set; }
        public string? ImgPath { get; set; }
    }

    public class ApplicantInfo
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public long VacancyId { get; set; }
        public string VacancyName { get; set; }
        public long? ApprovalOne { get; set; }
        public long? ApprovalTwo { get; set; }
    }
}
