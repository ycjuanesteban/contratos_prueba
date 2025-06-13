import { Modal, Button } from 'react-bootstrap';
import type { ReactNode } from 'react';

interface Props {
  show: boolean;
  title: string;
  onClose: () => void;
  onSave: () => void;
  children: ReactNode;
}

export default function GenericModal({ show, title, onClose, onSave, children }: Props) {
  return (
    <Modal show={show} onHide={onClose}>
      <Modal.Header closeButton>
        <Modal.Title>{title}</Modal.Title>
      </Modal.Header>
      <Modal.Body>{children}</Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={onClose}>Cancelar</Button>
        <Button variant="primary" onClick={onSave}>Guardar</Button>
      </Modal.Footer>
    </Modal>
  );
}
