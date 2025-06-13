import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from 'yup';
import { useEffect, useImperativeHandle, forwardRef } from 'react';

interface Props {
  onChange: (user: { name: string; lastName: string; dni: string }) => void;
}

const schema = yup.object({
  name: yup.string().required('El nombre es obligatorio'),
  lastName: yup.string().required('El apellido es obligatorio'),
  dni: yup
    .string()
    .matches(/^\d{8}[A-Z]$/, 'El DNI debe tener 8 dígitos seguidos de una letra mayúscula')
    .required('El DNI es obligatorio'),
});

type FormData = yup.InferType<typeof schema>;

const UserForm = forwardRef(({ onChange }: Props, ref) => {
  const {
    register,
    watch,
    trigger,
    getValues,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(schema),
    mode: 'onChange'
  });

  const name = watch('name');
  const lastName = watch('lastName');
  const dni = watch('dni');

  useEffect(() => {
    onChange({ name, lastName, dni });
  }, [name, lastName, dni]);

  useImperativeHandle(ref, () => ({
    async validate() {
      const valid = await trigger();
      if (!valid) return null;
      return getValues();
    }
  }));

  return (
    <form>
      <div className="mb-3">
        <label className="form-label">Nombre</label>
        <input className={`form-control ${errors.name ? 'is-invalid' : ''}`} {...register('name')} />
        {errors.name && <div className="invalid-feedback">{errors.name.message}</div>}
      </div>

      <div className="mb-3">
        <label className="form-label">Apellido</label>
        <input className={`form-control ${errors.lastName ? 'is-invalid' : ''}`} {...register('lastName')} />
        {errors.lastName && <div className="invalid-feedback">{errors.lastName.message}</div>}
      </div>

      <div className="mb-3">
        <label className="form-label">DNI</label>
        <input className={`form-control ${errors.dni ? 'is-invalid' : ''}`} {...register('dni')} />
        {errors.dni && <div className="invalid-feedback">{errors.dni.message}</div>}
      </div>
    </form>
  );
});

export default UserForm;