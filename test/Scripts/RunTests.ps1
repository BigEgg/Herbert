param (
    [string]$environment
)

IF ($environment) {
    WRITE-OUTPUT "Run Test with $environment Enviroment"
    $env:TEST_ENVIRONMENT = "$environment"
} ELSE {
    WRITE-OUTPUT "Run Test in local with Development Enviroment"
}

WRITE-OUTPUT "Run Tests for DAL"
CD ..\Herbert.DAL.Tests
dotnet test
WRITE-OUTPUT "Run Tests for DAL Done"

WRITE-OUTPUT "Run Tests for Business Logic"
CD ..\Herbert.Services.Tests
dotnet test
WRITE-OUTPUT "Run Tests for Business Logic Done"

CD ..\Scripts
