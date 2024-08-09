using manage_endpoints.Model;
using manage_endpoints.Service.Interface;

namespace manage_endpoints.Service;

public class EndpointService : BaseService<Endpoint>, IEndpointService
{
    public void AddEndpoint(Endpoint endpoint)
    {
        if (_items.Any(e => e.SerialNumber == endpoint.SerialNumber))
        {
            throw new InvalidOperationException("Endpoint with the same serial number already exists.");
        }

        AddItem(endpoint);
    }

    public void EditEndpoint(string serialNumber, int switchState)
    {
        var endpoint = FindItemByCondition(e => e.SerialNumber == serialNumber);

        if (endpoint == null)
        {
            throw new KeyNotFoundException("Endpoint not found.");
        }

        endpoint.UpdateSwitchState(switchState);
    }

    public void DeleteEndpoint(string serialNumber)
    {
        var endpoint = FindItemByCondition(e => e.SerialNumber == serialNumber);

        if (endpoint == null)
        {
            throw new KeyNotFoundException("Endpoint not found.");
        }

        _items.Remove(endpoint); 
    }

    public new List<Endpoint> GetAllEndpoints()
    {
        return GetAllItems();
    }

    public Endpoint FindEndpointBySerialNumber(string serialNumber)
    {
        return FindItemByCondition(e => e.SerialNumber == serialNumber);
    }
}