using System;
using System.Collections.Generic;
using System.Linq;
using APICsv.Database;
using APICsv.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace APICsv.Controllers
{
    [Route("api/[controller]")] // rota (aspectos) controller vai ser substituido por animais nesse caso 
    [ApiController]// controlador de api
    public class AnimaisController : ControllerBase
    {
        private readonly DBContext _dbContext;
        public AnimaisController(DBContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        [HttpGet]
        public ActionResult<List<Animal>> GetAll()
        {
            return Ok(_dbContext.Animals);
        }

        [HttpGet("{id}")]
        public ActionResult<Animal> GetById(int id)
        {
            try
            {

                Animal animal =
                   _dbContext.Animals.Find(a => a.Id == id);


                if (animal == null)
                    return NotFound();//404

                return Ok(animal);//200

            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message); //400
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            try
            {

                Animal animal =
                   _dbContext.Animals.Find(a => a.Id == id);


                if (animal == null)
                    return NotFound();//404

                _dbContext.Animals.Remove(animal);


                return NoContent();

            }
            catch (System.Exception)
            {
                return BadRequest(); //400
            }
        }

        [HttpPatch("AlterarNome")]
        public ActionResult<Animal> AlterarNome(
            [FromBody] Animal body)
        {
            Animal animal =
                _dbContext.Animals.Find(a => a.Id == body.Id);

            if (animal == null)
                return NotFound();

            animal.Name = body.Name;

            return Ok(animal);
        }

        //put e post 

        [HttpPut("AtualizarAnimal")]
        public ActionResult<Animal> put(
            [FromBody] Animal novoAnimal)
        {
            try
            {
                Animal alterado = _dbContext.Animals.FirstOrDefault(a => a.Id == novoAnimal.Id);

                if (novoAnimal == null)
                    return NotFound();

                alterado.Name = novoAnimal.Name;
                alterado.Classification = novoAnimal.Classification;
                alterado.Origin = novoAnimal.Origin;
                alterado.Reproduction = novoAnimal.Reproduction;
                alterado.Feeding = novoAnimal.Feeding;
                return Ok(novoAnimal);
            }
            catch (SystemException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CadastrarNovoAnimal")]
        public ActionResult<Animal> post(
            [FromBody] Animal novoAnimal)
        {
            try
            {
                _dbContext.Animals.Add(novoAnimal);
                return CreatedAtAction(nameof(GetById), new { id = novoAnimal.Id }, novoAnimal);

            }
            catch
            (System.Exception e)
            {
                return BadRequest(e.Message);

            }
        }
           private ActionResult<Animal> BadRequest(object message)
        {
            throw new NotImplementedException();
        }
    }
}

