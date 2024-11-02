newMig: 
	dotnet ef migrations add "$n" --project ./src/Infrastructure/Deliver.identity --startup-project ./src/APi/Deliver.Api

updateDB:
	dotnet ef database update --project ./src/Infrastructure/Deliver.identity --startup-project ./src/APi/Deliver.Api

	
