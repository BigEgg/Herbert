param (
    [string]$environment
)

IF ($environment) {
    WRITE-HOST "Run Test with $environment Enviroment"
    $env:TEST_ENVIRONMENT = "$environment"
} ELSE {
    WRITE-HOST "Run Test in local with Development Enviroment"
}

WRITE-HOST "Run Tests for DAL" -ForegroundColor "Yellow"
CD ..\Herbert.DAL.Tests
dotnet test
WRITE-HOST "Run Tests for DAL Done" -ForegroundColor "Cyan"

WRITE-HOST "Run Tests for Business Logic" -ForegroundColor "Yellow"
CD ..\Herbert.Services.Tests
dotnet test
WRITE-HOST "Run Tests for Business Logic Done" -ForegroundColor "Cyan"

WRITE-HOST "Run Tests for API Layer" -ForegroundColor "Yellow"
CD ..\Herbert.API.Tests
dotnet test
WRITE-HOST "Run Tests for API Layer Done" -ForegroundColor "Cyan"

CD ..\Scripts
