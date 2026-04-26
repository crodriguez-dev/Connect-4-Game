# Connect 4 Game

# Project Overview
This software is a logic-driven implementation of Connect 4 developed in C#, featuring both local multiplayer and a simulated AI opponent. I engineered a dual-mode start menu that lets users choose their experience, demonstrating my ability to manage complex state transitions and conditional branching. By calculating win conditions through horizontal, vertical, and diagonal array scans, I ensured the software provides accurate real-time feedback during gameplay. This project serves as a practical application of object-oriented programming principles and data structure management, moving beyond simple console output to create a functional, interactive game loop.

# Technical Highlights
* Artificial Intelligence Decision Making: Developed a Robot opponent that uses randomized selection logic to compete against the user in solo mode.
* Algorithmic Win Detection: Programmed four distinct nested loop structures to scan the 6x7 grid for horizontal, vertical, and both types of diagonal 4 in a row permutations.
* Interactive Visuals: Utilized ConsoleColor UI updates to distinguish between player moves and implemented a celebratory EndGame sequence for victories.
* Robust Input Validation: Engineered an input buffer to verify moves, preventing users from entering out of bounds or overflowing full columns.

# Video Demonstration
(Coming Soon)

# Installation and Usage
1. Ensure you have .NET SDK installed on your machine. If not, then follow the instructions found on the internet.
2. Clone this repository to your local directory.
3. Open your terminal and navigate to the project folder.
4. Run the command: dotnet run.
