#!/bin/bash

dotnet test --configuration Debug --logger trx --results-directory "TestResults" || exit 1
dotnet test --configuration Debug-PureECS --logger trx --results-directory "TestResults" || exit 1
dotnet test --configuration Debug-Events --logger trx --results-directory "TestResults" || exit 1
dotnet test --configuration Release --logger trx --results-directory "TestResults" || exit 1
dotnet test --configuration Release-PureECS --logger trx --results-directory "TestResults" || exit 1
dotnet test --configuration Release-Events --logger trx --results-directory "TestResults" || exit 1
