using Microsoft.AspNetCore.Mvc;

namespace ManagingSecrets.Controllers
{
    /// <summary>
    /// Represents the controller for handling secret-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SecretsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SecretsController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Retrieves the value of the specified environment variable for the Windows platform.
        /// </summary>
        /// <param name="variable">The name of the environment variable to retrieve. (Default: LOCAL_SECRET_VARIABLE)</param>
        /// <returns>
        /// Returns an Ok response with the value of the environment variable if found.
        /// Returns a BadRequest response with the message "Variable not found" if the specified environment variable is not found or its value is empty or null.
        /// Returns a BadRequest response with the message "This endpoint is for Windows Platform Only" if the platform is not Windows.
        /// </returns>
        /// <example>
        /// GET /local?variable=LOCAL_SECRET_VARIABLE
        /// </example>
        [HttpGet("local")]
        public async Task<IActionResult> GetLocalSecret([FromQuery] string variable = "LOCAL_SECRET_VARIABLE")
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                //var secretValues = await Task.Run(() => Environment.GetEnvironmentVariables());
                var secretValue = await Task.Run(() => Environment.GetEnvironmentVariable(variable));
                return secretValue == null ? BadRequest("Variable not found") : Ok(secretValue);
            }
            return BadRequest("This endpoint is for Windows Platform Only");
        }

        /// <summary>
        /// Retrieves the value of the specified environment variable.
        /// </summary>
        /// <param name="variable">The name of the environment variable to retrieve. (Default: AZURE_SECRET_VARIABLE)</param>
        /// <returns>
        /// Returns an Ok response with the value of the environment variable if found.
        /// Returns a BadRequest response with the message "Variable not found" if the specified environment variable is not found or its value is empty or null.
        /// </returns>
        [HttpGet("azureAppConfiguration")]
        public async Task<IActionResult> GetAzureSecret([FromQuery] string variable = "AZURE_SECRET_VARIABLE")
        {
            var secretValue = await Task.Run(() => Environment.GetEnvironmentVariable(variable));
            return secretValue == null ? BadRequest("Variable not found") : Ok(secretValue);
        }

        /// <summary>
        /// Retrieves the value of the specified setting from the app settings configuration.
        /// </summary>
        /// <param name="variable">The key of the setting to retrieve. (Default: DefaultConnection)</param>
        /// <returns>
        /// Returns an Ok response with the value of the setting if found.
        /// Returns a BadRequest response with the message "Variable not found" if the specified setting is not found or its value is empty or null.
        /// </returns>
        [HttpGet("azureSqlConnection")]
        public async Task<IActionResult> GetAzureConnectionString([FromQuery] string variable = "DefaultConnection")
        {
            var secretValue = await Task.Run(() => _configuration.GetConnectionString(variable));
            return secretValue == null ? BadRequest("Variable not found") : Ok(secretValue);
        }

        /// <summary>
        /// Retrieves the value of the specified setting from the app settings configuration.
        /// </summary>
        /// <param name="variable">The key of the setting to retrieve. (Default: AppConfiguration:SecretValue)</param>
        /// <returns>
        /// Returns an Ok response with the value of the setting if found.
        /// Returns a BadRequest response with the message "Variable not found" if the specified setting is not found or its value is empty or null.
        /// </returns>
        [HttpGet("appSettings")]
        public async Task<IActionResult> GetAppSettingsSecret([FromQuery] string variable = "AppConfiguration:SecretValue")
        {
            var secretValue = await Task.Run(() => _configuration.GetValue<string>(variable));
            return secretValue == null ? BadRequest("Variable not found") : Ok(secretValue);
        }


    }
}
