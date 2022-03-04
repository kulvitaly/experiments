using Azure.Identity;
using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestKeyVault.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public async Task<VaultInfo> Get()
        {
            try
            {
                // read secret1 from Key Vault

                var kvUri = "https://vku-kv-az204.vault.azure.net";
                SecretClient client = new(new Uri(kvUri), new DefaultAzureCredential());

                var secret = (await client.GetSecretAsync("secretColor")).Value.Value;

                return new VaultInfo
                {
                    Value = secret,
                    SecretFromVaultRef = _config.GetValue<string>("mySecret")
                };
            }
            catch (Exception ex)
            {
                return new VaultInfo
                {
                    Error = ex.ToString(),
                    SecretFromVaultRef = _config.GetValue<string>("mySecret")
                };
            }
        }

        [HttpGet("key")]
        public async Task<VaultInfo> GetKey()
        {
            try
            {
                var vaultName = "vku-kv-az204";
                var keyName = "ssedemo01";

                var kvUri = $"https://{vaultName}.vault.azure.net";
                KeyClient client = new(new Uri(kvUri), new DefaultAzureCredential());

                var key = (await client.GetKeyAsync(keyName)).Value;

                return new VaultInfo
                {
                    Value = key.KeyType.ToString()
                };
            }
            catch (Exception ex)
            {
                return new VaultInfo
                {
                    Error = ex.ToString()
                };
            }
        }

        [HttpGet("certificate")]
        public async Task<VaultInfo> GetCertificate()
        {
            try
            {
                var vaultName = "vku-kv-az204";
                var keyName = "test1";

                var kvUri = $"https://{vaultName}.vault.azure.net";
                CertificateClient client = new(new Uri(kvUri), new DefaultAzureCredential());

                var key = (await client.GetCertificateAsync(keyName)).Value;

                return new VaultInfo
                {
                    Value = key.KeyId.AbsoluteUri
                };
            }
            catch (Exception ex)
            {
                return new VaultInfo
                {
                    Error = ex.ToString()
                };
            }
        }
    }
}
