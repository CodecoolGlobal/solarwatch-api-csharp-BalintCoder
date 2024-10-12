import { StrictMode } from 'react';
import Login from "./Components/Login.jsx";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Register from "./Components/Register.jsx";
import MainPage from "./Components/MainPage.jsx";

function App() {
    return (
        <StrictMode>
            <BrowserRouter>
                <Routes>
                    <Route path="/login" element={<Login />} />
                    <Route path="/register" element={<Register />} />
                    <Route path="/" element={<MainPage />} />
                </Routes>
            </BrowserRouter>
        </StrictMode>
    );
}

export default App;