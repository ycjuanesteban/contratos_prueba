import { useEffect, useRef, useState } from 'react';
import type { RateApiResponse } from '../../models/rate';
import { rateService } from '../../services/rateServices';
import GenericModal from '../../components/Modal/GenericModal';
import RateForm from './RateForm';

export default function RatesPage() {
  const [rates, setRates] = useState<RateApiResponse[]>([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [formData, setFormData] = useState({ name: '' });
  const formRef = useRef<any>(null);

  const fetchRates = async () => {
    try {
      setLoading(true);
      const data = await rateService.getAll();
      setRates(data);
    } catch (err) {
      alert('Error al cargar tarifas');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchRates();
  }, []);

  const handleSave = async () => {
    try {
      const data = await formRef.current?.validate();
      if (!data) return;

      await rateService.create(formData);
      setShowModal(false);
      setFormData({ name: '' });
      fetchRates();
    } catch (e) {
      alert('Error al guardar la tarifa');
    }
  };

  return (
    <>
      <h1>Tarifas</h1>
      <button className="btn btn-primary mb-3" onClick={() => setShowModal(true)}>
        Crear tarifa
      </button>

      <GenericModal
        show={showModal}
        title="Nueva Tarifa"
        onClose={() => setShowModal(false)}
        onSave={handleSave}
      >
        <RateForm ref={formRef} onChange={setFormData} />
      </GenericModal>

      {loading ? (
        <p>Cargando contratos...</p>
      ) : (
        <table className="table table-striped">
          <thead>
            <tr>
              <th>Nombre</th>
            </tr>
          </thead>
          <tbody>
            {rates.map((rate) => (
              <tr key={rate.id}>
                <td>{rate.name}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
}
