#!/bin/bash

MIGRATION_NAME="$1"
ENTITIES_PROJECT_PATH="src/Portfolio.Entities"
STARTUP_PROJECT_PATH="src/Portfolio.Api"

dotnet ef migrations add "$MIGRATION_NAME" -s "$STARTUP_PROJECT_PATH" -p "$ENTITIES_PROJECT_PATH" -o Migrations

dotnet ef database update -s "$STARTUP_PROJECT_PATH" -p "$ENTITIES_PROJECT_PATH"
