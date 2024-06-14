namespace SCADA_Core.DTOs;

public class BaseTagInfoDto
{
    public string Id { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Description: {Description}";
    }
}