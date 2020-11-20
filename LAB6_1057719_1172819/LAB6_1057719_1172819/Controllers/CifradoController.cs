using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using huffman_prueba;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.AspNetCore.Routing;
using System.Collections;
using System.Data.SqlTypes;
using System.IO.Compression;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace LAB6_1057719_1172819.Controllers
{
    [ApiController]
    public class CifradoController : ControllerBase
    {

        [Route("api/rsa/keys/{number1}/{number2}")]
        [HttpGet]
        public IActionResult Mostrar(int number1, int number2)
        {
            RSA prueba = new RSA();
            keypair valores = prueba.Generatekeys(number1, number2);
           
            try
            {
                using var fileWriteprivado = new FileStream("private.key", FileMode.OpenOrCreate);
                StreamWriter writerprivado = new StreamWriter(fileWriteprivado);
                writerprivado.WriteLine(valores.llaveprivada.n);
                writerprivado.WriteLine(valores.llaveprivada.ed);
                fileWriteprivado.Close();
                using var fileWritepublico = new FileStream("public.key", FileMode.OpenOrCreate);
                StreamWriter writerpublico = new StreamWriter(fileWritepublico);
                writerpublico.WriteLine(valores.llavepublica.n);
                writerpublico.WriteLine(valores.llavepublica.ed);
                fileWritepublico.Close();

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
