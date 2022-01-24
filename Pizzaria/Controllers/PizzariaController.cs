using Application.Exception;
using Application.Interface;
using Application.Model.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pizzaria.Controllers
{
   [ApiController]
   [Route("[controller]")]
   public class PizzariaController : ControllerBase
   {
      private readonly IPedidoService _pedidoService;      
      private readonly ILogger<PizzariaController> _logger;
      private readonly string _codigoClienteNaoInformado = "Codigo do cliente é obrigatório";
      private readonly string _nomeMetodo = "Pizzaria controller, Get";
      private readonly string _content = "application/json";
      private readonly string _nomeMetodoPost = "Pizzaria controller, Post";
      private readonly string _modelNull = "modelNull";
      private readonly string _modelObrigatorio = "objeto obrigatório";

      /// <summary>
      /// Realiza injeção de dependência
      /// </summary>
      /// <param name="logger"></param>
      /// <param name="pedidoService"></param>
      public PizzariaController(ILogger<PizzariaController> logger, IPedidoService pedidoService)
      {
         _logger = logger;
         _pedidoService = pedidoService;
      }

      /// <summary>
      /// Retorna pedidos de um cliente
      /// </summary>
      /// <param name="page"></param>
      /// <param name="pageSize"></param>
      /// <param name="codigoCliente"></param>
      /// <returns></returns>
      [HttpGet]
      public ActionResult Get(int page = 0, int pageSize = 20, string codigoCliente="")
      {
         _logger.Log(LogLevel.Error, _nomeMetodo);

         try
         {
            if (string.IsNullOrEmpty(codigoCliente))
            {
               ModelState.AddModelError(nameof(codigoCliente), _codigoClienteNaoInformado);
            }

            if (ModelState.IsValid)
            {
               var result = _pedidoService.Get(codigoCliente, page, pageSize);

               return new JsonResult(result)
               {
                  StatusCode = result != null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                  ContentType = _content,
                  Value = result,
               };
            }

            foreach (var item in ModelState.Values)
            {
               foreach (var erro in item.Errors)
               {
                  _logger.LogError($"{DateTime.UtcNow}, {erro.ErrorMessage}");
               }
            }

            return BadRequest(ModelState);                    
         }
         catch (Exception e)
         {
            _logger.LogError($"{DateTime.UtcNow}, { e.Message }", e);
            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
         }
      }

      /// <summary>
      /// Registra um novo pedido
      /// </summary>
      /// <param name="pedidoRequest"></param>
      /// <returns></returns>
      [Route("")]
      [HttpPost]
      public async Task<ActionResult> Post(PedidoRequest model)
      {
         _logger.LogInformation($"{_nomeMetodoPost} { System.Text.Json.JsonSerializer.Serialize<PedidoRequest>(model) }");


         if (model != null)
         {
            try
            {
               if (ModelState.IsValid)
               {
                  _logger.LogInformation($"ip requisição: { HttpContext?.Connection?.RemoteIpAddress.ToString() }");
                  
                  var result = await _pedidoService.Adicionar(model);
                  return new JsonResult(result)
                  {
                     StatusCode = result != null ? (int)HttpStatusCode.OK : (int)HttpStatusCode.BadRequest,
                     ContentType = _content,
                     Value = result
                  };
               }
               else
               {
                  foreach (var item in ModelState.Values)
                  {
                     foreach (var erro in item.Errors)
                     {
                        _logger.LogError($"{DateTime.UtcNow}, Erro: {erro.ErrorMessage}");
                     }
                  }
                  return BadRequest(ModelState);
               }
            }            
            catch (BusinessException be)
            {
               _logger.LogError($"{DateTime.UtcNow}, { be.Message }", be);
               return BadRequest(be.Message);
            }
            catch (Exception e)
            {
               _logger.LogError($"{DateTime.UtcNow}, { e.Message }", e);
               return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
         }

         ModelState.AddModelError(_modelNull, _modelObrigatorio);
         return new StatusCodeResult((int)HttpStatusCode.BadRequest);         
      }
   }
}
