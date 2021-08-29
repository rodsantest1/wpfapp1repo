# Simple WPF and Web API solution <br/>(with ReactiveUI)  

9/18/20  
Web API - .Net Core 3.1  
WPF App - .Net Framework 4.8  
Unit Test Project - .Net Core 3.1  

### Description
Adds 2 numbers together without needing to click an Add button. This simple app calls an add service available through an API.

![AddImage](Image1.PNG)

#### Running solution:  
1. Select WebApplication1 as Startup project
2. Change the startup web server from IISExpress to WebApplication1
3. If you get error like following, restart Visual Studio

A numeric comparison was attempted on "$(MsBuildMajorVersion)" that evaluates to "" instead of a number, in condition "($(MsBuildMajorVersion) < 16)"  


4. Select **Multiple startup projects** option in solution properties.  
5. Select Start option for API project  
6. Select Start option for WPF project  
7. Click Apply and start the app

#### Running Appium Test  
Prerequisites: 
1. Test machine has to be in **Developer Mode**
2. Install **WinAppDriver**
3. Visit [site](https://github.com/microsoft/WinAppDriver) for more details  

Start the API:
1. Open command line in WebApplication1 bin/Debug/netcoreapp3.1 folder
2. Type WebApplication1.exe and press enter  

Run the test:  
1. Right-click in test method and select Debug Test or Run Test

8/28/21 App Crash
If can't connect to API the app crashes.


