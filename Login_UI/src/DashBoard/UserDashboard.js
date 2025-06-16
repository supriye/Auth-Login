import React from 'react';
import { useLocation } from 'react-router-dom';
import { useLogout } from '../utill/useLogout';

export default function Dashboard() {
    const location = useLocation();
    const userName = location.state?.userName || "Guest";
    const logout = useLogout();

    return (
        <div className='form'>
            <h1>Welcome, {userName}!</h1>
            <button onClick={logout}>Logout</button>
        </div>
    );
}
