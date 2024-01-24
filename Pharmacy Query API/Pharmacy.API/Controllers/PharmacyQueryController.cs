using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Application.Pharmacy.Dtos;
using Pharmacy.Application.Pharmacy.Handlers;
using Pharmacy.Domain.Interfaces;

namespace Pharmacy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyQueryController : ControllerBase
    {
        private readonly IGetListPharmacyHandler _pharmacyHandler;
        private readonly IBroadcastPharmaciesHandler _broadcastPharmaciesHandler;

        public PharmacyQueryController(IGetListPharmacyHandler pharmacyHandler, IBroadcastPharmaciesHandler broadcastPharmaciesHandler)
        {
            _pharmacyHandler = pharmacyHandler;
            _broadcastPharmaciesHandler = broadcastPharmaciesHandler;
        }

        [HttpGet("/api/get-pharmacies")]
        public async Task<List<PharmacyDto>> GetListPharmacy()
        {
            var listPhar = await _pharmacyHandler.GetListPharmacyAsync();
            return listPhar;
        }

        [HttpPost("/api/broadcast-pharmacies")]
        public async Task<IActionResult> BroadcastPharmacies()
        {
            await _broadcastPharmaciesHandler.BroadcastPharmacies();
            return Ok("Sent messages.");
        }
    }
}
