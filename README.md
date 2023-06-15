# ManagingSecrets
A simple project to show how secrets are managed and consumed

# Managing Secrets Controller

The `SecretsController` is a controller class in an ASP.NET Core application that handles secret-related operations. It provides several endpoints for retrieving secrets from different sources such as environment variables and app settings.

## Endpoints

### GetLocalSecret

This endpoint retrieves the value of a specified environment variable on the Windows platform.

- **HTTP Method**: GET
- **Route**: /api/Secrets/local
- **Parameters**:
  - `variable` (optional): The name of the environment variable to retrieve. Default value is `LOCAL_SECRET_VARIABLE`.
- **Response**:
  - If the platform is Windows:
    - If the environment variable is found and its value is not empty, returns an Ok response with the value.
    - If the environment variable is not found or its value is empty or null, returns a BadRequest response with the message "Variable not found."
  - If the platform is not Windows, returns a BadRequest response with the message "This endpoint is for Windows Platform Only."

### GetAzureSecret

This endpoint retrieves the value of a specified environment variable under Application Settings blade in `Azure Web App`.
To use this endpoint you must have deployed a web app to Azure and added the desired variable to `Application settings` in the `configuration` blade

- **HTTP Method**: GET
- **Route**: /api/Secrets/azureAppConfiguration
- **Parameters**:
  - `variable` (optional): The name of the environment variable to retrieve. Default value is `AZURE_SECRET_VARIABLE`.
- **Response**:
  - If the environment variable is found and its value is not empty, returns an Ok response with the value.
  - If the environment variable is not found or its value is empty or null, returns a BadRequest response with the message "Variable not found."

### GetAzureConnectionString

This endpoint retrieves the value of a specified connection string from the app settings configuration in `Azure`. 
To use this endpoint you must have deployed a web app to azure and added the desired connection string to `Connection strings` in the `configuration` blade

- **HTTP Method**: GET
- **Route**: /api/Secrets/azureSqlConnection
- **Parameters**:
  - `variable` (optional): The key of the setting to retrieve. Default value is `DefaultConnection`.
- **Response**:
  - If the setting is found and its value is not empty, returns an Ok response with the value.
  - If the setting is not found or its value is empty or null, returns a BadRequest response with the message "Variable not found."

### GetAppSettingsSecret

This endpoint retrieves the value of a specified setting from the app settings configuration.

- **HTTP Method**: GET
- **Route**: /api/Secrets/appSettings
- **Parameters**:
  - `variable` (optional): The key of the setting to retrieve. Default value is `AppConfiguration:SecretValue`.
- **Response**:
  - If the setting is found and its value is not empty, returns an Ok response with the value.
  - If the setting is not found or its value is empty or null, returns a BadRequest response with the message "Variable not found."

## Consuming the API

To consume the API and interact with the endpoints, you can make HTTP requests to the corresponding routes using a tool like `curl`, `Postman`, or through code in your application.

Here are some examples using `curl`:

- Retrieve the value of a local secret:

