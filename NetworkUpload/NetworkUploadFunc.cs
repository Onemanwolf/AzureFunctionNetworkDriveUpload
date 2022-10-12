using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace NetworkUpload
{
    public  class NetworkUploadFunc
    {

        private readonly NetworkConnection _networkConnection;
        private  string _networkName ;

        public NetworkUploadFunc()
        {
            _networkName = @"\\your IP\ShareFolder";
            _networkConnection = new NetworkConnection(_networkName, new NetworkCredential("UserName", "Password"));
        }


        [FunctionName("NetworkUploadFunc")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];
         await   UploadFile("");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        public async Task UploadFile(string fileName)
        {
        var NetConnect = _networkConnection;


          var fileList = Directory.GetDirectories(_networkName);
          var files = Directory.GetFiles(_networkName);

            foreach (var item in fileList) {  _networkName = item;  }
            DirectoryInfo[] cDirs = new DirectoryInfo(@"C:\FileUpload").GetDirectories();
          string file = @"c:\FileUpload\CDriveDirs.txt";

            _networkName = _networkName + "\\fDriveDirs.txt";
            byte[] bytes = Encoding.UTF8.GetBytes(file);
            using (FileStream fileStream = File.Create(_networkName, bytes.Length))
            {
             await fileStream.WriteAsync(bytes, 0, bytes.Length);
                fileStream.Close();
            }

    }
}
}

