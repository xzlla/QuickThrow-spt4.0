# 🎯 QuickThrowGrenade

This mod for **SPT** restores the deprecated "Fast Grenade Throw" mechanic from earlier Tarkov versions (≈ <0.12), allowing you to **instantly throw a grenade** without having to equip it first.

PS：The original author didn't update the SPT 4.0 version, and I didn't see a usable QuickThrow in SPT 4.0 either, so I ported it over.

The original author's website: https://github.com/yox92/QuickThrow

## 🔧 Key Features

- **Fast throw grenade**: Pressing the grenade key will throw the grenade instantly, skipping equip animations.
- **Low throw override**: Hold a configurable 'Left Control' key to **force a short throw** with QuickThrow feature.
- **Restore Vanilla**: Hold a configurable key 'Shift' to **to equip it first**. Vanilla feature.

## 🔌 Installation

Drop `QuickThrow.dll`, `QuickThrow_log.txt`, `debug.cfg` folder into your `BepInEx/plugins/QuickThrow` directory.

## 🧠 How It Works

This mod uses Harmony patches on two key methods:
- `Player.Proceed(ThrowWeapItemClass, Callback<IHandsThrowController>, bool)`  
  → Replaced with `Proceed(ThrowWeapItemClass, Callback<GInterface206>, bool)` (QuickUse) unless a bypass key is held.
- `Player.BaseGrenadeHandsController.vmethod_2(float timeSinceSafetyLevelRemoved, Vector3 position, Quaternion rotation, Vector3 force, bool lowThrow)`  
  → Dynamically enforces **low throw** if the appropriate key is pressed.

## 🖱️ Keybindings

| Action              | Default Key                            |
|---------------------|----------------------------------------|
| Force low throw     | `Left Control` or any configurable key |
| Force Vanilla throw | `Left Shift` or any configurable key   |

## ⚙️ Debug
A detailed log is printed to `QuickThrow_log.txt` when file `debug.cfg` true.
