# Project Context

When working with this codebase, prioritize readability over cleverness. Ask clarifying questions before making architectural changes.

## About This Project

Plugins for the LaunchBox game frontend.
- Online Video Links - This plugin allows you to add online videos to your game, which will appear on the game's menu both in LaunchBox and BigBox.
- Bulk Genre Editor - This plugin allows you to add or remove genres to multiple games, while keeping each game's existing genres.

## Key Directories
- `OnlineVideoLinks/`: Contains the code for the Online Video Links plugin.
- `BulkGenreEditor/`: Contains the code for the Bulk Genre Editor plugin.
- `LaunchboxPluginsTests/`: Contains unit tests for all the plugins.
- `FormsTestProject/`: Test project for running the forms, used for testing form-related features.
- `WpfTestProject/`: Test project for running WPF apps, used for testing WPF-related features.

## Standards
- Use PascalCase for class names, function names, and prpoerty names.
- Constants should be in ALL_CAPS with underscores (e.g., `MAX_RETRIES`).
- Private member variables in classes should be prefixed with an underscore (e.g., `_variable`).
- Use camelCase for local variable names.
- Plugin definitions are in the root directory of each plugin project.
- Each class should have its own file. Do not group multiple classes in a single file.

## Resources
- [LaunchBox Plugin Documentation](https://pluginapi.launchbox-app.com/html/4cf923f7-940c-5735-83de-04107a6ae0e6.htm)