using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AvantTest.Models;
using AvantTest.Services;
using System.Net;
using System.IO;
using System.Text.Json;
using AvantTest.Models.Dadata;


namespace AvantTest.Controllers
{
    /// <summary>
    /// Контроллер для работы с контрагентами
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ContractorController : ControllerBase
    {
        IDatasource db;
        public ContractorController(IDatasource _db)
        {
            db = _db;
        }
        
        
        /// <summary>
        /// Метод для получения всех записей контрагентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Contractor> result;
            try
            {
                result = db.GetAll();
            }
            catch (Exception)
            {
                return BadRequest("Ошибка базы данных");
            }

            return Ok(result);
        }

        /// <summary>
        /// Метод создания контрагента
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Contractor data)
        {
            try
            {
                if (db.CheckExist(data.INN, data.KPP))
                {
                    return BadRequest("Ошибка, этот контрагент существует");
                }
            }
            catch (Exception)
            {
                return BadRequest("Ошибка базы данных");
            }

            try
            {
                RootObject ddataResponse = await GetDadataInfo(data.INN, data.KPP);
                if (ddataResponse.suggestions.Count == 0)
                {
                    return BadRequest("Ошибка, по данной компании не найдено данных в ЕГРЮЛ");
                }
                else
                {
                    data.SetFullname(ddataResponse.suggestions[0].data.name.full_with_opf);
                }
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при получении информации о компании");
            }
            try
            {
                Contractor result = db.Create(data);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Ошибка при сохранении в базу данных");
            }
        }

        private async Task<AvantTest.Models.Dadata.RootObject> GetDadataInfo(string inn, string kpp)
        {
            string queryString;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://suggestions.dadata.ru/suggestions/api/4_1/rs/findById/party");
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Headers.Add("Authorization", "Token 5fc4bd7d1fe9ede218ed6782625a35d3d4c2a737");


            if (kpp == null)
            {
                queryString = $"{{\"query\":\"{inn}\"}}";
            }
            else
            {
                queryString = $"{{\"query\":\"{inn}\",\"kpp\":\"{kpp}\"}}";
            }

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(queryString);
            request.ContentLength = byteArray.Length;
            //отправляем данные
            using (Stream requestStream = await request.GetRequestStreamAsync())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            //получаем данные
            using (var responseStream = response.GetResponseStream())
            {
                RootObject responseObj = await JsonSerializer.DeserializeAsync<AvantTest.Models.Dadata.RootObject>(responseStream);
                return responseObj;
            }
        }

    }
}