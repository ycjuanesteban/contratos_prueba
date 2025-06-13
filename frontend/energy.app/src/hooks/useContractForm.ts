import { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { userService } from '../services/userServices';
import { rateService } from '../services/rateServices';
import { contractSchema } from '../pages/Contracts/ContractForm';
import type { UserApiResponse } from '../models/user';
import type { RateApiResponse } from '../models/rate';

export type ContractFormData = {
  userId: string;
  rateId: string;
  hiringDate: string;
};

interface Props {
  initialData?: Partial<ContractFormData>;
  onChange: (data: ContractFormData) => void;
}

export function useContractForm({ initialData, onChange }: Props) {
  const [users, setUsers] = useState<UserApiResponse[]>([]);
  const [rates, setRates] = useState<RateApiResponse[]>([]);

  const [selectedUserId, setSelectedUserId] = useState('');
  const [selectedRateId, setSelectedRateId] = useState('');
  const [selectedhiringDate, setHiringDate] = useState('');

  const {
    register,
    trigger,
    getValues,
    formState: { errors },
  } = useForm<ContractFormData>({
    resolver: yupResolver(contractSchema),
    defaultValues: {
      userId: '',
      rateId: '',
      hiringDate: '',
    },
  });

  useEffect(() => {
    setSelectedUserId(initialData?.userId || '');
    setSelectedRateId(initialData?.rateId || '');
    setHiringDate(initialData?.hiringDate?.split('T')[0] || '');
  }, [initialData]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [usersData, ratesData] = await Promise.all([
          userService.getAll(),
          rateService.getAll()
        ]);
        setUsers(usersData);
        setRates(ratesData);
      } catch (error) {
        console.error('Error cargando datos:', error);
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    if (initialData) {
      setSelectedUserId(initialData.userId || '');
      setSelectedRateId(initialData.rateId || '');
      setHiringDate(initialData.hiringDate?.split('T')[0] || '');
    }
  }, [initialData]);

  const handleUserChange = (value: string) => {
    setSelectedUserId(value);
    onChange({
      userId: value,
      rateId: selectedRateId,
      hiringDate: selectedhiringDate,
    });
  };

  const handleRateChange = (value: string) => {
    setSelectedRateId(value);
    onChange({
      userId: selectedUserId,
      rateId: value,
      hiringDate: selectedhiringDate,
    });
  };

  const handleDateChange = (value: string) => {
    setHiringDate(value);
    onChange({
      userId: selectedUserId,
      rateId: selectedRateId,
      hiringDate: value,
    });
  };

  return {
    users,
    rates,
    register,
    errors,
    handleUserChange,
    handleRateChange,
    handleDateChange,
    selectedUserId,
    selectedRateId,
    selectedhiringDate,
    validate: async () => {
      const isValid = await trigger();
      return isValid ? getValues() : null;
    },
  };
}
