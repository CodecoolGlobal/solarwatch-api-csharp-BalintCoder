# Solar API

## Overview

The Solar API is a .NET backend service that provides weather-related information, including sunrise and sunset times for cities. It features user authentication and registration using JWT tokens and roles. The API is designed to support secure access for both regular users and administrators.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## Features

- User registration and login with JWT authentication
- Role-based access control (User, Admin)
- Fetch sunrise and sunset times based on city names
- Weather forecast functionality (restricted to Admin users)
- Well-structured and documented codebase

## Technologies

- .NET 8.0
- ASP.NET Core
- Entity Framework Core
- Microsoft SQL Server
- JWT Authentication
- Swagger for API documentation
- HttpClient for external API requests

## Installation

1. **Clone the repository**:
   ```bash
   git clone git clone https://github.com/yourusername/solar.git
   cd solar
   cd solar
   ```

2. **Set up environment variables**:
   Create a `.env` file in the root directory and add your API keys:
   ```plaintext
   WEATHER_API_KEY=your_weather_api_key
   ISSUING_KEY=your_jwt_signing_key
   ```

3. **Install dependencies**:
   Make sure you have the .NET SDK installed. Then run:
   ```bash
   dotnet restore
   ```

4. **Run the database migrations**:
   ```bash
   dotnet ef database update
   ```

5. **Run the application**:
   ```bash
   dotnet run
   ```

## Usage

Once the API is running, you can access the documentation at `http://localhost:5000/swagger` to see all available endpoints and test them directly.

### Example Requests

- **Register a user**:
  ```http
  POST /auth/register
  Content-Type: application/json

  {
      "email": "user@example.com",
      "username": "exampleUser",
      "password": "yourPassword"
  }
  ```

- **Login a user**:
  ```http
  POST /auth/login
  Content-Type: application/json

  {
      "email": "user@example.com",
      "password": "yourPassword"
  }
  ```

- **Get sunrise and sunset times**:
  ```http
  GET /weather/times?cityName=London
  Authorization: Bearer your_jwt_token
  ```

## API Endpoints

### Authentication

- `POST /auth/register`: Registers a new user
- `POST /auth/login`: Authenticates a user and returns a JWT token

### Weather

- `GET /weather/times`: Fetches sunrise and sunset times for a specified city (accessible to User and Admin roles)
- `GET /weather/GetWeatherForecast`: Returns weather forecast data (accessible to Admin role)

## Contributing

Contributions are welcome! Please create a pull request or open an issue for any enhancements or bugs you encounter.


