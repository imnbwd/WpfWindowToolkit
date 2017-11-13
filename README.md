# WpfWindowToolkit
A wpf windows toolkit for window opening, including some behaviors, useful class for view model. Available on [nuget](https://www.nuget.org/packages/WpfWindowToolkit/)


### How to use it

#### Open a window

To open a window, you can use attached properties of `WindowHelper` like this:

```    
    <Button x:Name="btn1"
        helpers:WindowHelper.OpenWindowType="{x:Type local:Window1}"
        Content="Open window using Window Helper" />
```

or use `OpenWindowAction` like this:
```
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

#### Open a window with parameter

When opening a Window, you can pass a parameter to its viewmodel. Firstly set the parameter, like this:
```
    helpers:WindowHelper.Parameter="WPF (attached property)"
```
or
```
    <behaviors:OpenWindowAction Parameter="WPF (action)" WindowType="{x:Type local:Window1}" />
```

Then the view model of the window need to be opened need inherits `ViewModelBaseData<T>`, here the type parameter `T` should be type of the parameter you want to passed to, for example:
```
    public class Window1ViewModel : ViewModelBaseData<string>
    {   
        protected override string InternalData { get; set; }
        ...
    }

```

#### To close a window and handle window closing event

By using `CloseWindowAction`, you can add the functionality to close the current Window for an element, like this:
```
    <Button Content="Close the current window with confirmation">
        <i:Interaction.Triggers>
            <i:EventTrigger EventName="Click">
                <behaviors:CloseWindowAction ClosingCheckFunc="{Binding CheckBeforeCloseWindow}" />
            </i:EventTrigger>
        </i:Interaction.Triggers>
    </Button>
```

To handle window closing event, you can use `CloseWindowBehavior`, attach it to a Window like this:
```
    <i:Interaction.Behaviors>
        <behaviors:CloseWindowBehavior ClosingCheckFunc="{Binding CheckBeforeCloseWindow}" />
    </i:Interaction.Behaviors>
```


For more info, you could see the demo project, here is a screenshot for the demo app:

![Demo](https://github.com/imnbwd/WpfWindowToolkit/blob/master/Screenshot/Demo.png)
