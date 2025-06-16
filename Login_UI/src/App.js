import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import AuthForm from './EntryPoint/AuthForm';
import UserDashboard from './DashBoard/UserDashboard';
import AdminDashboard from './DashBoard/AdminDashboard';

function App() {
  return (
   <Router>
      <Routes>
        {/* Route for your login form (e.g., home page) */}
        <Route path="/" element={<AuthForm />} />

        {/* Route for your dashboard */}
        {/* IMPORTANT: The 'path' here must match the path you navigate to from AuthForm */}
        <Route path="/dashboard" element={<UserDashboard />} />
        <Route path="/admin-dashboard" element={<AdminDashboard />} />

        {/* Add other routes here as needed */}
      </Routes>
    </Router>
  );
}

export default App;
