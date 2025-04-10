import { useState } from "react";

export default function MainPage() {
    const [cityName, setCityName] = useState("");
    const [cityDetails, setCityDetails] = useState(null);
    const [error, setError] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await fetch(`/api/Weather/times?cityName=${cityName}`, {
                method: "GET",
            });

            if (!response.ok) {
                console.error("Response Error:", response.status, response.statusText); // Log response details
                throw new Error("City not found");
            }

            const data = await response.json();
            console.log("Fetched Data:", data); // Log the fetched data
            setCityDetails(data);
            setError(null);
        } catch (err) {
            console.error("Fetch Error:", err); // Log fetch errors
            setError(err.message);
            setCityDetails(null);
        }
    };

    return (
        <div className="flex flex-col items-center justify-center min-h-screen bg-gray-100">
            <h1 className="text-2xl mb-4">Weather App</h1>
            <form onSubmit={handleSubmit} className="flex flex-col items-center">
                <input
                    type="text"
                    value={cityName}
                    onChange={(e) => setCityName(e.target.value)}
                    placeholder="Enter city name"
                    className="px-3 py-2 border border-gray-300 rounded mb-4 focus:outline-none focus:ring focus:ring-blue-300"
                />
                <button
                    type="submit"
                    className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
                >
                    Get Weather Details
                </button>
            </form>

            {error && <p className="text-red-500 mt-4">{error}</p>}

            {cityDetails && (
                <div className="mt-8 p-4 bg-white rounded shadow">
                    <h2 className="text-xl">City Details</h2>
                    <p><strong>Sunrise:</strong> {new Date(cityDetails.sunrise).toLocaleTimeString()}</p>
                    <p><strong>Sunset:</strong> {new Date(cityDetails.sunset).toLocaleTimeString()}</p>
                    <p><strong>Time Zone:</strong> {cityDetails.timeZone}</p>
                </div>
            )}
        </div>
    );
}