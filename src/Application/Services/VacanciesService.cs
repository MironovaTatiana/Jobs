using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Dtos;
using Domain;
using Newtonsoft.Json.Linq;

namespace Application.Services
{
    public class VacanciesService : IVacanciesService
    {
        /// <summary>
        /// Получение списка вакансий
        /// </summary>
        public async ValueTask<IEnumerable<IJob>> GetVacanciesList(int count)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "d-fens HttpClient");
            var request = $"https://api.hh.ru/vacancies/?per_page={count}&only_with_salary=true";
            HttpResponseMessage response =
                (await httpClient.GetAsync(request)).EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var jobs = new List<JobDto>();
            var responseJson = TryLoadJson(responseBody);

            foreach (var itm in responseJson)
            {
                foreach (var child in itm.Value.Children())
                {
                    jobs.Add(new JobDto
                    {
                        Id = (int)child.SelectToken("id"),
                        Name = child.SelectToken("name").ToString(),
                        SalaryFrom = SetDecimalValue(child.SelectToken("salary.from").ToString()),
                        SalaryTo = SetDecimalValue(child.SelectToken("salary.to")),
                    });
                }
            }

            return jobs;
        }

        /// <summary>
        /// Установка числового значения
        /// </summary>
        private decimal SetDecimalValue(JToken token)
        {
            return token.ToString() != string.Empty
                                    ? (decimal)token
                                    : 0m;
        }

        /// <summary>
        /// Попытка загрузить JSON из ответа
        /// </summary>
        private JObject TryLoadJson(string responseText)
        {
            try
            {
                return JObject.Parse(responseText);
            }
            catch
            {
                return null;
            }
        }
    }
}
