using Medication.Application.Medication.Dtos;
using Medication.Application.Medication.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpsertMedicationController : ControllerBase
    {
        private readonly IUpsertMedicationHandler _upsertMedicationHandler;

        public UpsertMedicationController(IUpsertMedicationHandler upsertMedicationHandler)
        {
            _upsertMedicationHandler = upsertMedicationHandler;
        }

        [HttpPost("/api/upsert-medication")]
        public async Task<IActionResult> UpsertMedication(UpsertMedicationDto dto)
        {
            await _upsertMedicationHandler.UpsertMedicationAsync(dto);
            return Ok("Successful!");
        }
    }
}
