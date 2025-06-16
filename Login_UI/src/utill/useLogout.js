import { useNavigate } from 'react-router-dom';

export function useLogout() {
    const navigate = useNavigate();

    return () => {
        localStorage.removeItem('token');
        document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        navigate('/');
    };
}
