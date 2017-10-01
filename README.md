# Asp.net web api 2.2 starter template
Asp.net web api 2.2 starter template using repository and unit of work patterns.  
I use this starter template for my company's projects including projects for government and for industry companies.  
* Use entity framework 6.2 Code First to manipulate the database.
* Use autofac as ioc container.  
* Use popular libraries like: autofac, automapper, Nlog...  
* Use a entity base class as base for all models.
* Use EntityBaseRepository as base for all repositories.  
* Use ApiControllerBase as base for all controllers.


## Logging
I use serilog for logging.  
The Serilog configuration is located in SerilogConfiguration.cs under the web project.  
I add 3 sinks of serilogs, they are: mssql, rolling-file and output debug.  
The minimum level for mssql is information level.  
The minimum level for rolling file is debug level.  
There is no minimum level for output debug window.  
You can change all of these in SerilogConfiguration.cs.  

#### About the mssql sink:  
The default configuration of autoCreateSqlTable for mssql sink is true, and if you change the settings for mssql sink, the mssql logging may not work any more, then you should delete the log table and let the application create the log table again.  

#### Dependency injection of Serilog
The dependency injection of serilog has been configured in AutofacWebapiConfig.cs.  
You can inject the logger (ILogger) in any controller or services or any class you want.  
I've already inject the ILogger into CommonService.

#### In base controller
I've injected ILogger into any controller that derived from ApiControllerBase throung ICommonService.  
I also implemented some protected Log methods in ApiControllerBase, you can see that region and you can use these methods in the derived controllers. You can still access the ILogger and logging whatever you want.

#### Global Exception Logger
I use the static version of serilog in MyExceptionLogger for global exception logging.
