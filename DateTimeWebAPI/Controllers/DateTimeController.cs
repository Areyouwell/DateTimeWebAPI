using DTWebAPI.Models;
using DTWebAPI.Services;
using DTWebAPI.Services.AddClass;
using Microsoft.AspNetCore.Mvc;

namespace DateTimeWebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/DateTime")]
    public class DateTimeController : ControllerBase
    {
        private readonly IDateInfRepository _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DateTimeController(IDateInfRepository db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Accepts a .csv file. This file is parsed, the values are written to the database in the Values table.
        /// Values are calculated and written to a table Results
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /file
        /// </remarks>
        /// <param name="file">IFormFile? object</param>
        /// <returns>NoContent</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid file or data</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostFileAndParse(IFormFile? file)
        {
            if (file == null || !(file.Length > 0) || !file.ContentType.ToLower().Equals("text/csv"))
                return BadRequest("Invalid file");  

            if(_db.GetFileAndParse(file, _webHostEnvironment.WebRootPath) == 0)
                return Ok("Data add to DB");
            return BadRequest("Invalid data");
        }

        /// <summary>
        /// Get results in JSON format
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /filters
        /// </remarks>
        /// <param name="filters">Filters object</param>
        /// <returns>Returns JSON of ResultSimply</returns>
        /// <response code="200">Success</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ResultSimply>> PostFromResults(Filters filters)
        {
            return _db.GetResults(filters);
        }

        /// <summary>
        /// Get values from the Values table by filename
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /fileName
        /// </remarks>
        /// <param name="fileName">String fileName</param>
        /// <returns>Returns List of ValueSimply</returns>
        /// <response code="200">Success</response>
        /// <response code="404">File not found</response>
        [HttpGet("{fileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ValueSimply>> GetFromValues(string fileName)
        {
            FileInf fileInf = _db.GetFiles(fileName);

            if (fileInf == null) return NotFound("File not found");

            return _db.GetValues(fileInf.Id);
        }
    }
}
