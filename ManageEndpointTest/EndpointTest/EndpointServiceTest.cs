using manage_endpoints.Model;
using manage_endpoints.Service;
using manage_endpoints.Service.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ManageEndpointTest.EndpointTest
{
    [TestClass]
    public class EndpointServiceTest
    {
        private Mock<IEndpointService> _mockEndpointService;
        private Endpoint _endpoint;

        [TestInitialize]
        public void Setup()
        {
            _mockEndpointService = new Mock<IEndpointService>();

           
            _endpoint = new Endpoint("SN1", 16, 123, "v1.0", 0);
        }

        [TestMethod]
        public void AddEndpoint_ShouldAddEndpoint_WhenValid()
        {
   
            _mockEndpointService.Setup(service => service.FindEndpointBySerialNumber("SN1"))
                .Returns(_endpoint);

      
            _mockEndpointService.Object.AddEndpoint(_endpoint);

         
            _mockEndpointService.Verify(service => service.AddEndpoint(It.IsAny<Endpoint>()), Times.Once);
            Assert.AreEqual("SN1", _mockEndpointService.Object.FindEndpointBySerialNumber("SN1")?.SerialNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEndpoint_ShouldThrowException_WhenSerialNumberExists()
        {
            _mockEndpointService.Setup(service => service.FindEndpointBySerialNumber("SN1"))
                .Returns(_endpoint);
            
            _mockEndpointService.Object.AddEndpoint(_endpoint);
            _mockEndpointService.Object.AddEndpoint(_endpoint); // This should throw an exception
        }

        [TestMethod]
        public void EditEndpoint_ShouldUpdateSwitchState_WhenValid()
        {
            _mockEndpointService.Setup(service => service.FindEndpointBySerialNumber("SN1"))
                .Returns(_endpoint);
            
            _mockEndpointService.Object.EditEndpoint("SN1", 1);
            
            _mockEndpointService.Verify(service => service.EditEndpoint("SN1", 1), Times.Once);
            Assert.AreEqual(1, _mockEndpointService.Object.FindEndpointBySerialNumber("SN1")?.SwitchState);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void EditEndpoint_ShouldThrowException_WhenEndpointNotFound()
        {
            _mockEndpointService.Setup(service => service.FindEndpointBySerialNumber("SN1"))
                .Returns((Endpoint)null); // Endpoint not found
            
            _mockEndpointService.Object.EditEndpoint("SN1", 1);
        }

        [TestMethod]
        public void DeleteEndpoint_ShouldRemoveEndpoint_WhenValid()
        {
            _mockEndpointService.Setup(service => service.FindEndpointBySerialNumber("SN1"))
                .Returns((Endpoint)null); // Simulate deletion
            
            _mockEndpointService.Object.DeleteEndpoint("SN1");
            
            _mockEndpointService.Verify(service => service.DeleteEndpoint("SN1"), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void DeleteEndpoint_ShouldThrowException_WhenEndpointNotFound()
        {
            _mockEndpointService.Setup(service => service.FindEndpointBySerialNumber("SN1"))
                .Returns((Endpoint)null); // Endpoint not found
            
            _mockEndpointService.Object.DeleteEndpoint("SN1");
        }
    }
}
