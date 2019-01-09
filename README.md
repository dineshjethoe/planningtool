
# Planning Tool
> ##### KLM technical test/assignment (13 Dec. 2018)

## Instructions to build and configure:
1. Open the solution file (.sln) in VS and build the solution (Ctrl+Shift+B)
2. Right click on the Database project (named 'Database') and select Publish. Publish/deploy the database to a default local SQL server instance (no express edition, otherwise the connection string must be edited in the .config files of WinApp and Repositories).The database name would be KlmPlanningToolDb. Or use the profile file 'https://github.com/dineshjethoe/planningtool/blob/master/Database/Database.publish.xml' for the database deployment. 
3. Run the application, add a task and an employee and then make an assignment. There will be six icons (buttons) on the menu, (from left to right) Tasks, Employees, Assignments, Add, Edit and Remove. 

## Overview
This application is the result of an assignment for a job opportunity at KLM Royal Dutch Airlines.

## Considerations and assumptions
The application is developed based on the following considerations:
*	No person can do 2 tasks at the same time
*	Employees can only work max 8 hours per day
*	Employees should have 2 days off per week
*	The tasks are assigned to employees in sequential order. For example: when a task is assigned to an employee from 9 am to 11 am, then new assignment must be assigned after 11 am and not earlier than 9 am on the same assignment date. 
*	A task is assigned for a date, not over a period.  

## Constraints
There is a time frame to this assignment. Approx. 6-8 hours. 

## Patterns
### Architectural Patterns
The following software design patterns are used:
#### N-tier
The application project structure is organized based on the N-Tier pattern.
The solution consists of the following layers:
*	Data layer
This layer has two library projects:
 *	Entities: this consist of the models or business objects and is used across the solution. 
 *	Repositories: this provides the data access to the database via Entity Framework (ORM)
*	Business Logic layer
This layer has the validation logic provided as a service to the caller.
*	Presentation layer
This is the WinForms project consisting of user interfaces based on forms and views (reusable user controls).
*	Tests
This is the test project to test the functions in the business logic.

#### Model View Presenter (MVP)
The MVP patterns is used for the reuse of interfaces. This is accomplished using user controls which are the views. The application has two presenters namely DialogFormPresenter and the MainFormPresenter. These presenters intermediates between the views and the model (entities). 

### Design Patterns
#### Repository pattern
This is responsible to encapsulate the logic of the data access. A generic repository class is used to DRY principle. This way we can avoid creating multiple repositories for each entity. 
#### Unit of Work pattern
This pattern is used to group the CRUD (Create, Read, Update and Delete) operations on the repositories (data).
#### Event aggregator pattern
This pattern helps with update data in multiple views from other views using event-based messages. 
#### Command pattern
This application uses a toolstrip control as the menu. The buttons on the menu executes similar actions for multiple modules (entity/views). These actions could be reused thanks to the command pattern. 

## Design Principles
### Don’t repeat yourself (DRY) and Liskov substitution
By using C# generic features and general object-oriented programming practices (C# Interfaces) most of the views and functionalities could be reused. For example: the DataListView view is reused to display the data of each entity (task, employee, assigned task).
### Inversion of control
In order to develop this application in a modular fashion the Inversion of Control pattern is used. 

## Tools and frameworks
* Integrated development environment: Visual studio 2017
* SQL Server Management Studio (SSMS)
* Object relational mapping: Entity Framework 6
* Test framework: NUnit version 3
* Inversion of control container: Castle Windsor (IoC container)
* Runtime: .NET 4.6.1

## Resources used
The icons are taken from icons8.com

## Database
ORM data modeling approaches: database first.
A database objects are created in Visual Studio (SQL Server Data Tools). This is chosen because of being able to keep version control the database objects and easy deployment. 
The database is tested by deploying on a MS SQL Server 2016 database instance.
![Entity Relation Diagram](https://github.com/dineshjethoe/planningtool/blob/master/img/erd_planningtool_klm.png)

## Algorithms
The most important logic is the assignment validation. Here is an attempt to depict the logic flow:
![Algorithm](https://github.com/dineshjethoe/planningtool/blob/master/img/algorithm_planningtool_klm.png)




