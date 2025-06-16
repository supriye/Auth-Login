import React, { useState } from 'react';
import { useNavigate } from "react-router-dom";
import '../App.css';

export default function AuthForm() {
    const navigate = useNavigate();
    const [name, setName] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const onHandleClick = async () => {
        if (!name || !password) {
            setError("Username and password are required.");
            return;
        }

        try {
            const response = await fetch("https://localhost:7198/api/Auth/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                credentials: "include", // ⬅️ Important: allows cookies (JWT) to be saved
                body: JSON.stringify({ userName: name, password })
            });

            if (!response.ok) {
                const message = await response.text();
                throw new Error(message || "Login failed");
            }

            const data = await response.json();

            // Assuming backend sets role in response (you can modify backend to return { role: "Admin" })
            const userRole = data.role || "User";

            // Navigate based on role
            if (userRole === "Admin") {
                navigate("/admin-dashboard", { state: { userName: name, role: userRole } });
            } else {
                navigate("/dashboard", { state: { userName: name, role: userRole } });
            }
        } catch (err) {
            setError(err.message);
        }
    };

    return (
        <div className='container'>
            <div className='form-container'>
                <div className='form-toggle'>
                    <h2>Login Form</h2>
                </div>
                <div className='form'>
                    <input 
                        type='text' 
                        placeholder='Enter your name' 
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />
                    <input 
                        type='password' 
                        placeholder='Enter your password'
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                    <button onClick={onHandleClick}>Login</button>
                    {error && <p className="error">{error}</p>}
                </div>
            </div>
        </div>
    );
}
