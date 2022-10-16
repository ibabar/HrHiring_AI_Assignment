using System;
using System.Collections.Generic;

namespace WebAPI.Model
{
    public partial class Vacancy
    {
        public long Id { get; set; }
        public string OpenPosition { get; set; } = null!;
        public long? ApprovedCandidate { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Candidate? ApprovedCandidateNavigation { get; set; }
    }
}
