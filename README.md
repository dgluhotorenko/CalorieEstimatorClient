****CalorieAI Mobile Client****

A cross-platform mobile application built with .NET MAUI that helps users track their nutrition by simply taking a photo of their food.

**Features**

AI-Powered Analysis: Take a photo or pick one from the gallery to identify dishes and estimate portions.

Detailed Nutrition Breakdown: Get estimated calories, proteins, fats, and carbohydrates for each ingredient.

Custom Notes: Add context (e.g., "with soy sauce") to improve analysis accuracy.

Cross-Platform: Built with .NET MAUI, supporting Android and iOS.

<img width="360" height="640" alt="Screenshot_20260308_194214" src="https://github.com/user-attachments/assets/f1d26527-9a79-496b-b045-76acceb9204d" />
<img width="360" height="640" alt="Screenshot_20260308_195240" src="https://github.com/user-attachments/assets/bb459eb5-e471-4e44-a2bf-151a69d24ad7" />
<img width="360" height="640" alt="Screenshot_20260308_200506" src="https://github.com/user-attachments/assets/09475f4b-cc6f-4858-8b4b-4196ef5d0522" />
<img width="360" height="640" alt="Screenshot_20260308_200530" src="https://github.com/user-attachments/assets/5ea740c2-b4fe-4737-8624-f4f41f5ecd54" />
<img width="360" height="640" alt="Screenshot_20260308_200623" src="https://github.com/user-attachments/assets/0debe030-7888-46c6-bb45-177a8911d6e2" />


**Technology Stack**

Framework: .NET 10.0 MAUI

Architecture: MVVM (Model-View-ViewModel)

Toolkit: CommunityToolkit.Mvvm

Network: HttpClient with MultipartFormData for image uploading

JSON Handling: System.Text.Json (PascalCase to camelCase mapping)

**Getting Started**

Prerequisites

.NET 10 SDK or later.

Android SDK (API 34+ recommended).

IDE: JetBrains Rider or Visual Studio 2022.

Local Setup

Clone the repository:

git clone [https://github.com/dgluhotorenko/CalorieEstimatorClient.git](https://github.com/dgluhotorenko/CalorieEstimatorClient.git)


Configure API Endpoint:
Open Services/CalorieService.cs and update the BaseUrl with your running Web API address (or Dev Tunnel URL).

private const string BaseUrl = "[https://your-dev-tunnel.devtunnels.ms](https://your-dev-tunnel.devtunnels.ms)";


Run the application:

Select your target device (Android Emulator or physical device).

Click Run in your IDE.

**API Integration**

This client communicates with the [CalorieAI Web API](https://github.com/dgluhotorenko/CalorieEstimatorApi). It sends images as multipart/form-data and receives a structured nutrition report.

Note: This project is part of the CalorieAI ecosystem, designed to demonstrate the power of LLMs in health & fitness apps.
