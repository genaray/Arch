#!/bin/bash

dotnet test --configuration Debug || exit 1
dotnet test --configuration Debug-PureECS || exit 1
dotnet test --configuration Debug-Events || exit 1
dotnet test --configuration Release || exit 1
dotnet test --configuration Release-PureECS || exit 1
dotnet test --configuration Release-Events || exit 1
