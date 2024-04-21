using FluentValidation;
using Instituto.Modelos;
using Instituto.Modelos.DTO;
using Instituto.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Rest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstudiantesController : ControllerBase
    {
        #region Properties
        private readonly IValidator<CrearEstudianteDTO> _validatorEstudiante;
        private readonly IEstudianteRepositorio _estudianteRepositorio;
        #endregion

        public EstudiantesController(IValidator<CrearEstudianteDTO> estudianteValidator, IEstudianteRepositorio estudianteRepositorio)
        {
            _validatorEstudiante = estudianteValidator;
            _estudianteRepositorio = estudianteRepositorio;
        }

        [HttpGet("ConsultarPorDNI/{dni}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstudianteConsultaDefaultDTO))]
        public async Task<ActionResult<EstudianteConsultaDefaultDTO>> ConsultarPorDNI([FromRoute] string dni)
        {
            var result = await _estudianteRepositorio.ConsultarPorDNI(dni);

            if (result == null) 
            {
                return NotFound();
            }

            return Ok(result);  
        }

        [HttpGet("ConsultarTodos")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstudianteConsultaDefaultDTO))]
        public async Task<ActionResult<EstudianteConsultaDefaultDTO>> ConsultarTodos()
        {
            var result = await _estudianteRepositorio.ConsultarTodos();
            return Ok(result);
        }

        [HttpPost("Insertar")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObjectId))]
        public async Task<ActionResult<ObjectId>> Insertar([FromBody] CrearEstudianteDTO nuevo)
        {
            var validationResult = _validatorEstudiante.Validate(nuevo);
            if (validationResult == null || !validationResult.IsValid)
            {
                return BadRequest(validationResult?.Errors);
            }

            var result = await _estudianteRepositorio.Insertar(nuevo);

            return Ok(result);
        }

        [HttpPut("Actualizar")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ObjectId))]
        public async Task<ActionResult<ObjectId>> Actualizar([FromBody] CrearEstudianteDTO nuevo)
        {
            var validationResult = _validatorEstudiante.Validate(nuevo);
            if (validationResult == null || !validationResult.IsValid)
            {
                return BadRequest(validationResult?.Errors);
            }

            var result = await _estudianteRepositorio.Actualizar(nuevo);

            if (!result)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpDelete("BorrarPorID/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstudianteConsultaDefaultDTO))]
        public async Task<ActionResult<EstudianteConsultaDefaultDTO>> BorrarPorID([FromRoute] ObjectId id)
        {
            var result = await _estudianteRepositorio.BorrarPorID(id);

            if (!result)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
