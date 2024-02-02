#!/bin/bash

ENTITIES_PROJECT_PATH="src/Portfolio.Entities"
STARTUP_PROJECT_PATH="src/Portfolio.API"

dotnet ef migrations remove -s "$STARTUP_PROJECT_PATH" -p "$ENTITIES_PROJECT_PATH"

rm -rf "$ENTITIES_PROJECT_PATH/Migrations"

dotnet ef database update -s "$STARTUP_PROJECT_PATH" -p "$ENTITIES_PROJECT_PATH"
