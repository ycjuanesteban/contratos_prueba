export interface Rate {
  id: string;
  name: string;
  price: number;
}

export interface RateApiResponse {
  id: string;
  name: string;
}

export interface CreateRateRequest {
  name: string;
}
