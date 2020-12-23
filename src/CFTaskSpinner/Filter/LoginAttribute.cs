using CFTaskSpinner.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFTaskSpinner.Filter
{
    public class LoginAttribute:ActionFilterAttribute
    {
        ILogger<LoginAttribute> _logger;
        ICloudFoundryServices _cfService;
        public LoginAttribute(ILogger<LoginAttribute> logger, ICloudFoundryServices cfService)
        {
            _logger = logger;
            _cfService = cfService;
        }
        public async override void OnActionExecuting(ActionExecutingContext context)
        {
            var accessToken = await _cfService.LoginAsync();
            base.OnActionExecuting(context);
        }

    }
}
