import { API_BASE_URL } from "../config/api";
import type { CreateRateRequest, RateApiResponse } from "../models/rate";

const RATES_URL = `${API_BASE_URL}/Rates`;

export const rateService = {
  getAll: async (): Promise<RateApiResponse[]> => {
    const response = await fetch(RATES_URL);
    if (!response.ok) throw new Error('Error al cargar tarifas');
    return await response.json();
  },

  create: async (data: CreateRateRequest): Promise<void> => {
    const response = await fetch(RATES_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Error al crear tarifa: ${errorText}`);
    }
  },
};
