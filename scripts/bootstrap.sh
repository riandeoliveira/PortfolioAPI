#!/bin/bash

clear
echo "===== ASP.NET Template | Bootstrap ====="
echo

read -p "Project name (ex: Task List): " project_name

pascal_case=$(echo "$project_name" | sed -E 's/[^a-zA-Z0-9 ]//g' | awk '{for(i=1;i<=NF;i++){ $i=toupper(substr($i,1,1)) tolower(substr($i,2)) }}1' | tr -d ' ')

kebab_case=$(echo "$project_name" | iconv -t ascii//TRANSLIT | \
  tr '[:upper:]' '[:lower:]' | sed -E 's/[^a-z0-9]+/-/g; s/^-|-$//g')

snake_case=$(echo "$kebab_case" | tr '-' '_')

old_uuid="4F7D9F14-2786-4660-8209-F29F7394431A"

find . -type f \
  ! -path "./.git/*" \
  ! -name "$(basename "$0")" \
  -exec sed -i \
    -e "s/AspNetTemplate/$pascal_case/g" \
    -e "s/aspnet-template/$kebab_case/g" \
    -e "s/aspnet_template/$snake_case/g" \
  {} +

mv "AspNetTemplate.sln" "$pascal_case.sln"
mv "src/AspNetTemplate/AspNetTemplate.csproj" "src/AspNetTemplate/$pascal_case.csproj"
mv "src/AspNetTemplate" "src/$pascal_case"

new_uuid=$(uuidgen | tr '[:lower:]' '[:upper:]')

sln_file=$(ls "$pascal_case.sln" 2>/dev/null)

csproj_file="src/$pascal_case/$pascal_case.csproj"

script_name="$(basename "$0")"

sed -i "s/$old_uuid/$new_uuid/g" "$sln_file"
sed -i 's#<Version>.*</Version>#<Version>0.1.0</Version>#g' "$csproj_file"
sed -i '/^bootstrap:/,+2d' "Makefile"

rm -rf .git
rm -f "$script_name"

echo
echo "✅ Project ready to start! Have a nice coding ;)"
