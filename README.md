# Hand Tracking Menu System for Unity

## Overview

The **Hand Tracking Menu System for Unity** is a dynamic, reusable, and modular user interface (UI) designed for Unity's XR development. This system enables intuitive interaction with menus using hand gestures feature from Unity's **XR Hands 1.5.0** package.

The menu system supports animations, sound effects, and real-time fingertip tracking.

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
  - **Action States** to raise events.

- **Audio and Animation Support**:
  - Integrated sound effects via Audiosource and handled by **AudioManager**.
  - Smooth animations handled by **AnimationManager** using LeanTween.

---

## Prerequisites

- Unity 2023.1 or newer. (Unity 6)

- **Required Packages**:
  - XR Hands 1.5.0.
  - LeanTween.

- **Recommended Packages**:
   - XR Intraction Toolkit 3.0.7

---

## Complete Setup Guide

### Step 1: Import Package
- Download and import the `HandTrackingMenuSystem_v1.0` package into your Unity project.

### Step 2: XR Rig Setup
- Use the XR Rig from the **XR Interaction Toolkit** (recommended). Use its `Left/Right Interaction Visuals` inside the correspondent for the `Hand Tracking Event` required by FingerTracker component.
- If not, make sure to add an `Hand Tracking Event` component to each of your hand controls.

### Step 3: Create a State Manager
(You can skip this step if you add `Hand Tracking Menu System` prefab to the scene)

1. Create an empty GameObject named **UIStateManager**.
2. Attach the following components:
   - `UIStateMachine`
   - `FingerTracker`
   - `AnimationManager`
   - `AudioManager`
   - `UICreator`.

### Step 4: Configure Dependencies
(You can skip item 2 of this step if you have added `Hand Tracking Menu System` prefab to the scene)

1. Link the **Left/Right Interaction Visuals** or the scene object with **Hand Tracking Event** component to the `FingerTracker`.
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
   -Inside the 

2. **Assign UIStateManager to Gesture Events**:
   - Create a empty object for each Hand Gesture.
   - Add `CustomStaticHandGesture`component to each one of the created game objects.
   - Add the **UIStateManager** object to the **Gesture Performed** or **Gesture Ended** fields in each of the static hand gesture settings.

3. **Map Functions to Gestures**:
   - In the Inspector, assign the following methods from `UIStateMachine` to desired gestures:
     - **Palm Up Gesture** → `MenuOn`
     - **Palm Down Gesture** → `MenuOff`
     - **Thumb to Side Gesture** → `BackToParent`
     - **Pinch Gestures (Index/Middle/Ring/Little)** → `StateActivator`

     For `StateActivator` you have to assign a number from 0 to 3 that corresponds to each index.
     (0 for index finger, 1 for middle finger, 2 for ring finger and 3 for little finger).

4. **Recommended Mapping**:
   - **Palm Up**: Turn the menu on.
   - **Palm Down**: Turn the menu off.
   - **Index/Middle/Ring/Little Finger Pinches**: Activate corresponding states.
   - **Thumb to Side**: Navigate back to the parent state.

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