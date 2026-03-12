# MediaChief

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/Platform-Windows%20%7C%20Linux%20%7C%20macOS-lightgrey.svg)]()

A powerful, user-friendly desktop application for video remuxing, re-encoding, and video-to-audio conversion built with C# and .NET.

![Screenshot](screenshots/main-window.png)

## Table of Contents

- [Features](#features)
- [Usage](#usage)
- [Supported Formats](#supported-formats)
- [System Requirements](#system-requirements)
- [Building from Source](#building-from-source)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)

## Features

### 🎬 Video Remuxing
- **Container Swapping**: Change video containers (MKV ↔ MP4 ↔ AVI ↔ MOV) without re-encoding
- **Stream Management**: Add, remove, or modify audio/subtitle tracks
- **Metadata Preservation**: Keep original metadata during container switches
- **Batch Processing**: Remux multiple files simultaneously

### 🔄 Video Re-encoding
- **Codec Conversion**: H.264, H.265/HEVC, VP9, AV1 support
- **Quality Presets**: From fast encoding to maximum quality
- **Hardware Acceleration**: NVENC, AMD VCE, Intel Quick Sync support
- **Resolution Scaling**: Upscale or downscale with various algorithms
- **Frame Rate Conversion**: Change FPS with motion interpolation options
- **Bitrate Control**: CBR, VBR, and CRF encoding modes

### 🎵 Video-to-Audio Extraction
- **Format Support**: MP3, AAC, FLAC, WAV, OGG, Opus
- **Quality Selection**: Variable or constant bitrate options
- **Track Selection**: Extract specific audio tracks from multi-track videos
- **Batch Extraction**: Process entire playlists or folders

## Usage

### Quick Start

1. **Launch** MediaChief
2. **Select Conversion Type**
   - **Audio**
   - **Video**
3. **Import**: import folder or file
4. **Configure Settings**: Choose output format, destination
5. **Start**: Click "Convert" and monitor progress

### Example Workflows

#### Remux MKV to MP4
```
Input: movie.mkv (H.264 + AAC)
Operation: Remux
Output: movie.mp4
```

#### Extract Podcast Audio
```
Input: interview.mp4
Format: MP3
Bitrate: 192 kbps
Output: interview.mp3
```

## Supported Formats

### Input Containers
| Format | Extension | Notes |
|--------|-----------|-------|
| MPEG-4 | .mp4, .m4v, .m4a | Full support |
| Matroska | .mkv | Full support including chapters |
| AVI | .avi | Legacy support |
| QuickTime | .mov, .qt | Full support |
| MPEG-TS | .ts, .mts | Broadcast formats |
| FLV | .flv | Flash video |
| WebM | .webm | VP8/VP9/AV1 |
| WMV | .wmv | Windows Media |
| 3GP | .3gp, .3g2 | Mobile formats |

### Video Codecs
- H.264/AVC (decoding & encoding)
- H.265/HEVC (decoding & encoding)
- VP8, VP9 (decoding & encoding)
- AV1 (decoding & encoding)
- MPEG-2, MPEG-4 Part 2 (decoding)
- ProRes, DNxHD (decoding)
- Theora (decoding)

### Audio Codecs
- AAC, MP3, FLAC, WAV, OGG Vorbis, Opus
- AC3, E-AC3, DTS
- PCM (various formats)

### Output Formats
- **Video**: MP4, MKV, MOV, WebM, AVI, GIF
- **Audio**: MP3, AAC, FLAC, WAV, OGG, Opus, M4A

## System Requirements

| Component | Minimum | Recommended |
|-----------|---------|-------------|
| OS | Windows 10 / Ubuntu 20.04 / macOS 11 | Windows 11 / Ubuntu 22.04 / macOS 14 |
| CPU | x64, 2 cores | x64, 4+ cores with AVX2 |
| RAM | 4 GB | 8 GB+ |
| GPU | DirectX 11 compatible | NVIDIA/AMD with NVENC/VCE |
| Storage | 100 MB (app) + temp space | SSD for processing |

## Building from Source

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or VS Code (optional)
- FFmpeg development libraries (for custom builds)

### Clone and Build
```bash
# Clone repository
git clone https://github.com/yourusername/MediaChief.git
cd MediaChief

# Restore dependencies
dotnet restore

# Build debug version
dotnet build

# Build release version for windows
dotnet publish -c Release -r win-x64 --self-contained true

# Build release version for linux
dotnet publish -c Release -r linux64 --self-contained true

# Run
dotnet run
```

## Contributing

We welcome contributions! Please see [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

### Quick Start for Contributors
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Development Areas
- [ ] GPU acceleration improvements
- [ ] Additional codec support (VVC/EVC)
- [ ] Hardware detection and optimization
- [ ] Localization (i18n)
- [ ] Plugin system for custom filters

## License

Distributed under the MIT License. See [LICENSE](LICENSE) for more information.

## Acknowledgments

- [FFmpeg](https://ffmpeg.org/) - The backbone of our media processing
- [FFMpegCore](https://github.com/rosenbjerg/FFMpegCore) - wrapper for ffmpeg
- [CommunityToolkit.Mvvm](https://github.com/CommunityToolkit/dotnet) - MVVM toolkit
- [AvaloniaUi](https://avaloniaui.net/) - UI framework

## Support

- 📧 Email: alwleedamado@gmail.com
- 🐛 Issues: [GitHub Issues](https://github.com/alwleedamado/MediaChief/issues)
