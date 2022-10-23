using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketManagement.UserApi.BusinessLogic.Validation;

namespace TicketManagement.UserApi.BusinessLogic.Services.Results
{
    public class ChangePasswordResult : Result
    {
        public ChangePasswordResult(ValidationResult validationResult)
            : base(validationResult)
        {
        }
    }
}