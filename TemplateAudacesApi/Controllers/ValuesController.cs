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
using Vestillo.Business.Models;
using Vestillo.Business.Service;
using Dapper;
using MySql;
using Microsoft.Extensions.Configuration;

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
            Vestillo.Connection.ProviderFactory.StringConnection = _config.GetConnectionString("ExemplosDapper");
            Vestillo.Lib.Funcoes.SetIdEmpresaLogada=1;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IEnumerable<Object> Query([FromQuery] string uid
            , [FromQuery] string reference
            , [FromQuery] string type
            , [FromQuery] string product_group
            , [FromQuery] string supplier
            , [FromQuery] string description
            , [FromQuery] string collection)
        {
            List<Object> items = new List<Object>();
                                
            //busca so pelo uid
            if (!string.IsNullOrEmpty(uid) && string.IsNullOrEmpty(type))
            {
              var produto =  ProdutoServices.GetByReferencia(uid);
                if (produto != null )
                    items.Add(produto);
            }

            //busca pelo grupo
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(type) &&  type.Equals("group"))
            {
               
                var produto = ProdutoServices.GetProdutosByGrupo(uid);
                if (produto != null )
                    items.Add(produto);
            }

            //busca pela referencia
            if (!string.IsNullOrEmpty(reference) && string.IsNullOrEmpty(type))
            {
                var produto = ProdutoServices.GetByReferencia(reference);
                if (produto != null)
                    items.Add(produto);
            }

            //busca so pelo produto acabado
            if ( !string.IsNullOrEmpty(type) && type.Equals("finished_product") )
            {
                var lstProdutos = ProdutoServices.GetListPorFiltros(0, reference, description, collection);
                if (lstProdutos != null)
                    items.AddRange(lstProdutos);

            }

            //busca so pelo material
            if ( !string.IsNullOrEmpty(type) && type.Equals("raw_material"))
            {
                var lstProdutos = ProdutoServices.GetListMaterialPorFiltros(1, reference, description, product_group, supplier);
                if (lstProdutos != null && lstProdutos.Any())
                    items.AddRange(lstProdutos);
            }

            /*if (type == "raw_material")
            {
               
                items.Add(new Material
                {
                    type = "raw_material",
                    uid = "BTN-ID1",
                    name = "Button 1",
                    reference = "BTN1",
                    description = "Small button",
                    value =  10.49,
                    measure_unit = "UN",
                    product_group = "Buttons",
                    supplier = "ACME INC",
                    notes = "A small plastic button"
                });
            }
            else if (type == "finished_product")
            {
                items.Add(new Garment
                {
                    type         = "finished_product",
                    uid          = "039292",
                    name         = "POLO_SHIRT 3",
                    reference    = "039292",
                    description  = "MENS POLO SHIRT",
                    value        = 15,
                    product_group= "Shirts",
                    collection   = "HIGHSTIL SUMMER 2017",
                    notes        = "Lyle & Scott 2",
                });
            }
            else if (type == "activity")
            {
                items.Add(new Activity
                {
                    type        = "activity",
                    uid         = "ACT001",
                    name        = "Wash",
                    reference   = "LV001",
                    description = "Wash the whole fabric",
                    value       = 0.5,
                    measure_unit= "min",
                    time        = 30,
                    notes       = "Repeated two times",
                    sector      = "SCT1",
                    machine     = "MC1"
                });
            }
            else if (type == "generic")
            {
                items.Add(new Generic
                {
                    type       = "generic",
                    uid        = "CLN001",
                    reference  = "CLNABC",
                    name       = "cliente ABC",
                    endereco   = "Rua João Paulo 400",
                    telefone   = "011 987654321",
                    description= "cliente de São Paulo"
                });
            }
            else if (type == "measure")
            {
                items.Add(new Measure
                {
                    type      = "measure",
                    uid       = "MSR001",
                    name      = "gola",
                    reference = "MSRGola",
                    notes     = "Testeeeee",
                    value     = "1.5",
                    measure_unit = "mm",
                    last_modified = "2018-09-07T12:31Z",
                    values = new MeasureValues
                    {
                        P = 1.2,
                        M = 1.5,
                        G = 1.8,
                        order = "P;M;G"
                    }
                });
            }*/
            if (!items.Any())
                return null;

            return items;
          
        }

       

        


        [HttpPost]
        [Route("v1/garment")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public StatusRetorno Garment([FromBody] Garment value)
        {
            StatusRetorno ret = null;

            try
            {
               ValidarProdutoParaGravacao(value);
              var FichaTecnicaMaterial = FichaTecnicaService.Incluir(value);
                ret = new StatusRetorno()
                {
                    uid = FichaTecnicaMaterial.ProdutoId.ToString(),
                    status = "Ok",
                    message = "Produto Incluido|Ficha:" + FichaTecnicaMaterial.Id + ",  incluida com sucesso."
                };
                return ret;
            }
            catch (Vestillo.Lib.VestilloException ex)
            {
                ret = new StatusRetorno()
                {                  
                    status = "bad request",
                    message = ex.Mensagem
                };
                return ret;
            }
           
        }

        private void ValidarProdutoParaGravacao(Garment value)
        {
            StringBuilder erro = new StringBuilder();
           
                if (string.IsNullOrEmpty(value.description))
                    erro.AppendLine("Campo 'description' está em branco.");

                if (string.IsNullOrEmpty(value.reference))
                    erro.AppendLine("Campo 'reference' está em branco.");

                if (value.value.Equals(0))
                    erro.AppendLine("Campo 'value' está em branco.");

                if (string.IsNullOrEmpty(value.name))
                    erro.AppendLine("Campo 'name' está em branco.");

                 if  (value.variants == null || !value.variants.Any())
                     erro.AppendLine("Ficha Técnicas sem itens");


            if (erro.Length > 0)
                    throw new Exception(erro.ToString());
          
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
