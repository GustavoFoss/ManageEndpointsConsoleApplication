using manage_endpoints.Model;
using manage_endpoints.Service.Interface;

namespace manage_endpoints.Service;

public class EndpointService : IEndpointService
{
    private readonly List<Endpoint> _endpoints = new List<Endpoint>();

    public void AddEndpoint(Endpoint endpoint)
    {
        if (_endpoints.Any(e => e.SerialNumber == endpoint.SerialNumber))
        {
            throw new InvalidOperationException("Endpoint with the same serial number already exists.");
        }

        _endpoints.Add(endpoint);
    }

    public void EditEndpoint(string serialNumber, int switchState)
    {
        var endpoint = _endpoints.SingleOrDefault(e => e.SerialNumber == serialNumber);

        if (endpoint == null)
        {
            throw new KeyNotFoundException("Endpoint not found.");
        }

        endpoint.UpdateSwitchState(switchState);
    }

    public void DeleteEndpoint(string serialNumber)
    {
        var endpoint = _endpoints.SingleOrDefault(e => e.SerialNumber == serialNumber);

        if (endpoint == null)
        {
            throw new KeyNotFoundException("Endpoint not found.");
        }

        _endpoints.Remove(endpoint);
    }

    public List<Endpoint> GetAllEndpoints()
    {
        return _endpoints.ToList();
    }

    public Endpoint FindEndpointBySerialNumber(string serialNumber)
    {
        return _endpoints.SingleOrDefault(e => e.SerialNumber == serialNumber);
    }
}