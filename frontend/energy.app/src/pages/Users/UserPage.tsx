import { useEffect, useRef, useState } from 'react';
import { userService } from '../../services/userServices';
import UserForm from './UserForm';
import type { UserApiResponse } from '../../models/user';
import GenericModal from '../../components/Modal/GenericModal';

export default function UsersPage() {
  const [users, setUsers] = useState<UserApiResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [formData, setFormData] = useState({ name: '', lastName: '', dni: '' });
  const formRef = useRef<any>(null);

  const fetchUsers = async () => {
    try {
      setLoading(true);
      const data = await userService.getAll();
      setUsers(data);
    }
    catch (error) {
      console.error('Error al cargar usuarios:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  const handleSave = async () => {
    try {
      const data = await formRef.current?.validate();
      if (!data) return;

      await userService.create(formData);
      setShowModal(false);
      setFormData({ name: '', lastName: '', dni: '' });
      fetchUsers();
    } catch (e) {
      alert('Error al guardar el usuario');
    }
  };

  return (
    <>
      <h1>Usuarios</h1>
      <button className="btn btn-primary mb-3" onClick={() => setShowModal(true)}>
        Crear Usuario
      </button>

      <GenericModal
        show={showModal}
        title="Nuevo Usuario"
        onClose={() => setShowModal(false)}
        onSave={handleSave}
      >
        <UserForm ref={formRef} onChange={setFormData} />
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
            </tr>
          </thead>
          <tbody>
            {users.map(u => (
              <tr key={u.id}>
                <td>{u.dni}</td>
                <td>{u.name}</td>
                <td>{u.lastName}</td>
              </tr>
            ))}
          </tbody>
        </table>)}
    </>
  );
}
