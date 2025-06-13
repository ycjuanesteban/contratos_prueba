import { useForm } from 'react-hook-form';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import { useEffect, useImperativeHandle, forwardRef } from 'react';

interface Props {
  onChange: (rate: { name: string }) => void;
}

const schema = yup.object({
  name: yup.string().required('El nombre es obligatorio'),
});

type FormData = yup.InferType<typeof schema>;

const RateForm = forwardRef(({ onChange }: Props, ref) => {
  const {
    register,
    watch,
    trigger,
    getValues,
    formState: { errors },
  } = useForm<FormData>({
    resolver: yupResolver(schema),
    mode: 'onChange',
  });

  const name = watch('name');

  useEffect(() => {
    onChange({ name });
  }, [name]);

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
        <label className="form-label">Nombre de tarifa</label>
        <input
          className={`form-control ${errors.name ? 'is-invalid' : ''}`}
          {...register('name')}
        />
        {errors.name && <div className="invalid-feedback">{errors.name.message}</div>}
      </div>
    </form>
  );
});

export default RateForm;