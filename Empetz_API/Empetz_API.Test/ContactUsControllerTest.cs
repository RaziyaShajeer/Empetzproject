using AutoMapper;
using Domain.Service.Category.Interfaces;
using Domain.Service.ContactUs.DTOs;
using Domain.Service.ContactUs.Interfaces;
using Domain.Service.User.Interfaces;
using Empetz_API.ContactUs.RequestObject;
using Empetz_API.ContactUs;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Empetz_API.Test
{
    public class ContactUsControllerTest
    {
   
        public IContactUsService _contactUsService { get; set; }
        public ContactUsControllerTest(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService ?? throw new ArgumentNullException(nameof(contactUsService));
        }
        [Fact]
        public async Task ContactUsController_AddContactUs_ReturnsOkWithContactUsId()
        {
            // Arrange
            var contactUsService = A.Fake<IContactUsService>(); // Using FakeItEasy for a fake service
            var controller = new ContactUsController(contactUsService);

            var contactUsRequest = new ContactUsRequest
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Message = "Hello, this is a test message",
                UserId = Guid.NewGuid() // Provide a valid user id here
            };

            // Assume your service method returns a contactUsId
            A.CallTo(() => contactUsService.AddContactUsAsync(A<ContactUsDTO>.Ignored))
                .Returns(Task.FromResult(Guid.NewGuid())); // Simulate a contactUsId

            // Act
            var result = await controller.AddContactUs(contactUsRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(new { ContactUsId = Guid.Empty }); // Adjust the expected ContactUsId value

            A.CallTo(() => contactUsService.AddContactUsAsync(A<ContactUsDTO>.Ignored))
                .MustHaveHappenedOnceExactly(); // Ensure that the service method was called once
        }
    }
}
