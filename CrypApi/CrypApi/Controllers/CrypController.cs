using CrypApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrypApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrypController : ControllerBase
    {
        private readonly CrypApiService _crypApiService;
        public CrypController(CrypApiService crypApiService)
        {
            _crypApiService = crypApiService;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllCryptoData()
        {
            var data = await _crypApiService.GetCrptoDataAsync();
            return Ok(data);
        }
    }
}
