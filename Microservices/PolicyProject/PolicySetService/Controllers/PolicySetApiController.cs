using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupService;
using LoginService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PolicyService.Models;
using PolicySetService.Models;

namespace PolicySetService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PolicySetApiController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IPolicySetRepository _policySetRepository;
        private readonly IPolicyRepository _policyRepository;
        private readonly ILogger<PolicySetApiController> _logger;

        public PolicySetApiController(ILoginRepository loginRepository, IGroupRepository groupRepository,
            IPolicyRepository policyRepository, IPolicySetRepository policySetRepository,
            ILogger<PolicySetApiController> logger)
        {
            _loginRepository = loginRepository;
            _groupRepository = groupRepository;
            _policyRepository = policyRepository;
            _policySetRepository = policySetRepository;
            _logger = logger;
        }


    }
}
