using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vestillo.Lib
{
    public class ConnectionWebAPI<TModel>
    {
        //private HttpClient client;
        private const string user = "vestillo";
        private const string password = "1234";

        public ConnectionWebAPI(string urlBase)
        {
        //    client = new HttpClient();
        //    client.BaseAddress = new Uri(urlBase);
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Add("Authorization", "Basic " + this.UserWebAPI);
        }

        private string UserWebAPI
        {
            get
            {
                var encoding = new System.Text.UTF8Encoding().GetBytes(user + ":" + password);
                return Convert.ToBase64String(encoding);
            }
        }

        public IEnumerable<TModel> Get(string requestUri)
        {
            //System.Net.Http.HttpResponseMessage response = client.GetAsync(requestUri).Result;
         
            //if (response.IsSuccessStatusCode)
            //{
            //    var uri = response.Headers.Location;
            //    return response.Content.ReadAsAsync<IEnumerable<TModel>>().Result;
            //}
            //else
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}

            throw new NotImplementedException();
       
        }

        public TModel Get(string requestUri, int id)
        {
            //System.Net.Http.HttpResponseMessage response = client.GetAsync(requestUri + @"/" + id.ToString()).Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var uri = response.Headers.Location;
            //    return response.Content.ReadAsAsync<TModel>().Result;
            //}
            //else
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}

            throw new NotImplementedException();

        }

        public TModel Get(string requestUri, string queryString)
        {
            //System.Net.Http.HttpResponseMessage response = client.GetAsync(requestUri + @"?" + queryString).Result;
         
            //if (response.IsSuccessStatusCode)
            //{
            //    var uri = response.Headers.Location;
            //    return response.Content.ReadAsAsync<TModel>().Result;
            //}
            //else
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}

            throw new NotImplementedException();

        }

        public void Post(string requestUri,ref TModel entity)
        {
            //System.Net.Http.HttpResponseMessage response = client.PostAsJsonAsync<TModel>(requestUri, entity).Result;
         
            //if (! response.IsSuccessStatusCode)
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}

            throw new NotImplementedException();
        }

        public void Put(string requestUri, int id, ref TModel entity)
        {
            //System.Net.Http.HttpResponseMessage response = client.PutAsJsonAsync<TModel>(requestUri + @"/" + id.ToString(), entity).Result;
        
            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}
        }

        public void Delete(string requestUri, int id)
        {
            //System.Net.Http.HttpResponseMessage response = client.DeleteAsync(requestUri + @"/" + id.ToString()).Result;
         
            //if (!response.IsSuccessStatusCode)
            //{
            //    throw new HttpRequestException(response.ReasonPhrase);
            //}
        }
    }
}
