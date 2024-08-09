using manage_endpoints.Model;

namespace manage_endpoints.Service.Interface;

public interface IEndpointService
{
    void AddEndpoint(Endpoint endpoint);
    void EditEndpoint(string serialNumber, int switchState);
    void DeleteEndpoint(string serialNumber);
    List<Endpoint> GetAllEndpoints();
    Endpoint FindEndpointBySerialNumber(string serialNumber);
}