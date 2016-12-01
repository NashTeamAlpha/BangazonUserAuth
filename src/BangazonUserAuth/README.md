## BangazonUserAuth Initial Merchant Site

Welcome to BangazonUserAuth! We want to help you sell your products to the entire world. 

To install this app, you'll need to have dotnet and bower. 

- If you need to install dotnet, you can do so from this [Microsoft link](https://www.microsoft.com/en-us/download/details.aspx?id=30653)
- if you need to install bower, you can do so from this [Bower link](https://bower.io/)

Copy or fork the repository on GitHub and save it on to your local machine. Install the bower dependencies by typing: 

```bower install```

This app uses a database that it creates. You will need to set an environmental variable that sets the path to this database. Locate the folder your project is saved in and note the path into that folder. 

- OSX/UNIX:
To set your environmental variable to the local database you'll need to type this into your terminal:

```export NTABangazonUserAuth_Db_Path="/yourpath/yourpath/yourpath/BangazonUserAuth.db```

- Windows OS:
To set your environmental variable to the local database you'll need to type this into your Powershell terminal:

`$env:NTABangazonUserAuth_Db_Path="/yourpath/yourpath/yourpath/BangazonUserAuth.db`


After this database path has been set, create the database and set up the dotnet dependencies by running the following commands:

```
dotnet restore
dotnet ef database update
dotnet run
```

Once the above installation requirements are met, the site should be available on your local host. 