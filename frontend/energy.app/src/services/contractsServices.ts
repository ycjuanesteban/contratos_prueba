import { API_BASE_URL } from '../config/api';
import type { ContractApiResponse, CreateContractRequest, UpdateContractRequest } from '../models/contracts';

const CONTRACTS_URL = `${API_BASE_URL}/Contracts`;

export const contractService = {
  getAll: async (): Promise<ContractApiResponse[]> => {
    const response = await fetch(CONTRACTS_URL);
    if (!response.ok) throw new Error('Error al cargar contratos');
    return await response.json();
  },

  create: async (data: CreateContractRequest): Promise<void> => {
    const response = await fetch(CONTRACTS_URL, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Error al crear contrato: ${errorText}`);
    }
  },

  update: async (data: UpdateContractRequest): Promise<void> => {
    const response = await fetch(CONTRACTS_URL, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(`Error al actualizar contrato: ${errorText}`);
    }
  },
};
