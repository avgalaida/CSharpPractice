using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StringProcessingController : ControllerBase
{
    [HttpGet("process")]
    public async Task<IActionResult> ProcessString([FromQuery] string input, [FromQuery] int algorithm)
    {
        var validationMessage = StringProcessService.ValidateString(input);
        if (validationMessage != null)
        {
            return BadRequest(new { message = validationMessage });
        }

        var processedString = StringProcessService.ProcessString(input);
        var charCounts = StringProcessService.GetCharCounts(processedString);
        var longestVowelSubstring = StringProcessService.GetLongestVowelSubstring(processedString);
        var sortAlgorithm = algorithm == 2 ? SortAlgorithmNames.TreeSort : SortAlgorithmNames.QuickSort;
        var sortedString = StringProcessService.SortString(processedString, sortAlgorithm);
        var reducedString = await StringProcessService.GetReducedString(sortedString);

        var result = new
        {
            processedString,
            charCounts,
            longestVowelSubstring,
            sortedString,
            reducedString
        };

        return Ok(result);
    }
}
