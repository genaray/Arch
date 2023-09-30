#!/bin/bash

# Publishes Unity release to dist/Assemblies using only netstandard2.0 and netstandard2.1
#########################################################################################

dotnet restore

mkdir -p dist/Assemblies

dotnet msbuild /t:Unity -p:PublishDir=`pwd`/dist/Assemblies

# Unity transitively provides the below libraries. Plus, they are heavy and redistribution-restrictive.
# All of Linq should go hence the wildcard, but System.Threading.Tasks.Extensions is fine (MIT).
rm `pwd`/dist/Assemblies/System.Linq* `pwd`/dist/Assemblies/System.Threading.dll
