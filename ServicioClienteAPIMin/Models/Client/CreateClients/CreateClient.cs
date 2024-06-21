using System.ComponentModel.DataAnnotations;

namespace ServicioClienteAPIMin.Models.Client.CreateClients;

public class CreateClient
{
  public int Id { get; set; }
  public string Name { get; set; }
  public int Age { get; set; }
}
