using manage_endpoints.Model;
using manage_endpoints.Service;
using manage_endpoints.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ManageEndpointTest.EndpointTest;

[TestClass]
public class EndpointServiceTest
{
    private IEndpointService _endpointService = new EndpointService();

    [TestInitialize]
    public void Setup()
    {
        _endpointService = new EndpointService();
    }

    [TestMethod]
    public void AddEndpoint_ShouldAddEndpoint_WhenValid()
    {
        // Arrange
        var endpoint = new Endpoint("SN1", 16, 123, "v1.0", 1);

        // Act
        _endpointService.AddEndpoint(endpoint);

        // Assert
        var result = _endpointService.FindEndpointBySerialNumber("SN1");
        Assert.AreEqual("SN1", result.SerialNumber);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddEndpoint_ShouldThrowException_WhenSerialNumberExists()
    {
        // Arrange
        var endpoint1 = new Endpoint("SN1", 16, 123, "v1.0", 1);
        var endpoint2 = new Endpoint("SN1", 17, 456, "v1.1", 0);

        // Act
        _endpointService.AddEndpoint(endpoint1);
        _endpointService.AddEndpoint(endpoint2);
    }

    [TestMethod]
    public void EditEndpoint_ShouldUpdateSwitchState_WhenValid()
    {
        // Arrange
        var endpoint = new Endpoint("SN1", 16, 123, "v1.0", 0);
        _endpointService.AddEndpoint(endpoint);

        // Act
        _endpointService.EditEndpoint("SN1", 1);

        // Assert
        var result = _endpointService.FindEndpointBySerialNumber("SN1");
        Assert.AreEqual(1, result.SwitchState);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void EditEndpoint_ShouldThrowException_WhenEndpointNotFound()
    {
        // Act
        _endpointService.EditEndpoint("SN1", 1);
    }

    [TestMethod]
    public void DeleteEndpoint_ShouldRemoveEndpoint_WhenValid()
    {
        // Arrange
        var endpoint = new Endpoint("SN1", 16, 123, "v1.0", 1);
        _endpointService.AddEndpoint(endpoint);

        // Act
        _endpointService.DeleteEndpoint("SN1");

        // Assert
        var result = _endpointService.FindEndpointBySerialNumber("SN1");
        Assert.AreEqual(null,result);
    }

    [TestMethod]
    [ExpectedException(typeof(KeyNotFoundException))]
    public void DeleteEndpoint_ShouldThrowException_WhenEndpointNotFound()
    {
        // Act
        _endpointService.DeleteEndpoint("SN1");
    }
}