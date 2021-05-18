using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsMeeting.Server.Controllers;
using SportsMeeting.Server.Services;
using SportsMeeting.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class MeetingTests
    {
        [Fact]
        public async Task MeetingControllerGetMeetingActionResult_IdDoesntExist_ReturnsNotFoundObjectResult()
        {
            int id = 0;
            var service = new Mock<IMeetingService>();
            service.Setup(x => x.getMeeting(id)).ReturnsAsync(GetTestMeeting(id));
            var controller = new MeetingController(service.Object);
           

            var result = await controller.getMeeting(id);
            var action = Assert.IsType<ActionResult<MeetingDto>>(result);
            Assert.IsType<NotFoundObjectResult>(action.Result);
        }

        [Fact]
        public async Task MeetingControllerGetMeetingActionResult_IdIsCorrect_ReturnsOkObjectResult()
        {
            int id = 1;
            var service = new Mock<IMeetingService>();
            service.Setup(x => x.getMeeting(id)).ReturnsAsync(GetTestMeeting(id));
            var controller = new MeetingController(service.Object);


            var result = await controller.getMeeting(id);
            var action = Assert.IsType<ActionResult<MeetingDto>>(result);
            Assert.IsType<OkObjectResult>(action.Result);
        }

        [Fact]
        public async Task MeetingControllerGetMeetingsActionResult_QueryIsCorrect_ReturnsOkObjectResult()
        {
            MeetingQuery query = new MeetingQuery
            {
                Category = "test1",
                Page = 1,
                Quantity = 9
            };

            int id = 1;
            var service = new Mock<IMeetingService>();
            service.Setup(x=> x.getAllMeetings(query)).ReturnsAsync(GetTestMeetingsAsync());
            var controller = new MeetingController(service.Object);


            var result = await controller.getMeetings(query);
            var action = Assert.IsType<ActionResult<List<MeetingDto>>>(result);
            Assert.IsType<OkObjectResult>(action.Result);
        }

        private List<MeetingDto> GetTestMeetings()
        {
            var meetings = new List<MeetingDto>();
            meetings.Add(new MeetingDto
            {
                Id = 1,
                Title = "test1",
                Description = "test1",
                PersonalLimit = 4,
                CategoryName = "test1",
                Date = DateTime.Now,
                Place = "test1",
                UserEmail = "test@localhost",
                UserName = "test",
                Category = GetTestCategory()
            });
            meetings.Add(new MeetingDto
            {
                Id = 2,
                Title = "test2",
                Description = "test2",
                PersonalLimit = 4,
                CategoryName = "test1",
                Date = DateTime.Now,
                Place = "test2",
                UserEmail = "test@localhost",
                UserName = "test",
                Category = GetTestCategory()
            });

            return meetings;
        }

        private PageResult<MeetingDto> GetTestMeetingsAsync()
        {
            var meetings = new List<MeetingDto>();
            meetings.Add(new MeetingDto
            {
                Id = 1,
                Title = "test1",
                Description = "test1",
                PersonalLimit = 4,
                CategoryName = "test1",
                Date = DateTime.Now,
                Place = "test1",
                UserEmail = "test@localhost",
                UserName = "test",
                Category = GetTestCategory()
            });
            meetings.Add(new MeetingDto
            {
                Id = 2,
                Title = "test2",
                Description = "test2",
                PersonalLimit = 4,
                CategoryName = "test1",
                Date = DateTime.Now,
                Place = "test2",
                UserEmail = "test@localhost",
                UserName = "test",
                Category = GetTestCategory()
            });
            
            var result = new PageResult<MeetingDto>(meetings, meetings.Count(), 9, 1);

            return result;
        }

        private MeetingDto GetTestMeeting(int id)
        {
            var meetings = GetTestMeetings();
            var meeting = meetings.FirstOrDefault(x => x.Id == id);
            return meeting;
        }


        private CategoryDto GetTestCategory()
        {
            var category = new CategoryDto
            {
                Id = 1,
                Name = "test",
                Description = "test1"

            };

            return category;
        }
    }
}
