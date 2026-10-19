# AGENTS Guide for WpfSDIAppln

## Scope
This file is the primary ongoing guide for contributors and coding agents working in `WpfSDIAppln`.

## Project Overview
`WpfSDIAppln` is a WPF SDI application using the MVVM pattern. It is the modernized WPF implementation of the original MFC SDI behavior.

## Solution Structure
- `Models/DocumentModel.cs` - Shared document state (`Content`, `FilePath`, `IsModified`)
- `ViewModels/ViewModelBase.cs` - Base type with `INotifyPropertyChanged`
- `ViewModels/RelayCommand.cs` - `ICommand` implementation
- `ViewModels/MainViewModel.cs` - Orchestrates commands, view switching, status updates
- `ViewModels/MainViewViewModel.cs` - Main view state
- `ViewModels/FormViewViewModel.cs` - Form view state
- `ViewModels/EditViewViewModel.cs` - Edit view state and text editing
- `Views/ViewDataTemplateSelector.cs` - Chooses visual template for current view model
- `Views/MainViewControl.xaml` - Main view UI
- `Views/FormViewControl.xaml` - Form view UI
- `Views/EditViewControl.xaml` - Edit view UI
- `MainWindow.xaml` - Menu, toolbar, content host, status bar
- `MainWindow.xaml.cs` - Startup wiring and UI timer/host behavior

## Runtime Architecture
- The main window binds to `MainViewModel`.
- `MainViewModel.CurrentView` determines what is shown in the central `ContentControl`.
- `ViewDataTemplateSelector` maps current view model type to the corresponding view control.
- `DocumentModel` is shared across child view models to keep document state consistent.

## Core Flows
### View Switching
1. User selects a view in the toolbar ComboBox.
2. `MainViewModel.SelectedViewName` changes.
3. `MainViewModel` switches `CurrentView`.
4. Data template selection updates the visible control.

### Command Execution
- Menu and toolbar actions bind to `RelayCommand` instances in `MainViewModel`.
- Typical commands: New, Open, Save, Print (placeholder), Exit, About.

### Document I/O
- New/Open/Save operations are handled in `MainViewModel`.
- Edit text updates flow through the edit view model into shared `DocumentModel` state.

## Build and Run
- Target framework: `.NET 10.0-windows`
- Build with Visual Studio (F5 to run).

## Contribution Rules
- Keep MVVM separation strict: no business logic in code-behind.
- Reuse existing model/viewmodel abstractions before adding new patterns.
- Keep command-driven interactions in view models.
- Preserve current user-facing behavior unless a change request explicitly asks otherwise.

## Related Documentation
- `MIGRATION_SUMMARY.md` contains migration history and completion status.
