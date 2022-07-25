namespace TrainingProgramService.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/exercises")]
public class ExercisesController : ControllerBase
{
    public ExercisesController()
    {
        
    }

    [HttpPost]
    public async Task<IActionResult> TestInboundConnection()
    {
        Console.WriteLine("--> Inbound POST # TP Service");
        return Ok("Inbound test from Exercises controller");
    }
}
