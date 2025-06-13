import { useImperativeHandle, forwardRef } from 'react';
import * as yup from 'yup';
import { useContractForm } from '../../hooks/useContractForm';

interface Props {
  onChange: (contract: any) => void;
  initialData?: {
    userId?: string;
    rateId?: string;
    hiringDate?: string;
  };
}

export const contractSchema = yup.object({
  userId: yup.string().required('Debe seleccionar un usuario'),
  rateId: yup.string().required('Debe seleccionar una tarifa'),
  hiringDate: yup
    .string()
    .required('Debe ingresar una fecha')
    .matches(/^\d{4}-\d{2}-\d{2}$/, 'La fecha debe tener el formato YYYY-MM-DD'),
});

const ContractForm = forwardRef(({ onChange, initialData }: Props, ref) => {
  const {
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
    validate
  } = useContractForm({ initialData, onChange });


  useImperativeHandle(ref, () => ({ validate }));


  return (
    <form>
      <div className="mb-3">
        <label className="form-label">Usuario</label>
        <select
          className={`form-select ${errors.userId ? 'is-invalid' : ''}`} {...register('userId')}
          value={selectedUserId}
          onChange={e => handleUserChange(e.target.value)}
        >
          <option value="">Seleccione un usuario</option>
          {users.map(user => (
            <option key={user.id} value={user.id}>
              {user.name} {user.lastName} - {user.dni}
            </option>
          ))}
        </select>
        {errors.userId && <div className="invalid-feedback">{errors.userId.message}</div>}
      </div>

      <div className="mb-3">
        <label className="form-label">Tarifa</label>
        <select
          className={`form-select ${errors.rateId ? 'is-invalid' : ''}`} {...register('rateId')}
          value={selectedRateId}
          onChange={e => handleRateChange(e.target.value)}
        >
          <option value="">Seleccione una tarifa</option>
          {rates.map(rate => (
            <option key={rate.id} value={rate.id}>
              {rate.name}
            </option>
          ))}
        </select>
        {errors.rateId && <div className="invalid-feedback">{errors.rateId.message}</div>}
      </div>

      <div className="mb-3">
        <label className="form-label">Fecha de contratación</label>
        <input
          type="date"
          className={`form-control ${errors.hiringDate ? 'is-invalid' : ''}`} {...register('hiringDate')}
          value={selectedhiringDate}
          onChange={e => handleDateChange(e.target.value)}
        />
        {errors.hiringDate && <div className="invalid-feedback">{errors.hiringDate.message}</div>}
      </div>
    </form>
  );
});

export default ContractForm;