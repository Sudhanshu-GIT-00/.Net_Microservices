using System.ComponentModel.DataAnnotations;

namespace Mango.web.Utility
{
    public class AllowedExtensionsAttribute: ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedExtensionsAttribute(string[] extenstion)
        {
            _extensions = extenstion;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file != null)
            {
                var extension = Path.GetExtension(file.FileName); 
                if(!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult("This photo extension is not allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
