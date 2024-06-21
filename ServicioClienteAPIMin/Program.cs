using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServicioClienteAPIMin.Models.Client;
using ServicioClienteAPIMin.Models.Client.CreateClients;
using ServicioClienteAPIMin.Models.Client.UpdateClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<DataClient> dataClients = [
  new DataClient(){Id = 1, Name = "Gerardo", Age = 34},
  new DataClient(){Id = 2, Name = "Ramiro", Age = 18}, 
  new DataClient(){Id = 3, Name = "Ximena", Age = 25}, 
];

DataClient? obtainClient(int id){
  var client = dataClients.FirstOrDefault(x => x.Id == id);
  return client;
}

bool validation(string name, int age){
  if(name == string.Empty || age < 18 || age > 65)
  {
    return false;
  }
  return true;
}

app.MapGet("/ListDataClient", ()=>{
  return Results.Ok(dataClients); 
});

app.MapGet("/GetDataClient/{id}", (int id)=>{
  DataClient? client = obtainClient(id);
  if (client is null){
    return Results.NotFound("Client not found");
  }
  return Results.Ok(client);
});

//Post, Put Delete 

app.MapPost("/CreateClient",(CreateClient req) => {
  var fields = validation(req.Name, req.Age);
  if (!fields)
  {
    return Results.BadRequest("Empty or invalid fields");
  }
  else if(req.Id < 0){
    return Results.BadRequest("invalid id");
  }

  var client = new DataClient();
  client.Id = req.Id;
  client.Name = req.Name;
  client.Age = req.Age;
  dataClients.Add(client);
  return Results.Ok($"Client {req.Name} was registered");
});

app.MapPut("/UpdateClient/{id}", (int id, UpdateClient req) => {
  var fields = validation(req.Name, req.Age);
  if(!fields){
    return Results.BadRequest("Empty or invalid fields");
  }
  else 
  {
    DataClient? client = obtainClient(id);
    if(client is null)
      return Results.NotFound("Client not found");
    client.Name = req.Name;
    client.Age = req.Age;
    return Results.Ok($"Client {req.Name} has updated");
  }
});

app.MapDelete("/DeleteClient/{id}", (int id) => {
  DataClient? client = obtainClient(id);
  if(client is null)
    return Results.NotFound($"Client not found");
  return Results.Ok($"Client {client.Name} was delete");
});

app.Run();