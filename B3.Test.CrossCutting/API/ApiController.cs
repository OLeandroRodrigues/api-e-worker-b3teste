using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;


namespace B3.Test.CrossCutting.API
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        public ApiController()
        {

        }

        protected ObjectResult ResponseMessage(HttpStatusCode statusCode, object value)
        {
            return base.StatusCode((int)statusCode, value);
        }

        public bool IsEntityValid<TEntity1, TEntity2>(TEntity1 entityValidator, TEntity2 entity, out string[] erros)
        {
            dynamic _entityValidator = entityValidator;
            dynamic validation = _entityValidator.Validate(entity);

            var listErrors = new List<string>();
            foreach (var item in validation.Errors)
                listErrors.Add(item.ErrorMessage);

            erros = listErrors.ToArray();

            if (validation.Errors.Count > 0)
                return false;

            return true;
        }
    }
}
