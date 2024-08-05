using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringProcessingController : ControllerBase
    {
        private readonly StringProcessService _stringProcessor;

        public StringProcessingController(StringProcessService stringProcessor)
        {
            _stringProcessor = stringProcessor;
        }

        [HttpGet("process")]
        public async Task<IActionResult> ProcessString([FromQuery] string input, [FromQuery] int algorithm)
        {
            var validationMessage = _stringProcessor.ValidateString(input);
            if (validationMessage != null)
            {
                return BadRequest(new { message = validationMessage });
            }

            var processedString = _stringProcessor.ProcessString(input);
            var charCounts = _stringProcessor.GetCharCounts(processedString);
            var longestVowelSubstring = _stringProcessor.GetLongestVowelSubstring(processedString);
            var sortAlgorithm = algorithm == 2 ? SortAlgorithmNames.TreeSort : SortAlgorithmNames.QuickSort;
            var sortedString = _stringProcessor.SortString(processedString, sortAlgorithm);
            var reducedString = await _stringProcessor.GetReducedString(sortedString);

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
}