using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Devagran.Dtos;

namespace Devagran.Services
{
    public class CosmicService
    {
        public string ImageUpload(ImageDto imageDto)
        {
            Stream image = imageDto.Image.OpenReadStream();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "jKkRahpQzKasA94ZbDdBNyi3NCD0WChDrKZldqOruNSIHVUPry");

            var request = new HttpRequestMessage(HttpMethod.Post, "file");
            var content = new MultipartFormDataContent
            {
                { new StreamContent(image), "media", imageDto.Title },
            };

            request.Content = content;

            var response = client.PostAsync("https://upload.cosmicjs.com/v2/buckets/none-devagran/media", request.Content).Result;

            var urlResponse = response.Content.ReadFromJsonAsync<CosmicResponseDto>();

            return urlResponse.Result.Media.url;
        }
    }
}