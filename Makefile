STARTUP_PROJECT = src/AspNetTemplate
PROJECT_FILE = $(STARTUP_PROJECT)/AspNetTemplate.csproj
API_NAME = aspnet-template-api
DATABASE_NAME = aspnet-template-db

add-migration:
	@read -p "Migration name: " migration; \
	dotnet ef migrations add $$migration \
		--project $(PROJECT_FILE) \
		--startup-project $(STARTUP_PROJECT)

add-package:
	@read -p "Package name: " package; \
	dotnet add "$(PROJECT_FILE)" package $$package

bootstrap:
	@bash scripts/bootstrap.sh

build:
	@dotnet build $(PROJECT_FILE)

migrate:
	@dotnet ef database update \
		--project $(PROJECT_FILE) \
		--startup-project $(STARTUP_PROJECT)

remove-package:
	@read -p "Package name: " package; \
	dotnet remove "$(PROJECT_FILE)" package $$package

run:
	@dotnet run --project $(STARTUP_PROJECT)

version-major:
	@bash scripts/bump_version.sh major

version-minor:
	@bash scripts/bump_version.sh minor

version-patch:
	@bash scripts/bump_version.sh patch

up:
	@docker compose up -d

up-api:
	@docker compose up -d $(API_NAME)

up-db:
	@docker compose up -d $(DATABASE_NAME)
