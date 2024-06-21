using System.ComponentModel.DataAnnotations;

namespace ServicioClienteAPIMin.Models.Client.UpdateClients;

public class UpdateClient
{
  public string Name { get; set; }
  public int Age { get; set; }
}
