#!/bin/bash

# Publishes Unity release to dist/Assemblies using only netstandard2.0 and netstandard2.1
# Does NOT include source generation
#########################################################################################

dotnet restore

assemblyDir="`pwd`/dist/Assemblies"

mkdir -p "${assemblyDir}"

dotnet msbuild /t:Unity -p:PublishDir="${assemblyDir}"
