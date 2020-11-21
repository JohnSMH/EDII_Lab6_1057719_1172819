using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RSA_prueba;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.AspNetCore.Routing;
using System.Collections;
using System.Data.SqlTypes;
using System.IO.Compression;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Numerics;

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
            using var fileWriteprivado = new FileStream("private.key", FileMode.OpenOrCreate);
            using var fileWritepublico = new FileStream("public.key", FileMode.OpenOrCreate);

            try
            {
                StreamWriter writerprivado = new StreamWriter(fileWriteprivado);
                writerprivado.WriteLine(valores.llaveprivada.n);
                writerprivado.WriteLine(valores.llaveprivada.ed);
                writerprivado.Close();
                fileWriteprivado.Close();

                StreamWriter writerpublico = new StreamWriter(fileWritepublico);
                writerpublico.WriteLine(valores.llavepublica.n);
                writerpublico.WriteLine(valores.llavepublica.ed);
                writerpublico.Close();


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
                RSA prueba = new RSA();
                using var fileWrite = new FileStream(nombre + ".txt", FileMode.OpenOrCreate);
                var writer = new BinaryWriter(fileWrite);
                StreamReader leer = new StreamReader(fileReadllave);
                int n = Convert.ToInt32(leer.ReadLine());
                int eod = Convert.ToInt32(leer.ReadLine());

                if (filellave.FileName.Contains("public.key"))
                {
                    using var reader = new BinaryReader(fileRead);
                    var buffer = new byte[2000];
                    List<byte[]> bytelist = new List<byte[]>();
                    int size = 0;
                    while (fileRead.Position < fileRead.Length)
                    {
                        buffer = reader.ReadBytes(2000);
                        foreach (var value in buffer)
                        {
                            //writer.Write(prueba.Manualbytetoint(prueba.CipherAndDecipher(value, eod, n)));
                            byte[] valor = prueba.CipherAndDecipher((int)value, eod, n);
                            bytelist.Add(valor);
                            if (valor.Length > size)
                                size = valor.Length;
                        }
                    }

                    writer.Write(size);
                    foreach (var number in bytelist) {
                        int correccion = size - number.Length;
                        writer.Write(number);
                        while (correccion!=0)
                        {
                            writer.Write(BigInteger.Zero.ToByteArray());
                            correccion--;
                        }
                        
                    }
                }

                if (filellave.FileName.Contains("private.key"))
                {
                    using var reader = new BinaryReader(fileRead);
                    int size = BitConverter.ToInt32(reader.ReadBytes(4));
                    var buffer = new byte[1000*size];
                    
                    while (fileRead.Position < fileRead.Length)
                    {
                        byte[] number = reader.ReadBytes(size);
                        int deciphered = prueba.Manualbytetoint(number);
                        byte[] final = prueba.CipherAndDecipher(deciphered,eod,n);
                        writer.Write(final);
                    }
                }
                writer.Close();
                leer.Close();
                fileWrite.Close();
                var files = System.IO.File.OpenRead(nombre + ".txt");
                return new FileStreamResult(files, "application/txt")
                {
                    FileDownloadName = nombre + ".txt"
                };

            }
        
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
