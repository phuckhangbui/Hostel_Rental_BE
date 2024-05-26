//namespace HostelManagementWebAPI.Controllers
//{
//    public class OwnerHostelController : BaseApiController
//    {
//        private readonly IOwnerHostelService _ownerHostelService;

//        public OwnerHostelController(IOwnerHostelService ownerHostelService)
//        {
//            _ownerHostelService = ownerHostelService;
//        }



//        [HttpGet("ownerhostels")]
//        public async Task<ActionResult> GetOwnerHostels()
//        {
//            try
//            {
//                var ownerHostels = await _ownerHostelService.GetOwnerHostels();
//                return Ok(ownerHostels);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
//            }
//        }

//        [HttpPut("ownerhostels")]
//        public async Task<ActionResult> Update([FromBody] UpdateOwnerHostelRequestDto updateOwnerHostelRequestDto)
//        {
//            try
//            {
//                await _ownerHostelService.UpdateOwnerHostel(updateOwnerHostelRequestDto);
//                return Ok();
//            }
//            catch (ServiceException ex)
//            {
//                return BadRequest(new ApiResponseStatus(400, ex.Message));
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new ApiResponseStatus(500, ex.Message));
//            }
//        }
//    }
//}
