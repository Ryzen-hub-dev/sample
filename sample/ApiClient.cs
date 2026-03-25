using Newtonsoft.Json;
using RestSharp;
using System;

namespace sample
{
    public static class ApiClient
    {
        // API 根地址（确保以斜杠结尾）
        private const string BaseUrl = "https://admin.getfaisal.xyz/api/";

        // 固定 API Key
        private const string ApiKey = "96YC6-Z9G44-VM5KT-3Q2SV-D53ZB";

        // 创建 RestClient（手动传 base URL）
        private static readonly RestClient client = new RestClient();

        /// <summary>
        /// GET 请求，带 key 参数（用于读取数据）
        /// </summary>
        public static T Get<T>(string endpoint)
        {
            // 拼接完整 URL 并添加 key 以避免缓存
            string fullUrl = $"{BaseUrl}{endpoint}?key={ApiKey}&t={DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";

            var request = new RestRequest(fullUrl, Method.Get);
            request.AddHeader("User-Agent", "Mozilla/5.0");

            var response = client.Execute(request);

            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception($"[GET] API Failed: {response.StatusCode} - {response.ErrorMessage}\n{response.Content}");
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        /// <summary>
        /// POST 请求，带 key 参数（用于写入数据）
        /// </summary>
        public static T Post<T>(string endpoint, object data)
        {
            string fullUrl = $"{BaseUrl}{endpoint}?key={ApiKey}";

            var request = new RestRequest(fullUrl, Method.Post);
            request.AddHeader("User-Agent", "Mozilla/5.0");
            request.AddHeader("Content-Type", "application/json");

            string json = JsonConvert.SerializeObject(data);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = client.Execute(request);

            if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
            {
                throw new Exception($"[POST] API Failed: {response.StatusCode} - {response.ErrorMessage}\n{response.Content}");
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
