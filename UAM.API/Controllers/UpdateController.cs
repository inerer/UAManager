using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using UAM.API.Models;
using Version = System.Version;

namespace UAM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly UaVersionsContext _context;

        public UpdateController(UaVersionsContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllVersions()
        {
            return Ok(await _context.Versions.ToListAsync());
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUpdate(string build)
        {
            var ver = await _context.Versions.FirstOrDefaultAsync(v => v.Build == build);

            if (ver == null)
                return NotFound();

            var path = $"Files/{ver.Path}";

            if (!System.IO.File.Exists(path))
                return NotFound();

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            return File(bytes, "application/zip");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUpdateById(Guid id)
        {
            var ver = await _context.Versions.FirstOrDefaultAsync(v => v.Id == id);

            if (ver == null)
                return NotFound();

            var path = $"Files/{ver.Path}";

            if (!System.IO.File.Exists(path))
                return NotFound();

            var bytes = await System.IO.File.ReadAllBytesAsync(path);

            return File(bytes, "application/zip");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLastUpdate()
        {
            var version = await _context.Versions.OrderByDescending(v => v.Timestamp).FirstOrDefaultAsync();

            return Ok(version);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddUpdate(IFormFile uploadedFile)
        {
            if (uploadedFile.ContentType != "application/x-zip-compressed")
                throw new FileFormatException("available only zip");

            var path = $"Files/{uploadedFile.FileName}";

            // save file
            await using (StreamWriter streamWriter = new StreamWriter(path))
            {
                await uploadedFile.CopyToAsync(streamWriter.BaseStream);
            }

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateVersion(Models.Version version)
        {
            try
            {
                await _context.Versions.AddAsync(version);

                await _context.SaveChangesAsync();

                return Ok(version);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return BadRequest(e.Message);
            }
        }
    }
}