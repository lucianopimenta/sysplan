using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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
    public class FilmeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Filme> _repositoryFilme;

        public FilmeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repositoryFilme = _unitOfWork.GetRepository<Filme>();
        }

        [HttpGet]
        [Route("{codigo}")]
        public IActionResult Get(int codigo)
        {
            try
            {
                var filme = _repositoryFilme.Get(x => x.Codigo.Equals(codigo)).FirstOrDefault();
                if (filme != null)
                {
                    return Ok(new ResponseResult()
                    {
                        success = true,
                        data = new JObject(
                            new JProperty("Codigo", filme.Codigo),
                            new JProperty("Catalogo", filme.Catalogo),
                            new JProperty("Nome", filme.Nome),
                            new JProperty("Ano", filme.Ano),
                            new JProperty("Imagem", filme.Imagem),
                            new JProperty("Slogan", filme.Slogan),
                            new JProperty("VisaoGeral", filme.VisaoGeral),
                            new JProperty("Classificacao", filme.Classificacao))
                    });
                }
                else
                    return Ok(new ResponseResult()
                    {
                        success = false,
                        data = null,
                        errorMessage = "Registro não encontrado"
                    });
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

        [HttpGet]
        public IActionResult Filtro(string nome)
        {
            try
            {
                JArray json = new JArray();

                if (nome == null)
                    nome = string.Empty;

                _repositoryFilme.Get(x => x.Nome.ToUpper().Contains(nome.ToUpper())).OrderBy(x => x.Nome).ToList().ForEach(filme =>
                {
                    JObject obj =
                       new JObject(
                           new JProperty("Codigo", filme.Codigo),
                            new JProperty("Catalogo", filme.Catalogo),
                            new JProperty("Nome", filme.Nome),
                            new JProperty("Ano", filme.Ano),
                            new JProperty("Imagem", filme.Imagem),
                            new JProperty("Slogan", filme.Slogan),
                            new JProperty("VisaoGeral", filme.VisaoGeral),
                            new JProperty("Classificacao", filme.Classificacao));

                    json.Add(obj);
                });

                return Ok(new ResponseResult()
                {
                    success = true,
                    data = json
                });
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
        public IActionResult Post([FromBody] Filme request)
        {
            using (var transaction = _repositoryFilme.BeginTransaction())
            {
                try
                {
                    #region Validações

                    if (string.IsNullOrEmpty(request.Nome))
                        AddValidation("Nome");

                    if (string.IsNullOrEmpty(request.Imagem))
                        AddValidation("Imagem");

                    if (string.IsNullOrEmpty(request.Slogan))
                        AddValidation("Slogan");

                    if (request.Classificacao == 0)
                        AddValidation("Classificação");

                    if (Details.Count > 0)
                        return BadRequest(new ResponseResult()
                        {
                            success = false,
                            data = null,
                            errorMessage = ToStringDetails()
                        });

                    #endregion

                    _repositoryFilme.Save(request);
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

        [HttpPut]
        public IActionResult Put([FromBody] Filme request)
        {
            using (var transaction = _repositoryFilme.BeginTransaction())
            {
                try
                {
                    #region Validações

                    if (request.Codigo == 0)
                        AddValidation("Código");

                    if (string.IsNullOrEmpty(request.Nome))
                        AddValidation("Nome");

                    if (string.IsNullOrEmpty(request.Imagem))
                        AddValidation("Imagem");

                    if (string.IsNullOrEmpty(request.Slogan))
                        AddValidation("Slogan");

                    if (request.Classificacao == 0)
                        AddValidation("Classificação");

                    if (Details.Count > 0)
                        return BadRequest(new ResponseResult()
                        {
                            success = false,
                            data = null,
                            errorMessage = ToStringDetails()
                        });

                    #endregion

                    var filme = _repositoryFilme.GetNoTracking(x => x.Codigo.Equals(request.Codigo)).FirstOrDefault();
                    if (filme != null)
                    {
                        _repositoryFilme.Save(request);
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

        [HttpDelete]
        [Route("{codigo}")]
        public IActionResult Delete(int codigo)
        {
            using (var transaction = _repositoryFilme.BeginTransaction())
            {
                try
                {
                    var filme = _repositoryFilme.Get(x => x.Codigo.Equals(codigo)).FirstOrDefault();
                    if (filme != null)
                    {
                        _repositoryFilme.Delete(filme);
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
