# WpfWindowToolkit

![Demo](https://github.com/imnbwd/WpfWindowToolkit/blob/master/Images/Logo.png)

[![Build Status](https://imnbwd.visualstudio.com/WpfWindowToolkit/_apis/build/status/imnbwd.WpfWindowToolkit?branchName=master)](https://imnbwd.visualstudio.com/WpfWindowToolkit/_build/latest?definitionId=6&branchName=master) 
[![NuGet](https://img.shields.io/nuget/v/wpfwindowtoolkit.svg)](https://www.nuget.org/packages/wpfwindowtoolkit) 
[![NuGet Download](https://img.shields.io/nuget/dt/wpfwindowtoolkit.svg)](https://www.nuget.org/packages/wpfwindowtoolkit) 
[![License](https://img.shields.io/badge/License-Apache%202.0-blue.svg)](https://opensource.org/licenses/Apache-2.0) 
[![Join the chat at https://gitter.im/imnbwd/WpfWindowToolkit](https://badges.gitter.im/imnbwd/WpfWindowToolkit.svg)](https://gitter.im/imnbwd/WpfWindowToolkit?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

A wpf windows toolkit for window operations, including a few behaviors, useful class for view model. Available on [NuGet](https://www.nuget.org/packages/WpfWindowToolkit/)

## Features
* Support both .NET Core 3.0+ and .NET Framework 4.5+
* Handle Window opening, closing using behaviors
* Pass parameters and return value between windows 
* Fully MVVM supported
* Other utility behaviors
## Installation

#### Package Manager

```PowerShell
Install-Package WpfWindowToolkit
```

#### .NET CLI

```
dotnet add package WpfWindowToolkit
```

## Namespace

To use `WpfWindowToolkit` in your application you need to add the following namespaces to your Xaml files.

```XAML
xmlns:behaviors="http://wpfwindowtoolkit.org/behaviors"
xmlns:helpers="http://wpfwindowtoolkit.org/helpers"
```


## How to


### Open a window

To open a window, you can use attached properties of `WindowHelper` like this:

```XAML
    <Button x:Name="btn1"
        helpers:WindowHelper.OpenWindowType="{x:Type local:Window1}"
        Content="Open window using Window Helper" />
```

or use `OpenWindowAction` like this:
```XAML
    <Button x:Name="btn5"
        Margin="0,5,0,0"
        Content="Open window with parameter using action">
        <i:Interaction.Triggers>
            <i:EventTrigger EventName="Click">
                 <behaviors:OpenWindowAction Parameter="WPF (action)" WindowType="{x:Type local:Window1}" /> 
            </i:EventTrigger>
        </i:Interaction.Triggers>
    </Button>
```

### Open a window with parameter

**# approach 1 & 2**

When opening a Window, you can pass a parameter to its viewmodel. Firstly set the parameter, like this:
```XAML
    helpers:WindowHelper.Parameter="WPF (attached property)"
```
or
```XAML
    <behaviors:OpenWindowAction Parameter="WPF (action)" WindowType="{x:Type local:Window1}" />
```

Then the view model of the window needed to be opened should inherit `ViewModelBaseData<T>`, here the type parameter `T` should be type of the parameter you want to passed to, for example:
```C#
    public class Window1ViewModel : ViewModelBaseData<string>
    {   
        protected override string InternalData { get; set; }
        ...
    }

```
Of course, you can use data binding to bind a more business specific model or a more complex data type that needs passing to another window.

**# approach 3**

However, maybe you want to have more control concerning opening a window, not just using `OpenWindowAction` and `WindowHelper`. Here is what you can do:

Firstly, the view model of first window should inherit from `ViewModelBaseEx`, then you can use its `ShowWindow(OpenWindowInfo)` method to open another window, so your code would be like this:

```C#
    public class MainViewModel : ViewModelBaseEx
    {
        ...
        public RelayCommand ComplexLogicForOpeningAWindowCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    // your logic here (before)
                    this.ShowWindow(new OpenWindowInfo { IsModal = true, Parameter = CurrentFriend, WindowType = typeof(Window2) });
                    // your logic here (after)
                });
            }
        }
    }
```

On the other hand, the view model of the second window which needs to be opened should be processed in the same way as discussed previously, that is to inherit from `ViewModelBaseData<T>`.

### Return a value from the opening window

When the opening window is closed, you may want to get a return value. To do this, firstly, the view model of the first window should inherit from `ViewModelBaseEx<T>`, here the type parameter is the type of the return value, then you can use `ShowWindow(OpenWindowInfo, Action<TReturnValue>)` method. Of course, your view model can also inherit from `ViewModelBaseEx` and use its `ShowWindow(OpenWindowInfo, Action<object>)` method. The second parameter indicates how to handle or process the return value by passing it to an `Action`.

```C#
    public class ReturnValueMainWindowViewModel : ViewModelBaseEx<Friend>
    {
        public void ShowFriendSelectionWindow()
        {
            this.ShowWindow(new OpenWindowInfo { WindowType = typeof(ReturnValueTestWindow) }, friend =>
            {
                if (friend != null)
                {
                    MessageBox.Show($"You have selected this friend: {friend.Name}");
                }
                else
                {
                    MessageBox.Show("No friend has been selected");
                }
            });
        }
    }
```

Then, the view model of the second window need implement the interface `IWindowReturnValue` or `IWindowReturnValue<T>` which only contain a property named `ReturnValue`, at a proper place set the value to this property. Here is the code:

```C#
    public class ReturnValueTestWindowViewModel : BindableBase, IWindowReturnValue<Friend>
    {
        ...
        public void SetReturnValue() 
        {
            ReturnValue = SelectedFriend;
        }
        ...
    }
```

### Close a window

By using `CloseWindowAction`, you can add the functionality to close the current Window for an element, like this:
```XAML
    <Button Content="Close the current window with confirmation">
        <i:Interaction.Triggers>
            <i:EventTrigger EventName="Click">
                <behaviors:CloseWindowAction ClosingCheckFunc="{Binding CheckBeforeCloseWindow}" />
            </i:EventTrigger>
        </i:Interaction.Triggers>
    </Button>
```
### Handle window closing event

To handle window closing event, you can use `ClosingWindowBehavior`, attach it to a Window like this:
```XAML
    <i:Interaction.Behaviors>
        <behaviors:ClosingWindowBehavior ClosingCheckFunc="{Binding CheckBeforeCloseWindow}" />
    </i:Interaction.Behaviors>
```
By binding `ClosingCheckFunc` property to a function which type is `Func<bool>` to indicate whether the window can be closed or not.


### Close window from view model

Sometimes, before closing the window, there is some logic in the view model needed to be executed, to do this, just follow the 2 steps:

Firstly, add the behavior `EnableWindowCloseBehavior` to the window that needs to be closed from its view model like this:

```XAML
    <Window>
        <i:Interaction.Behaviors>
            <behavior:EnableWindowCloseBehavior />
        </i:Interaction.Behaviors>
    ...
    </Window>
```

Then, the corresponding view model should implement `IClosable` which contains an `Action` named `CloseAction`:
```C#
    public class CloseTestViewModel : BindableBase, IClosable
    {
        public Action CloseWindow { get; set; }
        ...
    }
```
At a proper place, you could invoke `CloseWindow` action to close the window:
```C#
   CloseWindow?.Invoke();  // or just CloseWindow();
```

### Get event parameter in view model
It's essential to get event parameter for some operations, drag and drop , for instance, we can get the dragging data by accessing the event parameter of `DragOver` or `Drop` event, which type is `DragEventArgs`. 

To get event parameter in view model, you can use `InvokeCommandAction` or `CallMethodAction`, both of their names are same as the corresponding action in Blend Behaviors. While, the difference is these two action can invoke a command or a method with the parameter of the event which invokes them by a `EventTrigger`.

```XAML
<Button Content="Get event argument(InvokeCommandAction)">
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="Click">
            <behaviors:InvokeCommandAction Command="{Binding ShowEventArgumentCommand}" />
        </i:EventTrigger>
    </i:Interaction.Triggers>
</Button>

<Button Content="Get event argument(CallMethodAction)">
    <i:Interaction.Triggers>
        <i:EventTrigger EventName="Click">
            <behaviors:CallMethodAction MethodName="ShowEventArgument" TargetObject="{Binding}" />
        </i:EventTrigger>
    </i:Interaction.Triggers>
</Button>
```

```C#
public ICommand ShowEventArgumentCommand => new RelayCommand<RoutedEventArgs>(ShowEventArgument);

public void ShowEventArgument(RoutedEventArgs e)
{
    ...
}
```

Tips: 

> For `InvokeCommandAction`, you can still set a value for `CommandParameter` property for your purpose, without explicitly setting value for this property, event parameter will be used as the parameter in this action.

> For `CallMethodAction`, the specified method should contain either no parameter or one parameter with the event parameter type.



## More info

For more info, you could dive into the demo project, here is a screenshot for the demo app:

![Demo](https://github.com/imnbwd/WpfWindowToolkit/blob/master/Images/Screenshot/Demo.png)
