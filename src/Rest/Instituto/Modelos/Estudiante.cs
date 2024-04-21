using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instituto.Modelos
{
    public class Estudiante
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.Empty;

        [BsonElement("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("dni")]
        public string DNI { get; set; } = string.Empty;

        [BsonElement("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [BsonElement("fecha_alta")]
        public DateTime FechaAlta { get; set; }

        [BsonElement("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }
}
