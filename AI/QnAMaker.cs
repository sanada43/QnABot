using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QnAMaker
{
    public class Program
    {
        // NOTE: Replace this with a valid host name.
        static string host = "https://qna-sakurada.azurewebsites.net/qnamaker";

        // NOTE: Replace this with a valid endpoint key.
        // This is not your subscription key.
        // To get your endpoint keys, call the GET /endpointkeys method.
        static string endpoint_key = "540ee3a7-1263-4de1-8cc1-3b239c4fd35f";

        // NOTE: Replace this with a valid knowledge base ID.
        // Make sure you have published the knowledge base with the
        // POST /knowledgebases/{knowledge base ID} method.
        static string kb = "24139419-a004-42f5-9923-ee1d03d20297";

        static string service = "/qnamaker";
        static string method = "/knowledgebases/" + kb + "/generateAnswer/";
        private string test;

        public Program(string test)
        {
            this.test = test;
        }

        static string question1 = @"{'question': '";
        static string question2 = @"','top': 3}";

        async static Task<string> Post(string uri, string body)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async static Task<string>program(string question)
        {
            var uri = host + service + method;
            //Console.WriteLine("Calling " + uri + ".");
            var response = Post(uri, question1 + question + question2);
            //Console.WriteLine(response);
            //Console.WriteLine("Press any key to continue.");
            return response.ToString();
        }

        public static implicit operator string(Program v)
        {
            throw new NotImplementedException();
        }
    }

    public class Program2
    {
        // NOTE: Replace this with a valid host name.
        static string host = "https://qna-sakurada.azurewebsites.net/qnamaker";

        // NOTE: Replace this with a valid endpoint key.
        // This is not your subscription key.
        // To get your endpoint keys, call the GET /endpointkeys method.
        static string endpoint_key = "540ee3a7-1263-4de1-8cc1-3b239c4fd35f";

        // NOTE: Replace this with a valid knowledge base ID.
        // Make sure you have published the knowledge base with the
        // POST /knowledgebases/{knowledge base ID} method.
        static string kb = "15748e93-7baa-4b0f-8423-7120d83e4c0f";

        static string service = "/qnamaker";
        static string method = "/knowledgebases/" + kb + "/generateAnswer/";
        private string test;

        public Program2(string test)
        {
            this.test = test;
        }

        static string question1 = @"{'question': '";
        static string question2 = @"','top': 3}";

        async static Task<string> Post(string uri, string body)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
                request.Headers.Add("Authorization", "EndpointKey " + endpoint_key);

                var response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public async static Task<string> program2(string question)
        {
            var uri = host + service + method;
            //Console.WriteLine("Calling " + uri + ".");
            var response = Post(uri, question1+question+question2);
            //Console.WriteLine(response);
            //Console.WriteLine("Press any key to continue.");
            return response.ToString();
        }

        public static implicit operator string(Program2 v)
        {
            throw new NotImplementedException();
        }
    }
}
