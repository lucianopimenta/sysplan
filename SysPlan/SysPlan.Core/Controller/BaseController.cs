using Microsoft.AspNetCore.Mvc;
using SysPlan.Core.Model;
using System.Collections.Generic;
using System.Text;

namespace SysPlan.Core.Controller
{
    public class BaseController : ControllerBase
    {
        protected List<ErrorDetail> Details { get; set; }

        public BaseController()
        {
            Details = new List<ErrorDetail>();
        }

        protected void AddValidation(string field, TypeValidator validator, string[] args = null)
        {
            switch (validator)
            {
                case TypeValidator.CampoObrigatorio:
                    Details.Add(new ErrorDetail() { message = string.Format("Campo {0}: obrigatório.", field) });
                    break;
                case TypeValidator.CampoTamanho:
                    Details.Add(new ErrorDetail() { message = string.Format("Campo {0}: tamanho máximo: {1} caracteres.", field, args[0]) });
                    break;
                case TypeValidator.CampoTamanhoIntervalo:
                    Details.Add(new ErrorDetail() { message = string.Format("Campo {0}: valor inválido (mínimo: {1}, máximo: {2}).", field, args[0], args[1]) });
                    break;
                case TypeValidator.CampoTamanhoUnico:
                    Details.Add(new ErrorDetail() { message = string.Format("Campo {0}: tamanho obrigatório de {1} caracteres.", field, args[0]) });
                    break;
                case TypeValidator.CampoValorInvalido:
                    Details.Add(new ErrorDetail() { message = string.Format("Campo {0}: valor inválido.", field) });
                    break;
                case TypeValidator.CampoNaoExisteBanco:
                    Details.Add(new ErrorDetail() { message = string.Format("O campo {0} não tem valor relacionado na tabela {1}", field, args[0]) });
                    break;
                case TypeValidator.CampoDuplicado:
                    Details.Add(new ErrorDetail() { message = string.Format("Já existe um {0} com o valor informado.", field) });
                    break;
                case TypeValidator.None:
                    Details.Add(new ErrorDetail() { message = args[0] });
                    break;
            }
        }

        protected void AddValidation(string field, string[] args = null)
        {
            AddValidation(field, TypeValidator.CampoObrigatorio, args);
        }

        protected string ToStringDetails()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in Details)
            {
                sb.AppendLine(item.message);
            }

            return sb.ToString();
        }
    }
}
