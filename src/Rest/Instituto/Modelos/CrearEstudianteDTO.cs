using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instituto.Modelos
{
    public class CrearEstudianteDTO
    {

        public string Nombre { get; set; } = string.Empty;

        public string DNI { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }
    }
}
