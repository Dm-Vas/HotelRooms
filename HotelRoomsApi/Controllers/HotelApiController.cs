using AutoMapper;
using HotelRoomsApi.Models;
using HotelRoomsApi.Models.Dto;
using HotelRoomsApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text.Json;

namespace HotelRoomsApi.Controllers
{
    [Route("api/hotelApi")]
    [ApiController]
    public class HotelApiController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IRoomRepository _dbRoom;
        private readonly IMapper _mapper;

        public HotelApiController(IRoomRepository dbRoom, IMapper mapper)
        {
            _dbRoom = dbRoom;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetRooms([FromQuery] int? capacity, 
            [FromQuery(Name ="search by name")] string? search, int pageSize = 0, int pageNumber = 1)
        {
            try 
            { 
                IEnumerable<Room> roomList;

                if (capacity > 0)
                {
                    roomList = await _dbRoom.GetAllAsync(room => room.Capacity == capacity, pageSize: pageSize, pageNumber: pageNumber);
                }
                else
                {
                    roomList = await _dbRoom.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }

                if (!string.IsNullOrEmpty(search))
                {
                    roomList = roomList.Where(item => item.Name.ToLower().Contains(search.ToLower()));
                }

                Pagination pagination = new()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));
                _response.Result = _mapper.Map<List<RoomDTO>>(roomList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            } 
            catch  (Exception exception)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { exception.ToString() };

                return _response;
            }
        }

        [HttpGet("{id:int}", Name = "GetRoom")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRoom(int id)
        {
            try 
            { 
                var room = await _dbRoom.GetAsync(item => item.Id == id);

                if (room == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;

                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<RoomDTO>(room);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { exception.ToString() };

                return _response;
            }
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse>> CreateRoom([FromBody] RoomCreateDTO createDTO)
        {
            try 
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                if (await _dbRoom.GetAsync(item => item.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("RoomAlreadyExists", "Room already exists.");

                    return BadRequest(ModelState);
                }

                Room room = _mapper.Map<Room>(createDTO);

                await _dbRoom.CreateAsync(room);

                _response.Result = _mapper.Map<RoomDTO>(room);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetRoom", new { id = room.Id }, _response);

            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { exception.ToString() };

                return _response;
            }
        }

        [HttpPut("{id:int}", Name = "UpdateRoom")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateRoom(int id, [FromBody] RoomUpdateDTO updateDTO)
        {
            try
            { 
                if (updateDTO == null || id != updateDTO.Id) 
                {
                    return BadRequest();
                }

                Room model = _mapper.Map<Room>(updateDTO);

                await _dbRoom.UpdateAsync(model);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { exception.ToString() };

                return _response;
            }
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialRoom")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdatePartialRoom(int id, JsonPatchDocument<RoomUpdateDTO> patchDTO)
        {
            try
            { 
                if (patchDTO == null)
                {
                    return BadRequest();
                }

                var room = await _dbRoom.GetAsync(item => item.Id == id, tracked:false);

                if (room == null)
                {
                    return NotFound();
                }

                RoomUpdateDTO roomDTO = _mapper.Map<RoomUpdateDTO>(room);

                patchDTO.ApplyTo(roomDTO, ModelState);

                Room model = _mapper.Map<Room>(roomDTO);

                await _dbRoom.UpdateAsync(model);

                if (!ModelState.IsValid) 
                {
                    return BadRequest(ModelState);
                }

                return NoContent();
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { exception.ToString() };

                return _response;
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteRoom")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteRoom(int id)
        {
            try
            {
                var room = await _dbRoom.GetAsync(item => item.Id == id);

                if (room == null)
                {
                    return NotFound();
                }

                await _dbRoom.RemoveAsync(room);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { exception.ToString() };

                return _response;
            }
        }
    }
}
