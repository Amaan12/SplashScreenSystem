# SplashScreenSystem

A lightweight, asynchronous splash screen sequencing system for Unity powered by **UniTask** and **DOTween**.

## Features

- **Sequential Step Playback**: Sequentially plays splash items (logos, disclaimers, publisher cards) in order.
- **Asynchronous & Non-blocking**: Uses `UniTask` to manage timing, transitions, and cancellations cleanly.
- **Modular Tweens**: Built-in `SplashScreenFadeTween` (CanvasGroup alpha) and `SplashScreenScaleTween` (RectTransform scale). Easily extensible for custom tweens.
- **Automatic Timing Synchronization**: Calculates maximum enter and exit durations across all child tweens automatically.
- **Completion Event**: `OnSequenceComplete` event triggers when the sequence finishes (e.g., to load the main menu or gameplay scene).

## Prerequisites

- **[UniTask](https://github.com/Cysharp/UniTask)** (`com.cysharp.unitask` >= 2.0.0)
- **[DOTween](https://dotween.demigiant.com/)** (Demigiant)

> **Note**: Ensure DOTween has generated its assembly definition (`Tools > Demigiant > DOTween Utility Panel > Create ASMDEF`).

## Installation

### Via Unity Package Manager (Git URL)

1. In Unity, open **Window** > **Package Manager**.
2. Click the **+** icon in the top-left corner and select **Add package from git URL...**.
3. Enter:
   ```text
   https://github.com/Amaan12/SplashScreenSystem.git
   ```
4. Click **Add**.

## Importing the Sample

1. In the **Package Manager**, select **SplashScreenSystem**.
2. Expand the **Samples** section.
3. Click **Import** next to **Demo**.
4. The demo will be imported into:
   ```text
   Assets/Samples/SplashScreenSystem/1.0.0/Demo/
   ```
5. Open `BootLoaderSplashScreenDemo.unity` to see the splash screen in action.

## Quick Start

1. Create a Canvas with a GameObject named `SplashScreen`.
2. Attach `SplashScreenSequencer` to it.
3. Add child GameObjects for each splash screen step (e.g., Studio Logo, Engine Logo, Game Logo).
4. Add `SplashScreenFadeTween` and/or `SplashScreenScaleTween` to the child objects.
5. In the `SplashScreenSequencer` component, assign the steps to the `Steps` list and hook into `OnSequenceComplete` to load your next scene.

For full setup documentation, see [implementation.md](implementation.md).

## License

[MIT](LICENSE)
