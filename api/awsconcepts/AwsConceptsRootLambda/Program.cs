using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Contrib.Extensions.AWSXRay.Trace;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RestApiControllers;

//AWSSDKHandler.RegisterXRayForAllServices();
Sdk.SetDefaultTextMapPropagator(new AWSXRayPropagator());
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
builder.Services.AddControllers().WithApplicationDomainControllers();
// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi)
    .WithApiControllerServiceDependencies(builder.Configuration)
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = options.DefaultScheme = options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        //x.MetadataAddress = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_QBoIUnnt6/.well-known/openid-configuration";
        x.Audience = "4trdfg3041lq5nqgbfqrsjiod9";
        x.Authority = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_QBoIUnnt6";
        x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://www.awsconcepts.com", "https://awsconcepts.com", "https://api.awsconcepts.com").AllowAnyHeader()
                                                  .AllowAnyMethod();
        });
});

builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
    .AddXRayTraceId()
        .AddSource("AWSConceptsLambdaApi")
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName: "AWSConceptsLambdaApi", serviceVersion: "1"))
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
    .AddAWSLambdaConfigurations()
        ;
});

var app = builder.Build();

//app.UseXRay("AWSConceptsLambdaApi");
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers().RequireAuthorization();

app.MapGet("/", () => "Ok");

app.Run();
