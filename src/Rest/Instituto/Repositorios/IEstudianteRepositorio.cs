﻿using Instituto.Modelos;
using Instituto.Modelos.DTO;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instituto.Repositorios
{
    public interface IEstudianteRepositorio
    {
        // Create
        Task<ObjectId> Insertar(CrearEstudianteDTO est);

        // Read
        Task<EstudianteConsultaDefaultDTO?> ConsultarPorID(ObjectId objectId);
        Task<IEnumerable<EstudianteConsultaDefaultDTO>> ConsultarTodos();
        Task<EstudianteConsultaDefaultDTO?> ConsultarPorDNI(string dni);

        // Update
        Task<bool> Actualizar(CrearEstudianteDTO est);

        // Delete
        Task<bool> BorrarPorID(ObjectId id);
    }
}
