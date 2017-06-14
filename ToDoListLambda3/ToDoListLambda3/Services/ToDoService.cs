using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ToDoListLambda3.Models;

namespace ToDoListLambda3.Services
{
    public class ToDoService : IDisposable
    {
        HttpClient client;

        Uri uriLambdaService = new Uri("http://lambda3todoapi.azurewebsites.net/api/todo", UriKind.Absolute);

        public ToDoService()
        {
            client = new HttpClient()
            {
                MaxResponseContentBufferSize = 256000
            };
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public async Task<List<ToDo>> ObterListaAsync()
        {
            try
            {
                var response = await client.GetAsync(uriLambdaService);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lista = JsonConvert.DeserializeObject<List<ToDo>>(content);
                    return lista;
                }
            }
            catch (Exception ex)
            {                
            }

            return null;
            
        }

        public async Task<bool> SalvarItem(ToDo toDo)
        {
            var jsonToDo = JsonConvert.SerializeObject(toDo);

            var contentToDo = new StringContent(jsonToDo, Encoding.UTF8, "application/json");            

            var response = await client.PostAsync(uriLambdaService, contentToDo);

            return response.IsSuccessStatusCode;
        } 

    }
}
