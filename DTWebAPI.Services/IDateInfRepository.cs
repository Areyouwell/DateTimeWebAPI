using DTWebAPI.Models;
using DTWebAPI.Services.AddClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DTWebAPI.Services
{
    public interface IDateInfRepository
    {
        ActionResult<IEnumerable<ResultSimply>> GetResults(Filters filters);
        ActionResult<IEnumerable<ValueSimply>> GetValues(int fileId);
        FileInf GetFiles(string fileName);
        int GetFileAndParse(IFormFile? file, string path);
    }
}
