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
using System.Security.Cryptography.X509Certificates;

namespace Lab1.Controllers
{
    [Route("api")]
    [ApiController]
    public class CifradoController : ControllerBase
    {
        [HttpGet]
        public IActionResult Mostrar()
        {
            return new JsonResult(new { BIENVENIDA = "LABORATORIO 4" }); 
        }


        //Post /api/cipher/{method} CIFRAR
        [HttpPost("cipher/{method}")]
        public IActionResult Cifrar([FromRoute] string method, [FromForm]IFormFile file, [FromForm] Datos key)
        {
            using var fileRead = file.OpenReadStream();
            try
            {
                if (method == "Cesar" && key.Word != null)
                {
                   
                    var BfileRead = new BufferedStream(fileRead);
                    var reader = new StreamReader(BfileRead);

                    string prueba = reader.ReadToEnd();

                    reader.Close();
                    BfileRead.Close();
                    fileRead.Close();

                    //WRITE
                    var fileWrite = new FileStream(Path.GetFileNameWithoutExtension(file.FileName) + ".csr", FileMode.OpenOrCreate);
                    var BfileWrite = new BufferedStream(fileWrite);
                    var writer = new StreamWriter(BfileWrite);

                    Cifrado hola = new Cifrado();

                    string encoded = hola.encoder(key.Word);

                    string salida = "";
                    salida = hola.CifrarCesar(prueba, encoded);

                    foreach (char item in salida)
                    {
                        writer.Write(item);
                    }

                    writer.Close();
                    BfileWrite.Close();
                    fileWrite.Close();

                    var files = System.IO.File.OpenRead(Path.GetFileNameWithoutExtension(file.FileName) + ".csr");
                    return new FileStreamResult(files, "application/csr")
                    {
                        FileDownloadName = Path.GetFileNameWithoutExtension(file.FileName) + ".csr"
                    };
                }
                else if (method == "ZigZag" && key.Levels != null)
                {
                    var BfileRead = new BufferedStream(fileRead);
                    var reader = new StreamReader(BfileRead);

                    string prueba = reader.ReadToEnd();

                    reader.Close();
                    BfileRead.Close();
                    fileRead.Close();

                    //WRITE
                    var fileWrite = new FileStream(Path.GetFileNameWithoutExtension(file.FileName) + ".zz", FileMode.OpenOrCreate);
                    var BfileWrite = new BufferedStream(fileWrite);
                    var writer = new StreamWriter(BfileWrite);

                    Cifrado hola = new Cifrado();

                    string salida = "";
                    salida = hola.Cifrarzigzag(prueba, key.Levels);

                    foreach (char item in salida)
                    {
                        writer.Write(item);
                    }

                    writer.Close();
                    BfileWrite.Close();
                    fileWrite.Close();

                    var files = System.IO.File.OpenRead(Path.GetFileNameWithoutExtension(file.FileName) + ".zz");
                    return new FileStreamResult(files, "application/zz")
                    {
                        FileDownloadName = Path.GetFileNameWithoutExtension(file.FileName) + ".zz"
                    };
                }
                else if (method == "Ruta" && key.Rows != null && key.Columns != null)
                {
                    var BfileRead = new BufferedStream(fileRead);
                    var reader = new StreamReader(BfileRead);

                    string prueba = reader.ReadToEnd();

                    reader.Close();
                    BfileRead.Close();
                    fileRead.Close();

                    //WRITE
                    var fileWrite = new FileStream(Path.GetFileNameWithoutExtension(file.FileName) + ".rt", FileMode.OpenOrCreate);
                    var BfileWrite = new BufferedStream(fileWrite);
                    var writer = new StreamWriter(BfileWrite);

                    Cifrado hola = new Cifrado();

                    string salida = "";
                    salida = hola.CifrarRuta(prueba, key.Rows, key.Columns);

                    foreach (char item in salida)
                    {
                        writer.Write(item);
                    }

                    writer.Close();
                    BfileWrite.Close();
                    fileWrite.Close();

                    var files = System.IO.File.OpenRead(Path.GetFileNameWithoutExtension(file.FileName) + ".rt");
                    return new FileStreamResult(files, "application/rt")
                    {
                        FileDownloadName = Path.GetFileNameWithoutExtension(file.FileName) + ".rt"
                    };
                }
                else { return BadRequest("Error no ingreso método o llave"); }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        // POST: api/descipher DESCOMPRIR
        [HttpPost("descipher")]
        public IActionResult Descifrar([FromRoute] string method, [FromForm]IFormFile file, [FromForm] Datos key)
        {
            string nombre = (file.FileName);
            using var fileRead = file.OpenReadStream();
            try
            {
                if (nombre.Contains(".csr") && key.Word != null)
                {

                    var BfileRead = new BufferedStream(fileRead);
                    var reader = new StreamReader(BfileRead);

                    string prueba = reader.ReadToEnd();

                    reader.Close();
                    BfileRead.Close();
                    fileRead.Close();

                    //WRITE
                    var fileWrite = new FileStream(Path.GetFileNameWithoutExtension(file.FileName) + ".txt", FileMode.OpenOrCreate);
                    var BfileWrite = new BufferedStream(fileWrite);
                    var writer = new StreamWriter(BfileWrite);

                    Cifrado hola = new Cifrado();

                    string encoded = hola.encoder(key.Word);

                    string salida = "";
                    salida = hola.DecifrarCesar(prueba, encoded);

                    foreach (char item in salida)
                    {
                        writer.Write(item);
                    }

                    writer.Close();
                    BfileWrite.Close();
                    fileWrite.Close();

                    var files = System.IO.File.OpenRead(Path.GetFileNameWithoutExtension(file.FileName) + ".txt");
                    return new FileStreamResult(files, "application/txt")
                    {
                        FileDownloadName = Path.GetFileNameWithoutExtension(file.FileName) + ".txt"
                    };
                }
                else if (nombre.Contains(".zz") && key.Levels != null)
                {
                    var BfileRead = new BufferedStream(fileRead);
                    var reader = new StreamReader(BfileRead);

                    string prueba = reader.ReadToEnd();

                    reader.Close();
                    BfileRead.Close();
                    fileRead.Close();

                    //WRITE
                    var fileWrite = new FileStream(Path.GetFileNameWithoutExtension(file.FileName) + ".txt", FileMode.OpenOrCreate);
                    var BfileWrite = new BufferedStream(fileWrite);
                    var writer = new StreamWriter(BfileWrite);

                    Cifrado hola = new Cifrado();

                    string salida = "";
                    salida = hola.Descifrarzigzag(prueba, key.Levels);

                    foreach (char item in salida)
                    {
                        writer.Write(item);
                    }

                    writer.Close();
                    BfileWrite.Close();
                    fileWrite.Close();

                    var files = System.IO.File.OpenRead(Path.GetFileNameWithoutExtension(file.FileName) + ".txt");
                    return new FileStreamResult(files, "application/txt")
                    {
                        FileDownloadName = Path.GetFileNameWithoutExtension(file.FileName) + ".txt"
                    };
                }
                else if (nombre.Contains(".rt") && key.Rows != null && key.Columns != null)
                {
                    var BfileRead = new BufferedStream(fileRead);
                    var reader = new StreamReader(BfileRead);

                    string prueba = reader.ReadToEnd();

                    reader.Close();
                    BfileRead.Close();
                    fileRead.Close();

                    //WRITE
                    var fileWrite = new FileStream(Path.GetFileNameWithoutExtension(file.FileName) + ".txt", FileMode.OpenOrCreate);
                    var BfileWrite = new BufferedStream(fileWrite);
                    var writer = new StreamWriter(BfileWrite);

                    Cifrado hola = new Cifrado();

                    string salida = "";
                    salida = hola.DecifrarRuta(prueba, key.Rows, key.Columns);

                    foreach (char item in salida)
                    {
                        writer.Write(item);
                    }

                    writer.Close();
                    BfileWrite.Close();
                    fileWrite.Close();

                    var files = System.IO.File.OpenRead(Path.GetFileNameWithoutExtension(file.FileName) + ".txt");
                    return new FileStreamResult(files, "application/rt")
                    {
                        FileDownloadName = Path.GetFileNameWithoutExtension(file.FileName) + ".txt"
                    };
                }
                else { return BadRequest("Error no ingreso método o llave"); }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
