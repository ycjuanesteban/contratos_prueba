export interface Contract {
  id: string;
  dni: string;
  firstName: string;
  lastName: string;
  rateId: string;
  startDate: string;
}

export interface ContractApiResponse {
  id: string;
  user: {
    id: string;
    name: string;
    lastName: string;
    dni: string;
  };
  rate: {
    id: string;
    name: string;
  };
  hiringDate: string;
}

export interface CreateContractRequest {
  userId: string;
  rateId: string;
  hiringDate: string;
}

export interface UpdateContractRequest {
  id: string;
  userId: string;
  rateId: string;
  hiringDate: string;
}
