# CsharpMiniProjects
# Mega Project Center for Tools and Games

This project is a hub that brings together multiple C# applications in a single WPF interface. It includes **six distinct apps** categorized into tools and games, providing a comprehensive desktop experience. The project also leverages AI-driven features for both code and image generation, utilizing ChatGPT for various elements of the development process.

## Features

- **Unified Interface**: The hub provides a clean and intuitive interface for accessing all six applications.
- **Categories**: Applications are divided into two main sections:
  - **Tools**: Productivity and utility apps.
  - **Games**: Interactive and fun game projects with unique mechanics.
- **AI Integration**: ChatGPT has been used for code generation and creating images for the different applications within the project.
- **Sliding Transitions**: Smooth navigation between different sections with customized sliding transitions.
- **Custom Design**: Buttons and user interface elements feature pastel colors and a hover effect that slightly enlarges buttons, providing a sleek and consistent user experience.

## Applications

### Games

1. **Snake Game**
   - Control the snake using the arrow keys.
   - Score points by collecting food, but avoid colliding with your own tail!
   - Classic gameplay with a fresh interface and smooth controls.

2. **Pong Game**
   - A modern twist on the classic Pong game.
   - Make the ball go faster by hitting it with the paddle.
   - Play against a friend or test your skills against an AI opponent.

### Tools

1. **Work Hours Management**
   - Manage and track your daily work hours with ease.
   - Add bonus time, adjust for missed hours, and calculate total hours across date ranges.
   - Set your hourly fee and get accurate salary calculations for any given period.

2. **Explicit Words Monitor**
   - Automatically detects explicit words from user input and internet DNS requests, blocking the app if necessary.
   - Built-in word lists for French, English, and Hebrew, along with a customizable list.
   - Password-protected to prevent unauthorized closure, making it a great tool for protecting children.

3. **To-Do List**
   - A simple and intuitive task management app to keep track of your daily to-dos.
   - Organize your tasks efficiently and stay on top of your goals.

4. **Countries Encyclopedia**
   - Visualize and explore all the countries of the world.
   - Learn everything you've ever wanted to know about any country, complete with detailed information and images.

## Installation

Before running the project, make sure to install the following dependencies:

1. **Xceed**: A suite of WPF controls for advanced user interface design.
   - Install via NuGet: `Install-Package Xceed.Wpf.Toolkit`
  
2. **Titanium.Web**: A web request library to handle HTTP and network operations.
   - Install via NuGet: `Install-Package Titanium.Web.Proxy`

3. **MouseKeyHook (by Mamaladaze)**: A global mouse and keyboard hook for monitoring input events.
   - Install via NuGet: `Install-Package MouseKeyHook`

4. **Newtonsoft.Json**: A popular JSON framework for .NET used for data serialization and storage.
   - Install via NuGet: `Install-Package Newtonsoft.Json`

## How to Run

1. Clone the repository:

    ```bash
    git clone https://github.com/YourUsername/MegaProjectCenter.git
    ```

2. Restore dependencies using NuGet:

    ```bash
    nuget restore
    ```

3. Open the solution in Visual Studio and build the project:

    ```bash
    dotnet build
    ```

4. Run the project from Visual Studio.

