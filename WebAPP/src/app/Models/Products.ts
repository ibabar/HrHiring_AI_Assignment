export class Products {
  productId: any;
  productName?: string;
  productCost?: number;
  productDescription?: string;
  productStock?: number;
}

export class Position {
  Id: any;
  OpenPosition?: string;
  ApprovalOne?: number;
  ApprovalTwo?: number;
  ApprovedCandidate?: number;
}

export class Applicant {
  Id: any;
  Name?: string;
  Email?: number;
  MobileNumber?: number;
  Resume?: string;
  VacancyId?: number;
}

export class Approval {
  VacancyId?: number;
  CandidateID?: number;
}
