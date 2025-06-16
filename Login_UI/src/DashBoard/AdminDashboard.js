import React from 'react';
import { useLocation } from 'react-router-dom';
import '../App.css';
import { useLogout } from '../utill/useLogout';

export default function AdminDashboard() {
    const location = useLocation();
    // const navigate = useNavigate();
    const { userName = "Guest", role = "Viewer" } = location.state || {};
    const logout = useLogout();

    return (
        <div className="admin-dashboard">
            <div className="info-box">
                <span className="label">User:</span>
                <span className="value">{userName}</span>
            </div>
            <div className="info-box">
                <span className="label">Role:</span>
                <span className="value">{role}</span>
            </div>
            <div className='form'>
                <button onClick={logout}>Logout</button>
            </div>
        </div>
    );
}
