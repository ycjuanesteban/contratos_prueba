import { API_BASE_URL } from "../config/api";
import type { CreateUserRequest, UserApiResponse } from "../models/user";

const USERS_URL = `${API_BASE_URL}/Users`;

export const userService = {
  getAll: async (): Promise<UserApiResponse[]> => {
    const res = await fetch(USERS_URL);
    if (!res.ok) throw new Error('Error al obtener usuarios');
    return await res.json();
  },

  create: async (data: CreateUserRequest): Promise<void> => {
    const res = await fetch(USERS_URL, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    });
    if (!res.ok) {
      const error = await res.text();
      throw new Error(`Error al crear usuario: ${error}`);
    }
  },
};
