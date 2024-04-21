using AutoMapper;
using Instituto.Modelos;
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

            var database = client.GetDatabase("MongoDBCS");
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

        public async Task<Estudiante?> ConsultarPorDNI(string dni)
        {
            var filter = Builders<Estudiante>.Filter.Eq(estudiante => estudiante.DNI, dni);
            Estudiante? estResult = await _estudiantes.Find(filter).FirstOrDefaultAsync();

            return estResult;
        }

        public async Task<Estudiante?> ConsultarPorID(ObjectId objectId)
        {
            var filter = Builders<Estudiante>.Filter.Eq(estudiante => estudiante.Id, objectId);
            Estudiante? estResult = await _estudiantes.Find(filter).FirstOrDefaultAsync();

            return estResult;
        }

        public async Task<IEnumerable<Estudiante>> ConsultarTodos()
        {
            var estudiantes = await _estudiantes.Find(_ => true).ToListAsync();
            return estudiantes;
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
