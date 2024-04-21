using AutoMapper;
using Instituto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instituto.Mappers
{
    public class EstudianteMapper : Profile
    {
        public EstudianteMapper()
        {
            CreateMap<CrearEstudianteDTO, Estudiante>();
        }
    }
}
