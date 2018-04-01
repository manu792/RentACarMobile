using SQLite;

namespace RentaCarros.Modelos
{
    public class Carro
    {
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        [PrimaryKey]
        public string Placa { get; set; }
        public int Kilometraje { get; set; }
    }
}