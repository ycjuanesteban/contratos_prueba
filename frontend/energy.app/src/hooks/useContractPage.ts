import { useEffect, useRef, useState } from 'react';
import { contractService } from '../services/contractsServices';
import type { ContractApiResponse } from '../models/contracts';

export function useContractsPage() {
  const [contracts, setContracts] = useState<ContractApiResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [contractData, setContractData] = useState<any>({});
  const [isEditMode, setIsEditMode] = useState(false);
  const [editingContractId, setEditingContractId] = useState<string | null>(null);
  const formRef = useRef<any>(null);

  const fetchContracts = async () => {
    try {
      setLoading(true);
      const data = await contractService.getAll();
      setContracts(data);
    } catch (error) {
      console.error('Error al cargar contratos:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchContracts();
  }, []);

  const openCreateModal = () => {
    setContractData({});
    setEditingContractId(null);
    setIsEditMode(false);
    setShowModal(true);
  };

  const openEditModal = (contract: ContractApiResponse) => {
    setContractData({
      userId: contract.user.id,
      rateId: contract.rate.id,
      hiringDate: contract.hiringDate,
    });
    setEditingContractId(contract.id);
    setIsEditMode(true);
    setShowModal(true);
  };

  const closeModal = () => {
    setShowModal(false);
    setIsEditMode(false);
    setEditingContractId(null);
    setContractData({});
  };

  const handleSave = async () => {
    try {
      const data = await formRef.current?.validate();
      if (!data) return;

      if (isEditMode && editingContractId) {
        await contractService.update({
          id: editingContractId,
          userId: contractData.userId,
          rateId: contractData.rateId,
          hiringDate: contractData.hiringDate
        });
      } else {
        const hiringDateISO = new Date(contractData.hiringDate).toISOString();
        await contractService.create({
          userId: contractData.userId,
          rateId: contractData.rateId,
          hiringDate: hiringDateISO,
        });
      }

      closeModal();
      fetchContracts();
    } catch (error) {
      alert('Error al guardar el contrato');
      console.error(error);
    }
  };

  return {
    contracts,
    loading,
    showModal,
    contractData,
    formRef,
    isEditMode,
    setContractData,
    openCreateModal,
    openEditModal,
    closeModal,
    handleSave,
  };
}
