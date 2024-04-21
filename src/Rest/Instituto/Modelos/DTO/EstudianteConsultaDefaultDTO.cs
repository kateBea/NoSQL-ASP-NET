using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;
using System.Text.Json.Serialization;

namespace Instituto.Modelos.DTO
{
    public class EstudianteConsultaDefaultDTO
    {

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [JsonPropertyName("dni")]
        public string DNI { get; set; } = string.Empty;

        [JsonPropertyName("telefono")]
        public string Telefono { get; set; } = string.Empty;

        [JsonPropertyName("fecha_alta")]
        public DateTime FechaAlta { get; set; }

        [JsonPropertyName("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }
    }
}
