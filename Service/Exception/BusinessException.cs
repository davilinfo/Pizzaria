using System;

namespace Application.Exception
{
   public class BusinessException : SystemException
   {
      public BusinessException()
      {

      }

      public BusinessException(string message) : base(message)
      {

      }
   }   
}
