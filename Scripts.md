# 1. Setup project directory
```powershell
mkdir Sample
cd Sample
git clone https://github.com/errrzarrr/sample-dotnet-core-cqrs-api.git
cd sample-dotnet-core-cqrs-api
git branch onelvis-devops-challenge
git checkout onelvis-devops-challenge
```
# 2. Create database
```powershell
[reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo")
$server = new-object ("Microsoft.SqlServer.Management.Smo.Server") "localhost"

$dbExists = $FALSE
foreach ($db in $server.databases) {
  if ($db.name -eq "RobertChallenge") {
    Write-Host "Db already exists."
    $dbExists = $TRUE
  }
}

if ($dbExists -eq $FALSE) {
  $db = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Database -argumentlist $server, "RobertChallenge"
  $db.Create()

  $user = "NT AUTHORITY\NETWORK SERVICE"
  $usr = New-Object -TypeName Microsoft.SqlServer.Management.Smo.User -argumentlist $db, $user
  $usr.Login = $user
  $usr.Create()

  $role = $db.Roles["db_datareader"]
  $role.AddMember($user)
  Invoke-SqlCmd -ServerInstance localhost -Database RobertChallenge -InputFile .\src\InitializeDatabase.sql
}


```

# 3 (2). Create environment variables (Powershell)
```powershell
# Challenge
[Environment]::SetEnvironmentVariable("UserSecretKey", "TestKey", "User")

# Project
[Environment]::SetEnvironmentVariable("ASPNETCORE_SampleProject_IntegrationTests_ConnectionString", "Data Source=localhost;Initial Catalog=RobertChallenge;Integrated Security=True", "User")
```

# 4. Creating the project that holds the method for retrieving the environment variables
```powershell 
cd src

#Creating the project
dotnet new classlib -o SampleProject.Library -f netcoreapp3.0 -lang "C#"

#Adding project to solution
dotnet sln SampleProject.API.sln add SampleProject.Library/SampleProject.Library.csproj

```
# 4. Creating the test project for the new classes
```powershell
#Creating the directory
mkdir SampleProject.Library.Tests
cd SampleProject.Library.Tests

#Adding Nunit to project
dotnet new nunit

# Adding the reference of the project that contains the methods to be tested
dotnet add reference ../SampleProject.Library/SampleProject.Library.csproj

cd ..

# Adding reference of the test project to the solution
dotnet sln SampleProject.API.sln add .\SampleProject.Library.Tests\SampleProject.Library.Tests.csproj
```
# 5. Restoring dependencies, running test, building and running solution
```powershell
# Restoring dependencies
dotnet restore

# Running tests
dotnet test

# Building solution
dotnet build

# Running solution
cd SampleProject.API
dotnet run
```

# 6. Adding new method's definition to README file
```bash
#!/bin/bash
echo "# GetUserPrivateKey  
This method returns the value of the environment variable that holds the user's private key.  
# GetConnectionString
This method returns the value of the environment variable that holds the connection string" >> README.md
```