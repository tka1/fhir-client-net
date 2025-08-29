//----------------------------------------------------------------------------------------------------------
// Imports
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;


//----------------------------------------------------------------------------------------------------------
// Part 1

// Creation of an htpclient holding the api key of the server as an header
var httpClient = new HttpClient();
//httpClient.DefaultRequestHeaders.Add("x-api-key", "api-key");


var settings = new FhirClientSettings
{
    Timeout = 0,
    PreferredFormat = ResourceFormat.Json,
    VerifyFhirVersion = true,
    // PreferredReturn can take Prefer.ReturnRepresentation or Prefer.ReturnMinimal to return the full resource or an empty payload
    PreferredReturn = Prefer.ReturnRepresentation
};
// Creation of our client using the right url
var client = new FhirClient("http://server.fire.ly/", httpClient, settings);


//----------------------------------------------------------------------------------------------------------
// Part 2


var patient0 = new Patient();
patient0.Name.Add(new HumanName().WithGiven("GivenName2").AndFamily("FamilyName2"));

// Creation of our client in the server
// It is to be noted that using SearchParams you can check if an equivalent resource already exists in the server
// For more information https://docs.fire.ly/projects/Firely-NET-SDK/client/crud.html
var created_pat = client.Create<Patient>(patient0);

Console.Write("Part 2 : Newly created patient Id : ");
Console.WriteLine(created_pat.Id);

var location_A = new Uri("http://server.fire.ly/Patient/" + created_pat.Id);
var pat_A = client.Read<Patient>(location_A);
var name = pat_A.Name[0];
Console.WriteLine($"Patient Name: {name.GivenElement[0]} {name.Family}");


