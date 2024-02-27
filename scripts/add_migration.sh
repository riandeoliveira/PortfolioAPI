#!/bin/bash

MIGRATION_NAME="$1"
DOMAIN_PROJECT_PATH="src/Portfolio.Domain"
STARTUP_PROJECT_PATH="src/Portfolio.Api"

dotnet ef migrations add "$MIGRATION_NAME" -s "$STARTUP_PROJECT_PATH" -p "$DOMAIN_PROJECT_PATH" -o Migrations

dotnet ef database update -s "$STARTUP_PROJECT_PATH" -p "$DOMAIN_PROJECT_PATH"
