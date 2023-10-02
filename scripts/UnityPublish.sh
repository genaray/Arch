#!/bin/bash

# Publishes Unity release to dist/Assemblies using only netstandard2.0 and netstandard2.1
#########################################################################################

dotnet restore

assemblyDir="`pwd`/dist/Assemblies"

rm -rf "${assemblyDir}"

mkdir -p "${assemblyDir}"

dotnet msbuild /t:Unity \
    -p:PublishDir="${assemblyDir}" \
    -p:TargetFramework=netstandard2.1 \
    -p:TargetFrameworks=netstandard2.1 \
    -p:TargetFrameworkVersion=v2.1
