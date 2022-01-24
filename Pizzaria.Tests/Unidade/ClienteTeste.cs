using Domain.EF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Pizzaria.Tests.Unidade
{
   [TestClass]
   public class ClienteTeste
   {
      [TestMethod]
      public void DeveRetornarCliente()
      {
         var cliente = Mock.Of<Cliente>();

         cliente.Endereco = "endereço teste";
         cliente.Nome = "cliente";
         cliente.Telefone = "1234-5678";
         cliente.Codigo = cliente.GenerateClienteHash();
         cliente.Timestamp = System.DateTime.MinValue;

         Assert.IsTrue(cliente.Codigo == cliente.GenerateClienteHash());
      }      
   }
}
