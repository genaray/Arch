#!/bin/bash
#
# Script to build and publish nuget packages.
#
# See usage() for usage.

ME="$(basename "${BASH_SOURCE[0]}")"
NUGET_SOURCE=https://api.nuget.org/v3/index.json

#
# usage
#
usage() {
    cat <<EOF
${ME} -- Script to build and publish a set of nuget packages.

Usage:

    ${ME} [--[no-]build] [--[no-]publish] [--test] [--api-key <key>]

The default behaviour is to build packages but not publish them.

When publishing packages, a nuget API key must be specified.
EOF
}

#
# main -- Build and publish nuget packages.
#
main() {
    
    # Parse arguments.
    BUILDING=True
    PUBLISHING=
    while [[ ! -z "$1" ]]; do
        arg="$1"
        shift
        case "$arg" in
            --test)
                TESTING=True
                ;;
            --build)
                BUILDING=True
                ;;
            --no-build)
                BUILDING=
                ;;
            --publish)
                PUBLISHING=True
                ;;
            --no-publish)
                PUBLISHING=
                ;;
            --api-key)
                API_KEY="$1"
                shift
                ;;
            -h|--help|-?)
                usage
                exit 0
                ;;
            *)
                usage
                error "Unknown argument: $arg"
                ;;
        esac
    done

    if [[ ! -f "src/Arch/Arch.csproj" ]]; then
        error "Must be run in repository root."
    fi

    if [[ ! -z "$PUBLISHING" && -z "$API_KEY" ]]; then
        error "To publish, API_KEY must be specified via environment or --api-key."
    fi

    if [[ ! -z "$BUILDING" ]]; then
        buildall
    fi

    if [[ ! -z "$PUBLISHING" ]]; then
        publish
    fi
}

#
# Hack the package name / id in the Arch cs project. This is the only way I have
# found to make the package name depend on a binary variant. Perhaps there is a
# better way...
#
hackcsproj() {
    doit cp src/Arch/Arch.csproj src/Arch/Arch.csproj.bak
    doit sed -i "s#<PackageId>Arch</PackageId>#<PackageId>Arch-$1</PackageId>#" src/Arch/Arch.csproj
    doit sed -i "s#<Title>Arch</Title>#<Title>Arch-$1</Title>#" src/Arch/Arch.csproj
}

#
# Put the project file back how it was.
#
unhackcsproj() {
    if [[ -f src/Arch/Arch.csproj.bak ]]; then
        doit mv src/Arch/Arch.csproj.bak src/Arch/Arch.csproj
    fi
}

#
# Display an error message and exit, ensuring that any change to the
# csproj file was reverted.
#
error() {
    >&2 echo "ERROR" "$@"
    unhackcsproj
    exit 1
}

#
# Execute and print a command line.
#
# Note: do not pass secrets into this function!
#
doit() {
    >&2 echo "$@"
    if [[ -z "$TESTING" ]]; then
        "$@" || error
    fi
}

#
#
# build <config> [variant]
# 
# Build one binary variant.
#
build() {
    local config="$1"
    if [[ -z "$config" ]]; then
        error "Config not specified."
    fi
    local variant="$2"

    # Determine preset to build, and suffix to append to the package name. For
    # release packages we don't include 'Release' in the suffix, but for 'Debug'
    # packages we do include 'Debug'.
    local preset="$config"
    local suffix=
    if [[ "$config" == Debug ]]; then
        suffix=Debug
    fi
    if [[ ! -z "$variant" ]]; then
        preset="$config-$variant"
        if [[ ! -z "$suffix" ]]; then
            suffix="$suffix-"
        fi
        suffix="$suffix$variant"
    fi

    # Now hack the cs proj to append the suffix to the package name if
    # necessary, then do the build.
    if [[ ! -z "$suffix" ]]; then
        hackcsproj "$suffix"
    fi
    doit dotnet build -c "$preset"
    doit dotnet pack -c "$preset" src/Arch/Arch.csproj
    if [[ ! -z "$suffix" ]]; then
        unhackcsproj
    fi
}

#
# buildall
#
# Build all variants.
#
buildall() {
    find src -name "*.nupkg" -delete
    doit dotnet clean
    doit dotnet restore
    for config in Debug Release; do
        build "$config"
        for variant in PureECS Events; do
            build $config $variant
        done
    done
    find src -name "*.nupkg"
}

#
# publish
#
# Publish built packages.
#
# API_KEY must be set.
#
publish() {
    # Note: the slightly odd construct below allows us to loop through the
    # output of 'find'.
    #   - -print0 ensures that the output of 'find' is null-delimited rather
    #     newline-delimited. This handles the rare case of newlines in the
    #     output.
    #   - 'IFS= read -r line' is used to read line-by-line from the input
    #   - '-d ''' makes 'read' null-delimited to match 'find'
    #   - The PIPESTATUS stuff catches errors from within the pipe -- otherwise
    #     if our code inside the loop failed, it would be silently ignored.
    find src -name "*.nupkg" -print0 |
        while IFS= read -r -d '' line; do

            # Push each package, being careful not to print secrets to the
            # output.
            >&2 echo dotnet nuget push "$line" --api-key '***' --source "$NUGET_SOURCE"
            if [[ -z "$TESTING" ]]; then
                dotnet nuget push "$line" --api-key "$API_KEY" --source "$NUGET_SOURCE" || exit 1
            fi

        done
    if [[ "${PIPESTATUS[@]}" != "0 0" ]]; then
        exit 1
    fi
}

main "$@"