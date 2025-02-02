﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_rental_webAPI.Properties.Domains;
using senai_rental_webAPI.Properties.Interfaces;
using senai_rental_webAPI.Properties.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_rental_webAPI.Properties.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private IVeiculoRepository _VeiculoRepository { get; set;  }

        public VeiculosController()
        {
            _VeiculoRepository = new VeiculoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<VeiculoDomain> ListaVeiculos = _VeiculoRepository.ListarTodos();

                return Ok(ListaVeiculos);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            try
            {
                VeiculoDomain VeiculoBuscado = _VeiculoRepository.BuscarPorid(id);

                if (VeiculoBuscado == null)
                {
                    return NotFound("Veiculo não encontrado");
                }

                return Ok(VeiculoBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
            
        }

        [HttpPost]
        public IActionResult Post(VeiculoDomain NovoVeiculo)
        {
            try
            {
                _VeiculoRepository.Cadastrar(NovoVeiculo);
                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpDelete("{IdDeleta}")]
        public IActionResult Delete(int IdDeleta)
        {
            try
            {
                _VeiculoRepository.Deletar(IdDeleta);
                return StatusCode(204);

            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }

        [HttpPut]
        public IActionResult Put(VeiculoDomain VeiculoAtualizar)
        {
            VeiculoDomain VeiculoBuscado = _VeiculoRepository.BuscarPorid(VeiculoAtualizar.IdVeiculo);

            if (VeiculoBuscado != null)
            {
                try
                {
                    _VeiculoRepository.Atualizar(VeiculoAtualizar);
                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }

            }

            return NotFound(new
            {
                mensagem = "Veiculo não encontrado",
                Coderro = true
            });

        }
    }
}
