import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AppNavbar from './components/Navbar/AppNavBar';
import ContractsPage from './pages/Contracts/ContractPage';
import UsersPage from './pages/Users/UserPage';
import RatesPage from './pages/Rates/RatePage';

export default function App() {
  return (
    <Router>
      <AppNavbar />
      <div className="container mt-4">
        <Routes>
          <Route path="/contracts" element={<ContractsPage />} />
          <Route path="/users" element={<UsersPage />} />
          <Route path="/rates" element={<RatesPage />} />
          <Route path="/" element={<div>Bienvenido Próxima Energía</div>} />
        </Routes>
      </div>
    </Router>
  );
}
