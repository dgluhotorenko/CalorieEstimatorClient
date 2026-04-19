# CalorieAI Mobile Client

A cross-platform mobile app built with .NET MAUI that estimates nutritional content of meals from photos using AI.

## Features

- **Photo Analysis** — take a photo or pick from gallery to identify dishes and estimate nutrition
- **Detailed Breakdown** — calories, proteins, fats, and carbs for each detected ingredient
- **Custom Notes** — add context (e.g., "with soy sauce") to improve accuracy
- **Cross-Platform** — built with .NET MAUI for Android and iOS

## Screenshots

<p>
  <img width="320" alt="Load image screen" alt="Screenshot_20260419_185738" src="https://github.com/user-attachments/assets/6a715e54-e044-4e3c-80c5-5ece875cfc5b" />
  <img width="320" alt="Notes chart" alt="Screenshot_20260419_185902" src="https://github.com/user-attachments/assets/4509c8e0-338a-4e9e-89d0-1581b9682e84" />
  <img width="320" alt="Notes details" alt="Screenshot_20260419_185936" src="https://github.com/user-attachments/assets/6b929df4-108c-46d5-b2c1-9e0823bf91a9" />
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
