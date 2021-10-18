using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HotChanApi.Controller;

public class FileController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadFile(List<IFormFile> files)
    {
        foreach(IFormFile file in files)
        {
            if (file == null || file.Length == 0)
                throw new Exception("file not selected");

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot",
                        file.FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
        return RedirectToAction("Files");
    }

    public async Task<IActionResult> Download(string filename)
    {
        if (filename == null)
            return Content("filename not present");

        var path = Path.Combine(
                       Directory.GetCurrentDirectory(),
                       "wwwroot", filename);

        var memory = new MemoryStream();
        using (var stream = new FileStream(path, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;

        return File(memory, Path.GetExtension(path), Path.GetFileName(path));
    }

}

