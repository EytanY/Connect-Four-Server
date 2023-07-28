using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Connect_Four_Server.Models;
using Connect_Four_Server.Models.Serialization;
namespace Connect_Four_Server.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectFourGameBoardController : ControllerBase
    {
        // POST: api/ConnectFourGameBoard
        [HttpPost]
        public ActionResult PostConnectFourGameBoardForNextMove([FromBody]  SerialConnectFourGameBoard serialConnectFourGameBoard)
        {
            if (serialConnectFourGameBoard != null)
            {
                ConnectFourGameBoard connectFourGameBoard = serialConnectFourGameBoard.GetConnectFourGameBoard();
                Move move = connectFourGameBoard.GetNextMove();
                return Ok(move);

            }
            return BadRequest();
            
        }

        [HttpGet]
        public ActionResult GetBoard()
        {
            return Ok(); // shows that api works

        }
    }
}
