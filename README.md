# CalorieAI Mobile Client

A cross-platform mobile app built with .NET MAUI that estimates nutritional content of meals from photos using AI.

## Features

- **Photo Analysis** — take a photo or pick from gallery to identify dishes and estimate nutrition
- **Detailed Breakdown** — calories, proteins, fats, and carbs for each detected ingredient
- **Custom Notes** — add context (e.g., "with soy sauce") to improve accuracy
- **Cross-Platform** — built with .NET MAUI for Android and iOS

## Screenshots

<p>
  <img width="240" alt="Ingredients breakdown" src="https://github.com/user-attachments/assets/09475f4b-cc6f-4858-8b4b-4196ef5d0522" />
  <img width="240" alt="Not food detection" src="https://github.com/user-attachments/assets/5ea740c2-b4fe-4737-8624-f4f41f5ecd54" />
  <img width="240" alt="Notes feature" src="https://github.com/user-attachments/assets/0debe030-7888-46c6-bb45-177a8911d6e2" />
</p>

## Tech Stack

| | |
|---|---|
| Framework | .NET 10 MAUI |
| Architecture | MVVM (CommunityToolkit.Mvvm) |
| HTTP | Typed HttpClient via `IHttpClientFactory` |
| Error Handling | `Result<T>` pattern (no thrown exceptions in service layer) |
| Serialization | System.Text.Json |

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) with MAUI workload
- Android SDK (API 21+)
- JetBrains Rider or Visual Studio 2022+

### Setup

```bash
git clone https://github.com/dgluhotorenko/CalorieEstimatorClient.git
cd CalorieEstimatorClient
```

Configure the API endpoint in `MauiProgram.cs`:

```csharp
private const string ApiBaseUrl = "http://10.0.2.2:5064";  // Android emulator
// For physical device, use your Dev Tunnel URL:
// private const string ApiBaseUrl = "https://your-id.devtunnels.ms";
```

Select your target device and run from your IDE.

## API Integration

This client communicates with the [CalorieAI Web API](https://github.com/dgluhotorenko/CalorieEstimatorApi). Images are sent as `multipart/form-data` and a structured nutrition report is returned.
