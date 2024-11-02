v ?= 

newMig: 
	dotnet ef migrations add "$n" $v --project ./src/Infrastructure/Deliver.identity --startup-project ./src/APi/Deliver.Api

updateDB:
	dotnet ef database update $v --project ./src/Infrastructure/Deliver.identity --startup-project ./src/APi/Deliver.Api 

	
