using Microsoft.Extensions.Configuration;

namespace BAOMINH_SOLUTION_WEB_API.Config
{
    public class Config
    {
        private readonly IConfiguration _configuration;

        public Config(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetUrlServerMediaConfig(string key)
        {
            return _configuration.GetValue<string>(key);
        }
        
        public string GetKeyConfig(string key)
        {
            return _configuration.GetValue<string>(key);
        }
    }
}
