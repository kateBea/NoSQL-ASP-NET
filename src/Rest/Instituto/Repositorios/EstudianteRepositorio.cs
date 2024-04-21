using AutoMapper;
using Instituto.Modelos;
using Instituto.Modelos.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Instituto.Repositorios
{
    public class EstudianteRepositorio : IEstudianteRepositorio
    {
        #region Properties
        private readonly IMongoCollection<Estudiante> _estudiantes;
        private readonly IMapper _estudianteMapper;
        #endregion

        public EstudianteRepositorio(IMongoClient client, IMapper estudianteMapper)
        {
            // NOTE: if the collection doesn’t already exist, the client will
            // automatically create it when we first try to access it.

            var database = client.GetDatabase("Instituto");
            var collection = database.GetCollection<Estudiante>(nameof(Estudiante));

            _estudiantes = collection;

            _estudianteMapper = estudianteMapper;
        }

        public async Task<bool> Actualizar(CrearEstudianteDTO nuevo)
        {
            var mapped = _estudianteMapper.Map<Estudiante>(nuevo);

            var filter = Builders<Estudiante>.Filter.Eq(estudiante => estudiante.Id, mapped.Id);
            var update = Builders<Estudiante>.Update
                .Set(estudiante => estudiante.Nombre, mapped.Nombre)
                .Set(estudiante => estudiante.Telefono, mapped.Telefono)
                .Set(estudiante => estudiante.FechaNacimiento, mapped.FechaNacimiento);

            var result = await _estudiantes.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> BorrarPorID(ObjectId id)
        {
            var filter = Builders<Estudiante>.Filter.Eq(estudiante => estudiante.Id, id);
            var result = await _estudiantes.DeleteOneAsync(filter);

            return result.DeletedCount > 0;
        }

        public async Task<EstudianteConsultaDefaultDTO?> ConsultarPorDNI(string dni)
        {
            var filter = Builders<Estudiante>.Filter.Eq(estudiante => estudiante.DNI, dni);
            Estudiante? est = await _estudiantes.Find(filter).FirstOrDefaultAsync();
            EstudianteConsultaDefaultDTO? result = _estudianteMapper.Map<EstudianteConsultaDefaultDTO>(est);

            return result;
        }

        public async Task<EstudianteConsultaDefaultDTO?> ConsultarPorID(ObjectId objectId)
        {
            var filter = Builders<Estudiante>.Filter.Eq(estudiante => estudiante.Id, objectId);
            Estudiante? est = await _estudiantes.Find(filter).FirstOrDefaultAsync();
            EstudianteConsultaDefaultDTO? result = _estudianteMapper.Map<EstudianteConsultaDefaultDTO>(est);

            return result;
        }

        public async Task<IEnumerable<EstudianteConsultaDefaultDTO>> ConsultarTodos()
        {
            List<EstudianteConsultaDefaultDTO> result = new List<EstudianteConsultaDefaultDTO>();
            var estudiantes = await _estudiantes.Find(_ => true).ToListAsync();
            
            foreach (var estudiante in estudiantes)
            {
                result.Add(_estudianteMapper.Map<EstudianteConsultaDefaultDTO>(estudiante));
            }

            return result;
        }

        public async Task<ObjectId> Insertar(CrearEstudianteDTO est)
        {
            var nuevo = _estudianteMapper.Map<Estudiante>(est);

            nuevo.FechaAlta = DateTime.Now;
            await _estudiantes.InsertOneAsync(nuevo);
            return nuevo.Id;
        }
    }
}
