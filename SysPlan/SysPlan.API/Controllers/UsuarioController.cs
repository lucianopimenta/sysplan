using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SysPlan.Application.Service;
using SysPlan.Core.Controller;
using SysPlan.Core.Model;
using SysPlan.Core.Repository;
using SysPlan.Data;
using SysPlan.Data.Entities;
using System;
using System.Linq;

namespace SysPlan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Usuario> _repositoryUsuario;

        public UsuarioController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryUsuario = _unitOfWork.GetRepository<Usuario>();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult GetLogin([FromBody] LoginRequest login)
        {
            try
            {
                var senha = HelperService.ComputeHashMd5(login.password);
                var usuario = _repositoryUsuario.Get(x => x.Login.Equals(login.login) && x.Ativo).FirstOrDefault();
                if (usuario == null)
                {
                    return BadRequest(new ResponseResult()
                    {
                        success = false,
                        data = null,
                        errorMessage = "Usuário invalido"
                    });
                }

                if (usuario.Senha == senha)
                {
                    return Ok(new ResponseResult()
                    {
                        success = true,
                        data = usuario
                    });
                }
                else
                {
                    return BadRequest(new ResponseResult()
                    {
                        success = false,
                        data = null,
                        errorMessage = "Usuário/senha inválido."
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseResult()
                {
                    success = false,
                    data = ex.StackTrace,
                    errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario request)
        {
            using (var transaction = _repositoryUsuario.BeginTransaction())
            {
                try
                {
                    #region Validações

                    if (string.IsNullOrEmpty(request.Login))
                        AddValidation("Login");

                    if (Details.Count > 0)
                        return BadRequest(new ResponseResult()
                        {
                            success = false,
                            data = null,
                            errorMessage = ToStringDetails()
                        });

                    #endregion

                    request.Senha = HelperService.GeraSenha();
                    request.Ativo = true;

                    _repositoryUsuario.Save(request);
                    transaction.Commit();

                    return Ok(new ResponseResult()
                    {
                        success = true,
                        data = null
                    });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, new ResponseResult()
                    {
                        success = false,
                        data = ex.StackTrace,
                        errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                    });
                }
            }
        }

        [HttpDelete]
        [Route("{codigo}")]
        public IActionResult Delete(int codigo)
        {
            using (var transaction = _repositoryUsuario.BeginTransaction())
            {
                try
                {
                    var usuario = _repositoryUsuario.Get(x => x.Codigo.Equals(codigo)).FirstOrDefault();
                    if (usuario != null)
                    {
                        usuario.Ativo = false;
                        _repositoryUsuario.Save(usuario);
                        transaction.Commit();
                    }
                    else
                        return Ok(new ResponseResult()
                        {
                            success = false,
                            data = null,
                            errorMessage = "Registro não encontrado"
                        });

                    return Ok(new ResponseResult()
                    {
                        success = true,
                        data = null
                    });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return StatusCode(500, new ResponseResult()
                    {
                        success = false,
                        data = ex.StackTrace,
                        errorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                    });
                }
            }
        }
    }
}
