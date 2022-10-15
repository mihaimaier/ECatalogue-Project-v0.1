<h1>Application Project Preview</h1>

<h5>Project Name: Online Catalogue <br>
Application Owner: Mihai Maier <br>
Tools used: Microsoft Visual Studio 2022, SQL Server Management Studio(SSMS), Swagger <br>
Programming Language: C# <br>
Framework: ASP.NET Core 6, Swagger(API) <br>
Architecture Style: 3-tier Architecture
</h5>

<h1>Project Overview</h1>
<h5>
The purpose of the application is to allow an end user to manage an online school catalogue.
The user has the possibility to perform different tasks through 4 different controllers that are setup within the application which are:<br>
 <br>

SeedDb Controller: The controller will allow an end user to delete create a database / insert raw data in the database and ultimately delete the database.

Student Controller: The controller will allow an end user to get a list of the students / get students by id/ add new students / remove students / add marks / update student data / update student address / get average of marks / get a mark for a subject for a student.

Subject Controller: The controller will allow an end user to get all subjects / get subject by id / delete a subject by id.

Teacher Controller: The controller will allow an end user to get all teachers / get teacher by id / add a teacher / assigns or updates a subject for a teacher / delete a teacher from system / change address for a teacher / give a teacher a promotion / return all marks by teacher.

<br>
<br>
New Functionalities


New functionalities that I would like to include within the application are:

- Login Page (allowing different end users to have access to the application)

- Access Rights Ability (a main end user will have super user access and the main end user can assign different access rights to other end user depending on their tasks)

- A reporting ability (The app will allow end user to generate reports based on their needs)
</h5>

<h1>Architecture</h1>

<h5>
Data Layer - The information is stored and retrieved from the SQL Database. The data is then passed to the logic tier (service layer). <br>
<br>
Logic Layer - (Data Service Layer) -  This particular layer coordinates the application, processes commands, makes logical decisions and evaluations and peforms calculations. It also moves and process the data between the surrounding layers. <br>
<br>
Presentation Layer (Controller/API) - The presentation tier is the user interface and communication layer of the application, where the end user interacts with the application. Its main purpose is to display information to and collect information from the user.  </h5>
