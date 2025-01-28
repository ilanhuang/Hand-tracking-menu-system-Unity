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

---

## Complete Setup Guide

### Step 1: Import Unity Packages
- **Required Packages**:
- XR Hands 1.5.0.
- LeanTween.
- **Recommended Packages**:
- XR Intraction Toolkit 3.0.7

---

### Step 2: Project Settings And Validations
- Make sure to have installed XR Plug In Management (if not, do so).
- Check `OpenXR`on PC and Android.
- Enable desired interaction profiles in the `OpenXR` tab.
- Check `Hand Tracking Subsystem`, `Meta Hand Tracking Aim`, `Hand Interaction Poses` for PC and Android. (Also check `Meta Quest Support in Android`.)
- In `Project Validation` tab, click on fix all. (Ignore the Meta Quest Support warning for now.)

### Step 3: XR Rig Setup
- Install `Hand Interaction Demo` from `XR Interaction Toolkit` in Package Manager.
- Use the XR Rig prefab from the **Hand Interaction Demo** (recommended) and use its `Left/Right Interaction Visuals` for the `Hand Tracking Event` required by FingerTracker component.
- If not, make sure to add an `Hand Tracking Event` component to each of your hand controls.

### Step 4: Create a State Manager And Configure Dependencies
- Download and import the `HandTrackingMenuSystem_v1.0` package into your Unity project.

(You can skip steps 1, 2 and 3 if you add `Hand Tracking Menu System` prefab inside `Test -> Prefabs` to the scene)

1. Create an empty GameObject named **UIStateManager**.
2. Attach the following components:
   - `UIStateMachine`
   - `FingerTracker`
   - `AnimationManager`
   - `AudioManager`
   - `UICreator`.
3. Assign the following to the `UIStateMachine`: 
   - `FingerTracker`
   - `AnimationManager`
   - `AudioManager`
   - `UICreator`.
4. Link the **Left/Right Interaction Visuals** or the scene object with **Hand Tracking Event** component to the `FingerTracker`.

### Step 5:Menu Structure
1. Create the root **BranchingState**:
   - Right-click in the Project view → Create → UI → BranchingState.
2. Add child **BranchingStates** and/or **ActionStates** (Create → UI → ActionState) as necessary:
   - Define each state in the Inspector by linking its parent and children.
3. Assign teh **Root**(BranchingState) to the `UIStateMachine`.

### Step 6: UI States Configurations
-Set animations, audio, and UI prefabs or sprites for each state.

---

## Step 6: Hand Gesture Setup

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

   **Recommended Gesture - Methods Mapping**
   
   - **Palm Up** → `MenuOn`
   - **Palm Down** → `MenuOff`
   - **Thumb to Side** → `BackToParent`
   - **Pinches(Index/Middle/Ring/Little)** → `StateActivator`

     For `StateActivator` you have to assign a number from 0 to 3 that corresponds to each index.
     (0 for index finger, 1 for middle finger, 2 for ring finger and 3 for little finger).

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