# Changelog

All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-09-19

### Added
- **SplashScreenSequencer**: Asynchronous splash sequence controller leveraging `UniTask` to manage sequential activation, tween hold times, exit transitions, and step delays.
- **SplashScreenTween**: Extensible abstract base class for splash screen animations with configurable `middleDelay`, `EnterDuration`, and `ExitDuration`.
- **SplashScreenFadeTween**: `CanvasGroup` fade animation supporting custom ease curves, start/end alphas, and unscaled time updates.
- **SplashScreenScaleTween**: `RectTransform` scale animation with configurable ease types, start/end scale vectors, and unscaled time updates.
- **Completion Event**: `OnSequenceComplete` `UnityEvent` invoked upon completion of the entire splash screen sequence.
- **Demo Sample**: Sample assets located in `Samples~/Demo` containing:
  - `BootLoaderSplashScreenDemo.unity` - ready-to-test demonstration scene.
  - `SplashScreen.prefab` - complete multi-step splash screen hierarchy with background and sequential logo steps.
  - `Logo.prefab` - pre-configured logo prefab with fade and scale tweens.
  - Demo textures and icons.
- **Assembly Definition**: `SplashScreenSystem.asmdef` for clean compilation and dependency management.
