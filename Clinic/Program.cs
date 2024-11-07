using Microsoft.EntityFrameworkCore;
using ClinicDataBusinessLayer.Extensions;
using ClinicDataAccessLayer.Extensions;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
				.AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddataAccessLayerServices(builder.Configuration.GetConnectionString("DefaultConnectionString"));
builder.Services.AddBusinessLayerServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseRequestLocalization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


