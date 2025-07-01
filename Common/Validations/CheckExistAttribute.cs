using ApiLocadora.Services;
using System.ComponentModel.DataAnnotations;

namespace ApiLocadora.Common.Validations
{
    public class CheckExistAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ClienteService? service = validationContext.GetService(typeof(ClienteService)) as ClienteService;

            if(service == null)
            {
                return new ValidationResult(ErrorMessage = "Não possível consultar o cliente");
            }

            int id = (int)value;

            if (!service.Exist(id))
            {
                return new ValidationResult(ErrorMessage = "Cliente não encontrado");
            }

            return ValidationResult.Success;
        }
    }
}
