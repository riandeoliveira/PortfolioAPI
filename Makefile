PROJECT_NAME = AspNetTemplate
INFRA_PROJECT = src/$(PROJECT_NAME).Infra.Data
STARTUP_PROJECT = src/$(PROJECT_NAME).Api

add-migration:
	@read -p "Migration name: " migration; \
	dotnet ef migrations add $$migration --project $(INFRA_PROJECT) --startup-project $(STARTUP_PROJECT)

build:
	@dotnet build

migrate:
	@dotnet ef database update --project $(INFRA_PROJECT) --startup-project $(STARTUP_PROJECT)

run:
	@docker compose up -d
	@dotnet run --project $(STARTUP_PROJECT)
