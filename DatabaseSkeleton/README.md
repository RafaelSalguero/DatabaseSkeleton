Read me with a markdown reader such as [VSCode](https://code.visualstudio.com/) or
the [Chrome Markdown Reader extension](https://chrome.google.com/webstore/detail/markdown-reader/gpoigdifkoadgajcincpilkjmejcaanc)

#DatabaseSkeleton

This is the UI layer of the application. In this project  Views and ViewModels are considered 
that they both live on the UI layer since the business logic is on the `Logic` project

The application is based on `Caliburn.Micro` and heavily relies on dependency injection for instancing ViewModels

###Dependency injection
The IoC container used for solving dependencies have two configurations:

- **Runtime:** Configured on `AppBootstrapper.Configure` method
- **Design:** Configured on the `DesignViewModelLocator.Configure` method

###Name convention
This is an **MVVM ViewModel-First** application. Since the views are automatically binded to the view models 
the programmer **must** use the following critical conventions:

- **For naming views:** *Name*View
- **For naming view models:** *Name*ViewModel
- Both view and view model should be on **the same namespace**

These are other non critical conventions that the programmer should also follow for improving application scalability:

- Both view and view model should have the same dedicated folder such as:
```
---some folder
	|--Name
		|--NameView.xaml
		|--NameViewModel.cs
	|--OtherName
		|--OtherNameView.xaml
		|--OtherNameViewModel.cs	
```

###Connection string
The connection string is on the `App.config` file

###Binding conventions
Checkout [Caliburn.Micro](http://caliburnmicro.com/) for more conventions.

There are some of the conventions:
- **Label and TextBlock convention**
	```c#
	//C#
	//A message to show to the user
	public string Message { get; set; }
	```


	```xml
	<!--XAML-->
	<TextBlock x:Name="Message" />

	<!--or-->
	<Label x:Name="Message" />

	``` 

- **Button commands and the CanXXX convention**
	- Directly bind buttons to methods by naming the button as the ViewModel method:

	```c#
	//C#
	//This method will be executed when the user press the Ok button:
	public void Ok() { ... }

	//This property will control the enabled/disabled state of the Ok button
	public bool CanOk => true;
	```


	```xml
	<!--XAML-->
	<Button x:Name="Ok" />
	``` 
- **ListBox Items and ItemsSource convention**
	- Directly bind ListBox items and items source from a ViewModel:

	```c#
	//C#
	public List<Customer> Items { get; set; }
	public Customer SelectedItem { get; set; }
	```

	```xml
	<!--XAML-->
	<ListBox x:Name="Items" />

	<!--or-->
	<DataGrid x:Name="Items" />
	``` 

