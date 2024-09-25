PROJECT_NAME = AspNetTemplate
INFRA_PROJECT = src/$(PROJECT_NAME).Infrastructure
STARTUP_PROJECT = src/$(PROJECT_NAME).WebApi

run:
	@docker compose up -d
	@dotnet run --project $(STARTUP_PROJECT)

add-migration:
	@read -p "Migration name: " migration; \
	dotnet ef migrations add $$migration --project $(INFRA_PROJECT) --startup-project $(STARTUP_PROJECT)

migrate:
	@dotnet ef database update --project $(INFRA_PROJECT) --startup-project $(STARTUP_PROJECT)
