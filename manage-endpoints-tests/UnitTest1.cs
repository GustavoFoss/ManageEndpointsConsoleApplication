namespace manage_endpoints_tests;

[TestClass]
public class EndpointServiceTests
{
    private EndpointService _endpointService;

    [TestInitialize]
    public void Setup()
    {
        _endpointService = new EndpointService();
    }

    [TestMethod]
    public void AddEndpoint_ShouldAddEndpoint_WhenValid()
    {
        var endpoint = new Endpoint("SN1", 16, 123, "v1.0", 1);
        _endpointService.AddEndpoint(endpoint);

        var result = _endpointService.FindEndpointBySerialNumber("SN1");
        Assert.IsNotNull(result);
        Assert.AreEqual("SN1", result.SerialNumber);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddEndpoint_ShouldThrowException_WhenSerialNumberExists()
    {
        var endpoint1 = new Endpoint("SN1", 16, 123, "v1.0", 1);
        var endpoint2 = new Endpoint("SN1", 17, 456, "v1.1", 0);

        _endpointService.AddEndpoint(endpoint1);
        _endpointService.AddEndpoint(endpoint2);
    }

    [TestMethod]
    public void EditEndpoint_ShouldUpdateSwitchState_WhenValid()
    {
        var endpoint = new Endpoint("SN1", 16, 123, "v1.0", 0);
        _endpointService.AddEndpoint(endpoint);

        _endpointService.EditEndpoint("SN1", 1);
        var result = _endpointService.FindEndpointBySerialNumber("SN1");

        Assert.AreEqual(1, result.SwitchState);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void EditEndpoint_ShouldThrowException_WhenEndpointNotFound()
    {
        _endpointService.EditEndpoint("SN1", 1);
    }
}