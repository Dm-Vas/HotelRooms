using AutoMapper;
using HotelRoomsApi.Models;
using HotelRoomsApi.Models.Dto;
using HotelRoomsApi.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HotelRoomsApi.Controllers
{
    [Route("api/roomNumberApi")]
    [ApiController]
    public class RoomNumberApiController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IRoomNumberRepository _dbRoomNumber;
        private readonly IRoomRepository _dbRoom;
        private readonly IMapper _mapper;

        public RoomNumberApiController(IRoomNumberRepository dbRoomNumber, IMapper mapper, IRoomRepository dbRoom)
        {
            _dbRoomNumber = dbRoomNumber;
            _dbRoom = dbRoom;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetRoomNumbers()
        {
            try 
            { 
                IEnumerable<RoomNumber> roomNumberList = await _dbRoomNumber.GetAllAsync(includeProperties: "Room");

                _response.Result = _mapper.Map<List<RoomNumberDTO>>(roomNumberList);
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

        [HttpGet("{id:int}", Name = "GetRoomNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetRoomNumber(int id)
        {
            try 
            { 
                var roomNumber = await _dbRoomNumber.GetAsync(item => item.RoomNo == id);

                if (roomNumber == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;

                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<RoomNumberDTO>(roomNumber);
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
        public async Task<ActionResult<APIResponse>> CreateRoomNumber([FromBody] RoomNumberCreateDTO createDTO)
        {
            try 
            {
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
                if (await _dbRoomNumber.GetAsync(item => item.RoomNo == createDTO.RoomNo) != null)
                {
                    ModelState.AddModelError("RoomNumberAlreadyExists", "Room number already exists.");

                    return BadRequest(ModelState);
                }
                if (await _dbRoom.GetAsync(item => item.Id == createDTO.RoomID) == null)
                {
                    ModelState.AddModelError("RoomIdIsInvalid", "Room ID is invalid.");

                    return BadRequest(ModelState);

                }

                RoomNumber roomNumber = _mapper.Map<RoomNumber>(createDTO);

                await _dbRoomNumber.CreateAsync(roomNumber);

                _response.Result = _mapper.Map<RoomNumberDTO>(roomNumber);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetRoomNumber", new { id = roomNumber.RoomNo }, _response);

            }
            catch (Exception exception)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { exception.ToString() };

                return _response;
            }
        }

        [HttpPut("{id:int}", Name = "UpdateRoomNumber")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> UpdateRoomNumber(int id, [FromBody] RoomNumberUpdateDTO updateDTO)
        {
            try
            { 
                if (updateDTO == null || id != updateDTO.RoomNo) 
                {
                    return BadRequest();
                }
                if (await _dbRoom.GetAsync(item => item.Id == updateDTO.RoomID) == null)
                {
                    ModelState.AddModelError("RoomIdIsInvalid", "Room ID is invalid.");

                    return BadRequest(ModelState);

                }


                RoomNumber model = _mapper.Map<RoomNumber>(updateDTO);

                await _dbRoomNumber.UpdateAsync(model);

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

        [HttpDelete("{id:int}", Name = "DeleteRoomNumber")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteRoomNumber(int id)
        {
            try
            {
                var roomNumber = await _dbRoomNumber.GetAsync(item => item.RoomNo == id);

                if (roomNumber == null)
                {
                    return NotFound();
                }

                await _dbRoomNumber.RemoveAsync(roomNumber);

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
