# WPF SDI Application - Component Diagram

## Application Flow

```
┌─────────────────────────────────────────────────────────────────┐
│                         MainWindow                               │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │ Menu Bar                                                   │  │
│  │ File | Edit | Help                                        │  │
│  └───────────────────────────────────────────────────────────┘  │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │ ToolBar                                                    │  │
│  │ [New] [Open] [Save] [Print] | View: [ComboBox ▼]         │  │
│  └───────────────────────────────────────────────────────────┘  │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │                                                            │  │
│  │  ┌─────────────────────────────────────────────────────┐  │  │
│  │  │         ContentControl                               │  │  │
│  │  │  (Displays current view via DataTemplateSelector)   │  │  │
│  │  │                                                      │  │  │
│  │  │  ┌───────────────────────────────────────────────┐  │  │  │
│  │  │  │                                                │  │  │  │
│  │  │  │  Currently Active View:                       │  │  │  │
│  │  │  │   - MainViewControl                           │  │  │  │
│  │  │  │   - FormViewControl                           │  │  │  │
│  │  │  │   - EditViewControl                           │  │  │  │
│  │  │  │                                                │  │  │  │
│  │  │  └───────────────────────────────────────────────┘  │  │  │
│  │  └─────────────────────────────────────────────────────┘  │  │
│  │                                                            │  │
│  └───────────────────────────────────────────────────────────┘  │
│  ┌───────────────────────────────────────────────────────────┐  │
│  │ Status Bar                                                 │  │
│  │ Ready | CAPS | NUM                                        │  │
│  └───────────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────────┘
```

## MVVM Data Flow

```
┌──────────────┐         ┌──────────────────┐         ┌─────────────┐
│              │         │                  │         │             │
│     View     │ ◄─────► │    ViewModel     │ ◄─────► │    Model    │
│   (XAML)     │ Binding │  (Logic/State)   │         │    (Data)   │
│              │         │                  │         │             │
└──────────────┘         └──────────────────┘         └─────────────┘
      │                           │                          │
      │                           │                          │
  User Actions            INotifyPropertyChanged      DocumentModel
  (Click, Type)           Commands (ICommand)         (Content, Path)
```

## Class Hierarchy

```
MainWindow
  └─ MainViewModel : ViewModelBase
       ├─ Commands
       │   ├─ NewDocumentCommand : RelayCommand
       │   ├─ OpenDocumentCommand : RelayCommand
       │   ├─ SaveDocumentCommand : RelayCommand
       │   ├─ PrintCommand : RelayCommand
       │   ├─ ExitCommand : RelayCommand
       │   └─ AboutCommand : RelayCommand
       │
       ├─ Properties
       │   ├─ CurrentView (ViewModelBase)
       │   ├─ SelectedViewName (string)
       │   ├─ StatusBarText (string)
       │   ├─ CapsLockOn (bool)
       │   └─ NumLockOn (bool)
       │
       ├─ Child ViewModels
       │   ├─ MainViewViewModel : ViewModelBase
       │   ├─ FormViewViewModel : ViewModelBase
       │   └─ EditViewViewModel : ViewModelBase
       │
       └─ Shared Model
           └─ DocumentModel
               ├─ Content (string)
               ├─ IsModified (bool)
               └─ FilePath (string)
```

## View Switching Mechanism

```
User Selects View from ComboBox
        ↓
SelectedViewName Property Updates (MainViewModel)
        ↓
PropertyChanged Event Fires
        ↓
SwitchView() Method Called
        ↓
CurrentView Property Set to Appropriate ViewModel
        ↓
PropertyChanged Event Fires for CurrentView
        ↓
ContentControl Binding Updates
        ↓
ViewDataTemplateSelector.SelectTemplate() Called
        ↓
Appropriate DataTemplate Selected
        ↓
View Displayed in ContentControl
```

## Command Execution Flow

```
User Clicks "Save" Button
        ↓
Button.Command Binding to SaveDocumentCommand
        ↓
RelayCommand.Execute() Called
        ↓
MainViewModel.SaveDocument() Method Runs
        ↓
DocumentModel.Content Retrieved from EditViewViewModel
        ↓
File.WriteAllText() Saves to Disk
        ↓
StatusBarText Updated
        ↓
PropertyChanged Event Fires
        ↓
UI Updates to Show "Saved: [filepath]"
```

## Data Binding Examples

### Two-Way Binding (Edit View)
```
TextBox.Text ◄═══════► EditViewViewModel.EditContent
              Binding              ║
                                   ║
                                   ▼
                          DocumentModel.Content
```

### One-Way Binding (Status Bar)
```
MainViewModel.StatusBarText ═══════► TextBlock.Text
         (Source)                      (Target)
```

### Command Binding (Menu/Toolbar)
```
MenuItem.Command ═══════► MainViewModel.NewDocumentCommand
                                       ║
                                       ▼
                              NewDocument() Method
```

## Component Communication

```
┌─────────────────────────────────────────────────────────┐
│                    MainViewModel                        │
│                   (Orchestrator)                        │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐    │
│  │   MainView  │  │  FormView   │  │  EditView   │    │
│  │  ViewModel  │  │  ViewModel  │  │  ViewModel  │    │
│  └──────┬──────┘  └──────┬──────┘  └──────┬──────┘    │
│         │                 │                 │           │
│         └─────────────────┼─────────────────┘           │
│                           │                             │
│                      ┌────▼────┐                        │
│                      │Document │                        │
│                      │  Model  │                        │
│                      └─────────┘                        │
│                    (Shared State)                       │
└─────────────────────────────────────────────────────────┘
```

## Key Design Patterns Used

1. **MVVM Pattern**: Separation of UI, logic, and data
2. **Command Pattern**: ICommand for all user actions
3. **Observer Pattern**: INotifyPropertyChanged for data binding
4. **Data Template Pattern**: Dynamic view selection
5. **Singleton Pattern**: Single DocumentModel shared across views

## Benefits of This Architecture

✅ **Testability**: ViewModels can be tested without UI
✅ **Maintainability**: Clear separation of concerns
✅ **Extensibility**: Easy to add new views/features
✅ **Reusability**: ViewModels independent of views
✅ **Designer-Developer Workflow**: XAML designers work independently
