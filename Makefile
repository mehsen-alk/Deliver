v ?= 

# make newMig n="init"
newMig: 
	dotnet ef migrations add "$n" $v --project ./src/Infrastructure/Deliver.Persistence --startup-project ./src/APi/Deliver.Api --context "DeliverDbContext"

updateDB:
	dotnet ef database update $v --project ./src/Infrastructure/Deliver.Persistence --startup-project ./src/APi/Deliver.Api --context "DeliverDbContext"

reInitDocker:
	docker-compose down
	docker rmi deliver.api
	docker-compose up

updateProject:
	git pull
	docker-compose up -d --no-deps --build deliver.api
