using SQLite;

namespace RentaCarros.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }
        [PrimaryKey]
        public string Cedula { get; set; }
        public string Nombre { get; set; }
    }
}