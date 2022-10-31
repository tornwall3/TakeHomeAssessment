# Instructions

Build and run the Coterie.Api module to spin up the Quote endpoint.  Unit tests can be run with Moq in the Coterie.UnitTests module.

## Endpoint
POST https://localhost:5001/Quote

## Example Payload
{
  "business": "Plumber",
  "revenue": 600000,
  "states": [
    "TX",
    "OH",
	"FL"
  ]
}
