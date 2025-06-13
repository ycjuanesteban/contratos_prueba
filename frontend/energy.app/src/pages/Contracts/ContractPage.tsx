import GenericModal from '../../components/Modal/GenericModal';
import ContractForm from './ContractForm';
import { useContractsPage } from '../../hooks/useContractPage';

export default function ContractsPage() {
  const {
    contracts,
    loading,
    showModal,
    contractData,
    formRef,
    isEditMode,
    setContractData,
    openCreateModal,
    openEditModal,
    handleSave,
    closeModal
  } = useContractsPage();

  return (
    <>
      <h1>Contratos</h1>
      <button className="btn btn-primary mb-3" onClick={openCreateModal}>
        Crear Contrato
      </button>

      <GenericModal
        show={showModal}
        title={isEditMode ? "Editar Contrato" : "Nuevo Contrato"}
        onClose={closeModal}
        onSave={handleSave}
      >
        <ContractForm ref={formRef} onChange={setContractData} initialData={contractData} />
      </GenericModal>

      {loading ? (
        <p>Cargando contratos...</p>
      ) : (
        <table className="table table-striped">
          <thead>
            <tr>
              <th>DNI</th>
              <th>Nombre</th>
              <th>Apellido</th>
              <th>Tarifa</th>
              <th>Fecha de Contratación</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {contracts.map(contract => (
              <tr key={contract.id}>
                <td>{contract.user.dni}</td>
                <td>{contract.user.name}</td>
                <td>{contract.user.lastName}</td>
                <td>{contract.rate.name}</td>
                <td>{new Date(contract.hiringDate).toLocaleDateString()}</td>
                <td>
                  <button
                    className="btn btn-sm btn-secondary me-2"
                    onClick={() => openEditModal(contract)}
                  >
                    Editar
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
}
