using System;
using System.Collections.Generic;

namespace WebAPI.Model
{
    public partial class Candidate
    {
        public Candidate()
        {
            Vacancies = new HashSet<Vacancy>();
        }

        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? Resume { get; set; }
        public long VacancyId { get; set; }
        public int? ApplicationStatus { get; set; }
        public long? ApprovalOne { get; set; }
        public long? ApprovalTwo { get; set; }

        public virtual UserLogin? ApprovalOneNavigation { get; set; }
        public virtual UserLogin? ApprovalTwoNavigation { get; set; }
        public virtual ICollection<Vacancy> Vacancies { get; set; }
    }
}
