using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemplateAudacesApi.Models;
using TemplateAudacesApi.Utils;
using Vestillo.Business.Service;
using Microsoft.Extensions.Configuration;
using Vestillo.Business.Models;

namespace TemplateAudacesApi.Controllers
{
    [Route("audaces/idea/api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private Services.ProdutoServices _ProdutoServices;
        private Services.ProdutoServices ProdutoServices
        {
            get
            {
                if (_ProdutoServices == null)
                    _ProdutoServices = new Services.ProdutoServices();
                return _ProdutoServices;
            }
        }

        private Services.FichaTecnicaService _FichaTecnicaService;
        private Services.FichaTecnicaService FichaTecnicaService
        {
            get
            {
                if (_FichaTecnicaService == null)
                    _FichaTecnicaService = new Services.FichaTecnicaService();
                return _FichaTecnicaService;
            }
        }


        private Services.UsuarioService _UsuarioServices;
        private Services.UsuarioService UsuarioService
        {
            get
            {
                if (_UsuarioServices == null)
                    _UsuarioServices = new Services.UsuarioService();
                return _UsuarioServices;
            }
        }
        public ValuesController(IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _config = configuration;
            SetarPadroes();
        }

        private void SetarPadroes()
        {
            Vestillo.Connection.ProviderFactory.StringConnection = _config.GetConnectionString("db");
            var valor = Vestillo.Connection.ProviderFactory.IsAPI;
            Vestillo.Lib.Funcoes.SetIdEmpresaLogada = Convert.ToInt32(_config.GetSection("parametros").GetSection("empresa").Value);
            Vestillo.Lib.Funcoes.UtilizaAPI = true;
        }

        [HttpPost]
        [Route("v1/user/login")]
        [Consumes("application/x-www-form-urlencoded")]
        public ActionResult<LoginRetorno> Login([FromForm] string username, [FromForm] string password)
        {
            if (username == null || password == null)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new LoginRetorno { error = "invalid_grant" };
            }

            var user = UsuarioService.RetornarUsuario(username, password);
            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return new LoginRetorno { error = "invalid_grant" };
            }

            var token = TokenService.GenerateToken(user);

            return new LoginRetorno
            {
                access_token = token,
                expires_in = 3600,
                token_type = "Bearer",

            };
        }

        [HttpGet, Route("version")]
        public ActionResult<string> Version()
        {
            return Ok("1.6");
        }

        [HttpGet, Route("v1/query")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
     public IEnumerable<Object> Query(                                                                                                                                                                                           
         [FromQuery] string? uid = null,                                                                                                                                                                                         
         [FromQuery] string? reference = null,                                                                                                                                                                                   
         [FromQuery] string? type = null,                                                                                                                                                                                        
         [FromQuery] string? product_group = null,                                                                                                                                                                               
         [FromQuery] string? supplier = null,                                                                                                                                                                                    
         [FromQuery] string? description = null,                                                                                                                                                                                 
         [FromQuery] string? collection = null) 
        {
            List<Object> items = new List<Object>();
            try
            {
                if (!string.IsNullOrWhiteSpace(type) &&  type.Equals("activity"))
                    throw new Exception("Rotina de Buscar por activity não implementada ainda!");

                
                if (!string.IsNullOrEmpty(uid) && string.IsNullOrEmpty(type))
                {
                    var produto = ProdutoServices.GetByReferencia(uid);

                    if (produto != null)
                        items.Add(produto);
                }

                //busca pelo grupo
                if (!string.IsNullOrEmpty(reference) && !string.IsNullOrEmpty(type) && type.Equals("group"))
                {

                    var lstGrupos = ProdutoServices.GetProdutosByGrupo(reference,description);
                    if (lstGrupos != null && lstGrupos.Any())
                        items.AddRange(lstGrupos);
                }

                //busca pela referencia
                if (!string.IsNullOrEmpty(reference) && string.IsNullOrEmpty(type))
                {
                    var lstProdutos = ProdutoServices.GetProdutosByGrupo(reference, description);
                    if (lstProdutos != null)
                        items.AddRange(lstProdutos);
                }

                //busca so pelo produto acabado
                if (!string.IsNullOrEmpty(type) && type.Equals("finished_product"))
                {
                    var lstProdutos = ProdutoServices.GetListPorFiltros(0, reference, description, collection);
                    if (lstProdutos != null)
                        items.AddRange(lstProdutos);

                }

                //busca so pelo material
                if (!string.IsNullOrEmpty(type) && type.Equals("raw_material"))
                {
                    var lstProdutos = ProdutoServices.GetListMaterialPorFiltros(1, reference, description, product_group, supplier);
                    if (lstProdutos != null && lstProdutos.Any())
                        items.AddRange(lstProdutos);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            if (!items.Any())
                return null;

            return items;

        }






        [HttpPost]
        [Route("v1/garment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public StatusRetorno Garment([FromBody] Garment value, [FromQuery] string uid)
        {
            StatusRetorno ret = null;

            try
            {
                ValidarProdutoParaGravacao(value, uid);
                var FichaTecnicaMaterial = FichaTecnicaService.Incluir(value);
                ret = new StatusRetorno()
                {
                    uid = FichaTecnicaMaterial.ProdutoId.ToString(),
                    status = (string.IsNullOrWhiteSpace(uid)) ? "created" : "updated",
                    message = "Produto Incluido|Ficha:" + FichaTecnicaMaterial.Id + ",  incluida com sucesso."
                };
                return ret;

            }
            catch (Exception ex)
            {
                ret = new StatusRetorno()
                {
                    status = "bad request",
                    message = ex.Message
                };
                return ret;
            }

        }

        private void ValidarProdutoParaGravacao(Garment value, string uid)
        {
            StringBuilder erro = new StringBuilder();

            if (string.IsNullOrEmpty(value.name))
                erro.AppendLine("Campo 'name' está em branco.");


            if (value.variants == null || !value.variants.Any())
                erro.AppendLine("Ficha Técnica sem itens");

            if (value.variants.Any(p => p.materials.Any(m => m.amount == 0)))
                erro.AppendLine("Algum dos itens está com a quantidade igual a zero!");


            if (string.IsNullOrEmpty(uid) && RetornarSejaExisteProdutoCadastradoNaInclusaoDaFicha(value))
                erro.AppendLine($"Produto: { value.name } ja cadastrado!");



            if (erro.Length > 0)
                throw new Exception(erro.ToString());

        }


        private bool RetornarSejaExisteProdutoCadastradoNaInclusaoDaFicha(Garment value)
        {
            string referencia = string.Empty;
            string descricao = string.Empty;
            var produtoService = new TemplateAudacesApi.Services.ProdutoServices();

            Produto produto = produtoService.RetornarProduto(value, ref referencia, ref descricao);

            return (produto != null && produto.Id > 0);
        }

        [HttpGet]
        [Route("v1/picture")]
        [Produces("application/x-binary")]
        public ActionResult<Byte[]> Picture([FromQuery] string uid)
        {
            Image image = new Image(); //TODO: implementar codigo para pegar imagem pelo uid
            if (image != null)
            {
                var filePath = image.get_image_name();
                var bytes = System.IO.File.ReadAllBytes(filePath);

                return new FileContentResult(bytes, "application/x-binary")
                {
                    FileDownloadName = image.get_image_name()
                };
            }

            return null;
        }

        [HttpPost]
        [Route("v1/garment/picture")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<object> GarmentPicture([FromQuery] string garment_uid
            , [FromQuery] string description
            , [FromQuery] string picture_uid)
        {
            StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
            var ms = new MemoryStream();
            reader.BaseStream.CopyTo(ms);

            return new StatusRetorno { uid = "7678", status = "ok", message = "a message describing the response of the request" };
        }
    }
}
