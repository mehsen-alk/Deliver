v ?= 

updateAllDB:
	dotnet ef database update $v --project ./src/Infrastructure/Deliver.Persistence --startup-project ./src/APi/Deliver.Api --context "DeliverDbContext"
	dotnet ef database update $v --project ./src/Infrastructure/Deliver.identity --startup-project ./src/APi/Deliver.Api --context "DeliverIdentityDbContext" 

# make newMig n="init"
newMig: 
	dotnet ef migrations add "$n" $v --project ./src/Infrastructure/Deliver.Persistence --startup-project ./src/APi/Deliver.Api --context "DeliverDbContext"

updateDB:
	dotnet ef database update $v --project ./src/Infrastructure/Deliver.Persistence --startup-project ./src/APi/Deliver.Api --context "DeliverDbContext"

newIdentityMig: 
	dotnet ef migrations add "$n" $v --project ./src/Infrastructure/Deliver.identity --startup-project ./src/APi/Deliver.Api --context "DeliverIdentityDbContext"

updateIdentityDB:
	dotnet ef database update $v --project ./src/Infrastructure/Deliver.identity --startup-project ./src/APi/Deliver.Api --context "DeliverIdentityDbContext" 

	
