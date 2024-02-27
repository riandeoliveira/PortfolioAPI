#!/bin/bash

DOMAIN_PROJECT_PATH="src/Portfolio.DOMAIN"
STARTUP_PROJECT_PATH="src/Portfolio.Api"

dotnet ef migrations remove -s "$STARTUP_PROJECT_PATH" -p "$DOMAIN_PROJECT_PATH"

rm -rf "$DOMAIN_PROJECT_PATH/Migrations"

dotnet ef database update -s "$STARTUP_PROJECT_PATH" -p "$DOMAIN_PROJECT_PATH"
