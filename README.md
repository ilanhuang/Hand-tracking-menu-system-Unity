# Hand Tracking Menu System for Unity

## Overview

The **Hand Tracking Menu System for Unity** is a dynamic, reusable, and modular user interface (UI) designed for Unity's XR development. This system enables intuitive interaction with menus using hand gestures, powered by Unity's **XR Hands 1.5.0** package.

The menu system is built using the **Finite State Design Pattern** and is compatible with Unity's **StaticHandGesture** script for gesture recognition. It supports animations, sound effects, and real-time fingertip tracking, offering a polished and engaging user experience.

---

## Core Features

- **Recommended Gesture-Based Navigation**:
  - **Index/Middle/Ring/Little Pinch Gesture**: Navigate forward to child states.
  - **Palm Up Gesture**: Turn the menu on.
  - **Palm Down Gesture**: Turn the menu off.
  - **Thumb to Side Gesture**: Navigate back to the parent state.

- **Dynamic UI Updates**:
  - Menus dynamically spawn and update based on gestures.
  - UI elements align with fingertip positions in real time.

- **QuickMenu Support**:
  - Overlay menus display shortcuts or advanced features.
  - Unique QuickMenu actions are configurable per option.

- **Modular Architecture**:
  - **Branching States** for hierarchical menu navigation.
  - **Action States** to trigger specific actions.

- **Polished Experience**:
  - Integrated sound effects via the **AudioManager**.
  - Smooth animations handled by **AnimationManager** using LeanTween.

---

## Prerequisites

Ensure your Unity project meets the following requirements:

- Unity 2023.1 or newer.
- **Required Packages**:
  - XR Interaction Toolkit 3.0.
  - XR Hands 1.5.0.
  - LeanTween.

---

## Setup Guide

### Step 1: Import Package
- Download and import the `HandTrackingMenuSystem_v1` package into your Unity project.

### Step 2: XR Rig Setup
- Use the XR Rig from the **XR Interaction Toolkit** (recommended).
- Ensure it includes `Left/Right Interaction Visuals`.

### Step 3: Create a State Manager
1. Create an empty GameObject named **UIStateManager**.
2. Attach the following components:
   - `UIStateMachine`
   - `FingerTracker`
   - `AnimationManager`
   - `AudioManager`
   - `UICreator`.

### Step 4: Configure Dependencies
1. Link the **Left/Right Interaction Visuals** to the `FingerTracker`.
2. Assign the following to the `UIStateMachine`:
   - `FingerTracker`
   - `AnimationManager`
   - `AudioManager`
   - `UICreator`.

### Step 5: Define States
1. Create the root **BranchingState**:
   - Right-click in the Project view → Create → UI → BranchingState.
   - Assign it to the `UIStateMachine`.
2. Add child **BranchingStates** as necessary:
   - Define each state in the Inspector by linking its parent and children.
   - Configure animations, audio, and UI prefabs or sprites for each state.

### Step 6: Configure Action States
- Create **ActionStates** (Create → UI → ActionState).
- Attach `ActionEventListener` components to manage events triggered by ActionStates.

### Step 7: Position and Style UI
- Assign `jointID`, offsets, and scaling for precise alignment with fingers.
- Customize animations via the **AnimationManager**.

---

## Hand Gesture Setup

1. **Create Custom Hand Poses**:
   - Follow the **Unity XR Hands 1.5.0 tutorial** to create custom hand poses for gestures.
   - Assign hand poses to gestures based on their functionality.

2. **Assign UIStateManager to Gesture Events**:
   - Add the **UIStateManager** object to the **Gesture Performed** or **Gesture Ended** fields in your hand gesture settings.

3. **Map Functions to Gestures**:
   - In the Inspector, assign the following methods from `UIStateMachine` to desired gestures:
     - **Palm Up Gesture** → `MenuOn`
     - **Palm Down Gesture** → `MenuOff`
     - **Pinch Gestures (Index/Middle/Ring/Little)** → `StateActivator`
     - **Thumb to Side Gesture** → `BackToParent`

4. **Recommended Mapping**:
   - **Palm Up**: Turn the menu on.
   - **Palm Down**: Turn the menu off.
   - **Index/Middle/Ring/Little Finger Pinches**: Activate corresponding states.
   - **Thumb to Side**: Navigate back to the parent state.

---

## Testing and Refinement

- Enter Play mode and use hand gestures to test the menu system.
- Adjust hand pose parameters and gesture mappings for optimal performance.

---

## Components Breakdown

- **UIStateMachine**: Manages state transitions and interactions.
- **BranchingState**: Represents hierarchical menu states.
- **ActionState**: Defines terminal states with action triggers.
- **FingerTracker**: Tracks fingertip positions and updates UI alignment.
- **AudioManager**: Handles sound effects for transitions.
- **AnimationManager**: Plays LeanTween animations for smooth transitions.
- **UICreator**: Dynamically creates UI elements at runtime.
- **ActionEventListener**: Manages UnityEvents for actions.

---
