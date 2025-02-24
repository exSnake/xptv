# XPTV - IPTV Player for .NET MAUI

A modern, cross-platform IPTV player built with .NET MAUI, following Clean Architecture and Domain-Driven Design principles.

## Features

- Load and parse M3U/M3U8 playlists
- Group-based channel filtering
- Channel search functionality
- Pagination for large playlists
- Built-in video player for stream playback
- Cross-platform support (Windows, macOS, iOS, Android)

## Architecture

The project follows Clean Architecture principles with a domain-driven design approach:

```
└── 📁xptv
    └── 📁Core
        └── 📁Application        # Application services and interfaces
            └── 📁Channels
                └── 📁Services   # Channel-related services
            └── 📁Common         # Shared application components
        └── 📁Domain            # Domain entities and business logic
    └── 📁Presentation         # UI Layer
        └── 📁Common           # Shared UI components
        └── 📁Channels         # Channel-related UI
        └── 📁Player           # Video player UI
```

### Core Principles

- **Clean Architecture**: Clear separation of concerns between layers
- **DDD**: Strong domain model with bounded contexts
- **SOLID**: Following SOLID principles throughout the codebase
- **MVVM**: Clear separation between Views and ViewModels
- **Dependency Injection**: Loosely coupled components

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022 or JetBrains Rider
- For iOS development: macOS with Xcode
- For Android development: Android SDK

### Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/xptv.git
```

2. Open the solution in your preferred IDE

3. Restore NuGet packages:
```bash
dotnet restore
```

4. Build the project:
```bash
dotnet build
```

### Running the Application

#### Windows/macOS
```bash
dotnet run
```

For iOS/Android, use your IDE's built-in deployment tools or:
```bash
dotnet build -t:Run -f:net8.0-android
dotnet build -t:Run -f:net8.0-ios
```

## Usage

1. Launch the application
2. Click "Load M3U File" to open your IPTV playlist
3. Use the group filter to navigate channels
4. Use the search bar to find specific channels
5. Click on a channel to start playback

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

- Built with [.NET MAUI](https://github.com/dotnet/maui)
- Using [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet)