# DevliftMedia
DevliftMedia ASP.NET App 

This is a website that shows a local event list. 
The main features are: 
 - Role management and access control
 - Full CRUD for the event table 
 - Remote Db in SQL Server to keep the events data (Picture, Title, description and so on)
 - User register and login 


The data is validated on the server side by metadata class. The validations are only to show the functionality, most of the form fields are not being checked, as well as the picture insertion. The Db was designed to allow all the fields as null, as it would be for didactic purposes only. 

The web app has 3 access levels, Developer (Full access to all features), Admin (access to everything but deleting a role), and general access, that can see the events but not editing, deleting or creating any of them. 

To test the features the login details are: 

admin@admin.com
Password123!

developer@developer.com
Password123!

Thanks for the opportunity, and enjoy the code. 
