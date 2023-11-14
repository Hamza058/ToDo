using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using Web.Models;
using Web.Service.IService;
using static Web.Utility.SD;

namespace Web.Service
{
    public class GateWay
    {
        private readonly ITokenProvider _tokenProvider;

        public GateWay(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            //var httpClient=new HttpClient();
            //var jsonEmployee=JsonConvert.SerializeObject(obj);
            //var content= new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            //var responseMessage = await httpClient.PostAsync("https://localhost:7000/api/Auth/login",content);
            //var jsonString=await responseMessage.Content.ReadAsStringAsync();
            //var values = JsonConvert.DeserializeObject<string[]>(jsonString);
            //return Ok(values);

            var httpClient = new HttpClient();
            
            HttpResponseMessage? apiResponse = null;
            HttpContent? content = null;
            HttpRequestMessage? requestMessage = null;

            if(requestDto.Data != null)
            {
                content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
            }
            switch (requestDto.ApiType)
            {
                case ApiType.POST:
                    apiResponse = await httpClient.PostAsync(requestDto.Url, content);
                    break;
                case ApiType.DELETE:
                    apiResponse = await httpClient.DeleteAsync(requestDto.Url);
                    break;
                case ApiType.PUT:
                    apiResponse = await httpClient.PutAsync(requestDto.Url, content);
                    break;
                default:
                    apiResponse = await httpClient.GetAsync(requestDto.Url);
                    break;
            }
            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message = "Not Found" };

                case HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access Denied" };

                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Unauthorized" };

                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();

                    JObject inputObject = JObject.Parse(apiContent);
                    JObject resultObject = new JObject();
                    resultObject["result"] = inputObject;
                    string resultJson = resultObject.ToString(Formatting.None);

                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(resultJson);
                    return apiResponseDto;
            }
        }
    }
}
