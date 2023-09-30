#!/bin/bash

# Publishes Unity release to dist/Assemblies using only netstandard2.0 and netstandard2.1
#########################################################################################

dotnet restore

assemblyDir="`pwd`/dist/Assemblies"

mkdir -p "${assemblyDir}"

dotnet msbuild /t:Unity -p:PublishDir="${assemblyDir}"

# Unity transitively provides the below libraries. Plus, they are heavy and redistribution-restrictive.
# All of Linq should go hence the wildcard, but System.Threading.Tasks.Extensions is fine (MIT).
rm "${assemblyDir}/System.Linq*" "${assemblyDir}/System.Threading.dll"
