using SQLite;

namespace RentaCarros.Modelos
{
    public class Carro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CategoriaId { get; set; }
        [Unique]
        public string Placa { get; set; }
        public int Kilometraje { get; set; }
    }
}