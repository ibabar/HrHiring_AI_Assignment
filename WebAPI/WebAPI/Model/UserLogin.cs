using System;
using System.Collections.Generic;

namespace WebAPI.Model
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            CandidateApprovalOneNavigations = new HashSet<Candidate>();
            CandidateApprovalTwoNavigations = new HashSet<Candidate>();
        }

        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Candidate> CandidateApprovalOneNavigations { get; set; }
        public virtual ICollection<Candidate> CandidateApprovalTwoNavigations { get; set; }
    }
}
