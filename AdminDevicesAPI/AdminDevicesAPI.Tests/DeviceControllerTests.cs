using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using AdminDevicesAPI.Controllers;
using AdminDevicesAPI.Data;
using AdminDevicesAPI.DTOs;
using AdminDevicesAPI.Models;
using AdminDevicesAPI.Repositories;
using AdminDevicesAPI.Responses;
using AdminDevicesAPI.Services;
using Xunit;

public class DeviceControllerTests
{
    private DeviceController CreateController(out DeviceContext context)
    {
        var options = new DbContextOptionsBuilder<DeviceContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        context = new DeviceContext(options);
        var repository = new DeviceRepository(context);
        var service = new DeviceService(repository);
        var logger = new LoggerFactory().CreateLogger<DeviceController>();

        return new DeviceController(service, logger);
    }

    [Fact]
    public async Task PostDevice_ValidDevice_ReturnsCreatedAtAction()
    {
        var controller = CreateController(out _);
        var newDevice = new DeviceCreateDTO { Name = "New Device", Status = DeviceStatus.Active, Type = "Sensor" };

        var actionResult = await controller.PostDevice(newDevice);

        var createdResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var deviceResponse = Assert.IsType<DeviceResponseDTO>(createdResult.Value);

        Assert.Equal(newDevice.Name, deviceResponse.Name);
        Assert.Equal(newDevice.Status, deviceResponse.Status);
        Assert.Equal(newDevice.Type, deviceResponse.Type);
    }

    [Fact]
    public async Task PostDevice_InvalidModel_ReturnsBadRequest()
    {
        var controller = CreateController(out _);
        controller.ModelState.AddModelError("Name", "The Name field is required.");
        var newDevice = new DeviceCreateDTO { Name = "", Status = DeviceStatus.Active, Type = "Sensor" };

        var actionResult = await controller.PostDevice(newDevice);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
        Assert.True(modelState.ContainsKey("Name"));
    }

    [Fact]
    public async Task GetDevice_ExistingId_ReturnsDevice()
    {
        var controller = CreateController(out var context);
        var device = new Device { Name = "Test Device", Status = DeviceStatus.Active, Type = "Sensor" };
        context.Devices.Add(device);
        await context.SaveChangesAsync();

        var actionResult = await controller.GetDevice(device.Id);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var deviceResponse = Assert.IsType<DeviceResponseDTO>(okResult.Value);

        Assert.Equal(device.Name, deviceResponse.Name);
        Assert.Equal(device.Status, deviceResponse.Status);
        Assert.Equal(device.Type, deviceResponse.Type);
    }

    [Fact]
    public async Task GetDevice_NonExistingId_ReturnsNotFound()
    {
        var controller = CreateController(out _);

        var actionResult = await controller.GetDevice(999);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var errorResponse = Assert.IsType<ErrorResponse>(notFoundResult.Value);
        Assert.Contains("not found", errorResponse.Message);
    }

    [Fact]
    public async Task PutDevice_ExistingId_UpdatesDevice()
    {
        var controller = CreateController(out var context);
        var device = new Device { Name = "Old Device", Status = DeviceStatus.Inactive, Type = "Sensor" };
        context.Devices.Add(device);
        await context.SaveChangesAsync();

        var updateDto = new DeviceUpdateDTO { Name = "Updated Device", Status = DeviceStatus.Active, Type = "Actuator" };

        var actionResult = await controller.PutDevice(device.Id, updateDto);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var updatedDevice = Assert.IsType<DeviceResponseDTO>(okResult.Value);

        Assert.Equal(updateDto.Name, updatedDevice.Name);
        Assert.Equal(updateDto.Status, updatedDevice.Status);
        Assert.Equal(updateDto.Type, updatedDevice.Type);
    }

    [Fact]
    public async Task PutDevice_InvalidModel_ReturnsBadRequest()
    {
        var controller = CreateController(out _);
        controller.ModelState.AddModelError("Name", "The Name field is required.");
        var updateDto = new DeviceUpdateDTO { Name = "", Status = DeviceStatus.Active, Type = "Actuator" };

        var actionResult = await controller.PutDevice(1, updateDto);

        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
        Assert.True(modelState.ContainsKey("Name"));
    }

    [Fact]
    public async Task PutDevice_NonExistingId_ReturnsNotFound()
    {
        var controller = CreateController(out _);
        var updateDto = new DeviceUpdateDTO { Name = "Updated Device", Status = DeviceStatus.Active, Type = "Actuator" };

        var actionResult = await controller.PutDevice(999, updateDto);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var errorResponse = Assert.IsType<ErrorResponse>(notFoundResult.Value);
        Assert.Contains("not found", errorResponse.Message);
    }

    [Fact]
    public async Task DeleteDevice_ExistingId_ReturnsSuccess()
    {
        var controller = CreateController(out var context);
        var device = new Device { Name = "Test Device", Status = DeviceStatus.Active, Type = "Sensor" };
        context.Devices.Add(device);
        await context.SaveChangesAsync();

        var actionResult = await controller.DeleteDevice(device.Id);

        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var successResponse = Assert.IsType<SuccessResponse>(okResult.Value);

        Assert.Contains($"Device with ID {device.Id} was deleted successfully", successResponse.Message);
    }

    [Fact]
    public async Task DeleteDevice_NonExistingId_ReturnsNotFound()
    {
        var controller = CreateController(out _);

        var actionResult = await controller.DeleteDevice(999);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        var errorResponse = Assert.IsType<ErrorResponse>(notFoundResult.Value);
        Assert.Contains("not found", errorResponse.Message);
    }
}
