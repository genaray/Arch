#!/bin/bash

STATUS=0

main() {
    test Debug
    test Debug-Events
    test Debug-PureECS
    test Release
    test Release-Events
    test Release-PureECS
    exit $STATUS
}

test() {
    dotnet test --configuration "$1" --logger trx --results-directory "TestResults"
    if [[ "$?" != 0 ]]; then
        STATUS=1
    fi
}

main "$@"
