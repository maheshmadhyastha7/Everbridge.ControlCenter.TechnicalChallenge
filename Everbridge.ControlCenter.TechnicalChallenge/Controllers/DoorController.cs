using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase;
using Everbridge.ControlCenter.TechnicalChallenge.Models;
using Microsoft.AspNetCore.SignalR;
using Everbridge.ControlCenter.TechnicalChallenge.Hubs;

namespace Everbridge.ControlCenter.TechnicalChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DoorController : ControllerBase
    {
        private readonly ILogger<DoorController> _logger;
        private readonly DoorRepositoryService _doorRepositoryService;
        private readonly IHubContext<DoorManagementHub> _doorHub;

        public DoorController(ILogger<DoorController> logger, DoorRepositoryDatabaseContext databaseContext, IHubContext<DoorManagementHub> doorHub)
        {
            _logger = logger;
            _doorRepositoryService = new DoorRepositoryService(databaseContext);
            _doorHub = doorHub;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get() =>
             await _doorRepositoryService.GetDoorsIds().ConfigureAwait(false);


        [HttpGet]
        [Route("{doorId}")]
        public async Task<DoorModel> GetDoor([FromRoute][Required] string doorId)
        {
            var doorRecord = await _doorRepositoryService.GetDoor(doorId).ConfigureAwait(false);

            return (doorRecord == null) ? null
                : new DoorModel
                {
                    Id = doorRecord.Id,
                    Label = doorRecord.Label,
                    IsOpen = doorRecord.IsOpen,
                    IsLocked = doorRecord.IsLocked
                };
        }


        [HttpPost]
        public async Task<DoorModel> Add([FromBody][Required] DoorModel doorModel)
        {
            var doorRecordDto = new DoorRecordDto
            {
                Label = doorModel.Label,
                IsLocked = doorModel.IsLocked,
                IsOpen = doorModel.IsOpen
            };

            var doorRecord = await _doorRepositoryService.AddDoor(doorRecordDto);

            if (doorRecord == null)
            {
                return null;
            }

            await _doorHub.Clients.All.SendAsync("Add", doorRecord.Id);

            return new DoorModel
            {
                Id = doorRecord.Id,
                Label = doorRecord.Label,
                IsOpen = doorRecord.IsOpen,
                IsLocked = doorRecord.IsLocked
            };
        }

        [HttpPut]
        public async Task<DoorModel> Update([FromBody][Required] DoorModel doorModel)
        {
            var doorRecordDto = new DoorRecordDto
            {
                Id = doorModel.Id,
                Label = doorModel.Label,
                IsLocked = doorModel.IsLocked,
                IsOpen = doorModel.IsOpen
            };

            var doorRecord = await _doorRepositoryService.UpdateDoor(doorRecordDto);

            if (doorRecord == null)
            {
                return null;
            }

            await _doorHub.Clients.All.SendAsync("Update", doorRecord);

            return new DoorModel
            {
                Id = doorRecord.Id,
                Label = doorRecord.Label,
                IsOpen = doorRecord.IsOpen,
                IsLocked = doorRecord.IsLocked
            };
        }

        [HttpDelete]
        [Route("{doorId}")]
        public async Task<DoorModel> Remove([FromRoute][Required] string doorId)
        {
            var doorRecord = await _doorRepositoryService.RemoveDoor(doorId);

            if (doorRecord == null)
            {
                return null;
            }

            await _doorHub.Clients.All.SendAsync("Remove", doorRecord.Id);

            return new DoorModel
            {
                Id = doorRecord.Id,
                Label = doorRecord.Label,
                IsOpen = doorRecord.IsOpen,
                IsLocked = doorRecord.IsLocked
            };
        }
    }
}
