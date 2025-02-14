using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BRD_NF_4_7_2_TRANSMISSAO.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase arquivo, string descricao)
        {
            if (arquivo == null || string.IsNullOrEmpty(descricao))
            {
                ViewBag.Mensagem = "Por favor, selecione um arquivo e forneça o tipo de arquivo.";
                return View();
            }

            using (var client = new HttpClient())
            {
                string baseUrl = ConfigurationManager.AppSettings["BaseUrlApi"];
                string endPoint = ConfigurationManager.AppSettings["EndPoint"];

                client.BaseAddress = new Uri(baseUrl); // Troque pela URL base da sua API

                using (var content = new MultipartFormDataContent())
                {
                    var fileContent = new StreamContent(arquivo.InputStream);
                    fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                    {
                        Name = "\"arquivo\"",
                        FileName = "\"" + arquivo.FileName + "\""
                    };
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(arquivo.ContentType);
                    content.Add(fileContent);
                    content.Add(new StringContent(descricao), "\"tipoArquivo\"");

                    var response = await client.PostAsync(endPoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = "Arquivo processado corretamente!";
                        ViewBag.Mensagem = responseData;
                    }
                    else
                    {
                        ViewBag.Mensagem = "Falha ao enviar o arquivo. " + response.ReasonPhrase;
                    }
                }
            }

            return View();
        }
    }
}