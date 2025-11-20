
namespace Core.Models
{
    public class Bruger
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }
        public string Password { get; set; }

        public bool admin { get; set; } = false;
    }
}