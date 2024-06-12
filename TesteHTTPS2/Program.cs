using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Configurando Kestrel para usar HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions
    {
        SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
        ClientCertificateMode = ClientCertificateMode.AllowCertificate,
        //ServerCertificate = new X509Certificate2("./certs/certificado.pfx", "Mktestehttps2sdac2131xasdaDWA")
        ServerCertificate = new X509Certificate2("./certs/crc.net.br-pfx.pfx", "SZ6MT5CM69Z6RK")
    };

    options.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.UseHttps(httpsConnectionAdapterOptions);
    });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
