using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer.Models;
using Newtonsoft.Json;

namespace PresentationLayer
{

    public class RestClient : HttpClient
    {
        private string _baseUri = "https://Jmalovic.bsite.net";
        private string _accessToken = "";
        public RestClient()
        {
            InitializeTlsProtocol();
        }

        private string AddQueryString(string uri, Dictionary<string, string> query)
        {
            if (query == null || query.Count == 0)
            {
                return uri;
            }
            var stringBuilder = new StringBuilder(uri);
            stringBuilder.Append("?");
            foreach (var key in query.Keys)
            {
                stringBuilder.Append(key + "=" + query[key] + "&");
            }
            return stringBuilder.ToString().TrimEnd('&');
        }

        private async Task<string> DohvatiGresku(HttpResponseMessage response)
        {
            string poruka = "";
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var odgovor = JsonConvert.DeserializeObject<Greska>(jsonString);
                if (odgovor.Poruka != null)
                    poruka = odgovor.Status + " " + odgovor.Poruka;
                else if (odgovor.Message != null)
                    poruka = odgovor.Message + " " + odgovor.MessageDetail;
                else
                    poruka = response.ReasonPhrase;
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var odgovor = JsonConvert.DeserializeObject<Greska>(jsonString);
                if (odgovor.InnerException.ExceptionMessage != null)
                    poruka = odgovor.Message + " " + odgovor.InnerException.ExceptionMessage;
                else if (odgovor.Message != null)
                    poruka = odgovor.Message + " " + odgovor.MessageDetail;
                else
                    poruka = response.ReasonPhrase;
            }
            else
                poruka = response.ReasonPhrase;

            return poruka;
        }

        //public async Task<T> GetAsync<T>(string url, Dictionary<string, string> query)
        //{
        //    await SetTokenAsync(); DefaultRequestHeaders.Clear();
        //    DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
        //    DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken); 
        //    using (HttpResponseMessage response = await GetAsync(AddQueryString(_baseUri + url, query)))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var jsonString = await response.Content.ReadAsStringAsync();
        //            return JsonConvert.DeserializeObject<T>(jsonString);
        //        }
        //        else if (response.StatusCode == HttpStatusCode.NotFound)
        //        {
        //            throw new Exception("Nema pronađenih podatak");
        //        }
        //        else
        //        {
        //            throw new Exception(await this.DohvatiGresku(response));
        //        }
        //    }
        //}
        public async Task<T> GetAsync<T>(string url)
        {
            SetTokenAsync(); DefaultRequestHeaders.Clear();
            DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            using (HttpResponseMessage response = await GetAsync(_baseUri + url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Nema pronađenih podatak");
                }
                else
                {
                    throw new Exception(await this.DohvatiGresku(response));
                }
            }
        }
        public async Task<T> PostAsync<T>(string url, string data)
        {
            SetTokenAsync();
            DefaultRequestHeaders.Clear();
            DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await PostAsync(_baseUri + url, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                else
                {
                    throw new Exception(await this.DohvatiGresku(response));
                }
            }
        }
        public async Task<T> PutAsync<T>(string url, string data)
        {
            SetTokenAsync();
            DefaultRequestHeaders.Clear();
            DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await PutAsync(_baseUri + url, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                else
                {
                    throw new Exception(await this.DohvatiGresku(response));
                }
            }
        }
        public async Task<T> DeleteAsync<T>(string url)
        {
            SetTokenAsync();
            DefaultRequestHeaders.Clear();
            DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue() { NoCache = true };
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            //var content = new StringContent(data, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await base.DeleteAsync(_baseUri + url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonString);
                }
                else
                {
                    throw new Exception(await this.DohvatiGresku(response));
                }
            }
        }

        private void InitializeTlsProtocol()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13 | SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
       private void SetTokenAsync() { 
        //{
        //    if (_token == null || _token.Expiration > DateTime.Now)
        //    {
        //        DefaultRequestHeaders.Clear();
        //        DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "SOME CREDENTIAL SCHEME"); var content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded"); using (HttpResponseMessage response = await PostAsync(_baseUri + "/some-path-to-get/token", content))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var jsonString = await response.Content.ReadAsStringAsync();
        //                _token = JsonConvert.DeserializeObject<TokenResponse>(jsonString);
        //            }
        //            else
        //            {
        //                throw new Exception(response.ReasonPhrase);
        //            }
        //        }
        //    }
        }
    }
}
