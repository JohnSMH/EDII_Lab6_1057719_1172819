using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using huffman_prueba;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.AspNetCore.Routing;
using System.Collections;
using System.Data.SqlTypes;
using System.IO.Compression;

namespace Lab1.Controllers
{ 
    [ApiController]
    public class CifradoController : ControllerBase
    {
        [Route("api")]
        [HttpGet]
        public IActionResult Mostrar()
        {
            return new JsonResult(new { BIENVENIDA = "LABORATORIO 6" }); 
        }
        [Route("api/home")]
        [HttpGet]
        public IActionResult Mostrar()
        {
            return new JsonResult(new { BIENVENIDA = "hola 6" }); 
        }

        [Route("api/rsa/keys/{p}/{q}")]
        [HttpGet]
        public ActionResult Obtener([FromRoute] int p, [FromRoute] int q)
        {
            RSA prueba = new RSA();
            keypair valores = prueba.Generatekeys(p, q);
            try
            {
                var fileWriteprivado = new FileStream( "private.key", FileMode.OpenOrCreate);
                var writerprivado = new StreamWriter(fileWriteprivado);
                writerprivado.WriteLine(valores.llaveprivada.n);
                writerprivado.WriteLine(valores.llaveprivada.ed);
                var fileWritepublico = new FileStream("public.key", FileMode.OpenOrCreate);
                var writerpublico = new StreamWriter(fileWritepublico);
                writerpublico.WriteLine(valores.llavepublica.n);
                writerpublico.WriteLine(valores.llavepublica.ed);

                string[] files = { "private.key ", "public.key" };
                using (var zipArchive = ZipFile.Open("keys.zip", ZipArchiveMode.Update))
                {
                    foreach (var file in files)
                    {
                        var fileInfo = new FileInfo(file);
                        zipArchive.CreateEntryFromFile(fileInfo.FullName, fileInfo.Name);
                    }
                }
                var filess = System.IO.File.OpenRead("keys.zip");
                return new FileStreamResult(filess, "application/zip")
                {
                    FileDownloadName = "keys.zip"
                };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //Post  /api/rsa/{nombre}
        [Route("api/rsa/{nombre}")]
        [HttpPost]
        public IActionResult Cifrarllaves([FromRoute] string nombre, [FromForm]IFormFile file, [FromForm]IFormFile filellave)
        {
            using var fileRead = file.OpenReadStream();
            using var fileReadllave = filellave.OpenReadStream();
            try
            {
                //RSA prueba = new RSA();
                //keypair valores = prueba.Generatekeys(p, q);
                //prueba.Cipher(fileRead.ReadByte, 17);

                //writer.Close();
                //BfileWrite.Close();
                //fileWrite.Close();

                //var files = System.IO.File.OpenRead(Path.GetFileNameWithoutExtension(file.FileName) + ".rt");
                //return new FileStreamResult(files, "application/rt")
                //{
                //    FileDownloadName = Path.GetFileNameWithoutExtension(file.FileName) + ".rt"
                //};\
                return Ok();
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
