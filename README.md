# TransferMarkt Test Automation Framework

## Project Overview
This project is a test automation framework for [TransferMarkt](https://www.transfermarkt.com/) built with **C# .NET** and **Playwright**. It includes automated functional tests to validate key features of the website. The project follows SOLID principles and adheres to DRY and KISS standards, making the framework highly maintainable, scalable, and easy to understand.

## Table of Contents
- [Features](#features)
- [Technologies](#technologies)
- [Setup and Installation](#setup-and-installation)
- [Running the Tests](#running-the-tests)
- [Test Details](#test-details)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Features
This test automation framework includes:
- **Five functional tests** covering key areas of the website:
  - Premier League Table visibility on the homepage
  - Search functionality
  - Site navigation between key pages
  - Login form validation
  - API request monitoring to catch 500 errors
- **Page Object Model (POM)** for organized, reusable page actions
- **Logging** for clear, step-by-step tracking of test progress
- **Dependency Injection** for easy test management and mocking capabilities

## Technologies
- **C# .NET** – Primary language for framework development
- **Playwright** – Browser automation library for cross-browser testing
- **NUnit** – Test framework for structuring and executing tests

## Setup and Installation

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/yourusername/TransferMarktTestFramework.git
    cd TransferMarktTestFramework
    ```

2. **Install Dependencies**:
   Ensure that you have the latest version of **.NET SDK** and **Playwright**:
    ```bash
    dotnet restore
    dotnet tool install --global Microsoft.Playwright.CLI
    playwright install
    ```

3. **Configure Settings**:
   Modify `appsettings.json` for configuration details like base URL and headless mode. Ensure the file includes:
   ```json
   {
       "BaseUrl": "https://www.transfermarkt.com/",
       "HeadlessMode": true // Change to false if you want to see the browser
   }
   ```

**Running the Tests**:
To execute all tests:

```
dotnet test
```

For running tests in a specific class, use:

```
dotnet test --filter FullyQualifiedName~Namespace.ClassName
```

## Test Details

| Test Case                       | Description                                                                                     |
|---------------------------------|-------------------------------------------------------------------------------------------------|
| **Premier League Table Visibility** | Verifies that the Premier League Table is visible on the homepage.                         |
| **Search Functionality**             | Checks if searching for a player (e.g., “Messi”) returns results.                           |
| **Navigation**                       | Ensures users can navigate between key sections like News, Transfers, etc.                 |
| **Login Form Validation**            | Validates with incorrect login credentials return an error message.                        |
| **API Request Monitoring**           | Tracks API requests to ensure no 500 errors appear, simulating lazy loading where present. |

## Project Structure

```
TransferMarktTestFramework/
│
├── Pages/
│   ├── HomePage.cs                  # Page Object for the homepage
│   ├── SearchPage.cs                # Page Object for search functionality
│   ├── NavigationPage.cs            # Page Object for navigation elements
│   ├── LoginPage.cs                 # Page Object for login form
│
├── Tests/
│   ├── BaseTest                     # Base test class with common setup and teardown
│   ├── HomePageTests.cs             # Tests for Premier League Table visibility
│   ├── SearchPageTests.cs           # Tests for search functionality
│   ├── NavigationPageTests.cs       # Tests for navigation between pages
│   ├── LoginPageTests.cs            # Tests for login form validation
│   ├── ApiTests.cs                  # Tests for monitoring API requests
│
├── Utilities/
│   ├── AppSettings.cs               # Application settings class
│   ├── appsettings.json             # Configuration file for application settings
│   ├── ConfigLoader.cs              # Utility to load configuration settings
│   ├── Credentials.cs               # Class for managing user credentials
│   ├── credentials.json             # File containing user credentials
│   ├── HttpStatusCode.cs            # Utility for handling HTTP status codes
│
├── .gitignore
└── README.md                         # Project documentation
```

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch (`feature/YourFeature`).
3. Commit your changes.
4. Push the branch to your forked repository.
5. Create a Pull Request.



