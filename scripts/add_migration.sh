#!/bin/bash

MIGRATION_NAME="$1"
ENTITIES_PROJECT_PATH="src/Portfolio.Entities"
STARTUP_PROJECT_PATH="src/Portfolio.API"

dotnet ef migrations add "$MIGRATION_NAME" --startup-project "$STARTUP_PROJECT_PATH" --project "$ENTITIES_PROJECT_PATH" -o Migrations
