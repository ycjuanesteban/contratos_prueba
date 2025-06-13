import { Navbar, Nav, Container } from 'react-bootstrap';
import { Link } from 'react-router-dom';

export default function AppNavbar() {
  return (
    <Navbar bg="dark" variant="dark" expand="lg">
      <Container>
        <Navbar.Brand as={Link} to="/">ContratosApp</Navbar.Brand>
        <Nav className="me-auto">
          <Nav.Link as={Link} to="/contracts">Contratos</Nav.Link>
          <Nav.Link as={Link} to="/users">Usuarios</Nav.Link>
          <Nav.Link as={Link} to="/rates">Tarifas</Nav.Link>
        </Nav>
      </Container>
    </Navbar>
  );
}