using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using FileManipulation.Models;

namespace FileManipulation.Controllers
{
    public class FileController : Controller
    {
            private readonly IWebHostEnvironment _manup;

            public FileController(IWebHostEnvironment manup)
            {
                _manup = manup;
            }



        public IActionResult Index()
        {
            ManupClass man = new ManupClass();
            var manup_file = Path.Combine(_manup.WebRootPath, "File");
            DirectoryInfo ma = new DirectoryInfo(manup_file);
            FileInfo[] fileinfo = ma.GetFiles();
            man.FileManup = fileinfo;
            return View(man);
        }



        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 524288000)]
        [RequestSizeLimit(524288000)]
        public async Task<IActionResult> Index(IFormFile fileman)
        {
            
            string ext = Path.GetExtension(fileman.FileName);
            if (ext == ".jpg" || ext == ".pdf" || ext == ".docx")
            {
                var filesave = Path.Combine(_manup.WebRootPath, "File", fileman.FileName);
                var stream = new FileStream(filesave, FileMode.Create);
                await fileman.CopyToAsync(stream);
                stream.Close();

            }
            TempData["Message"] = $"Загрузка файла прошла успешно. Размер файла: {fileman.Length} байт";
            return RedirectToAction("Index");
        }






    }
}