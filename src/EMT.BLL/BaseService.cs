using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using EMT.Common;
using EMT.DAL.EF;

namespace EMT.BLL.Services
{
    public abstract class BaseService
    {
        protected readonly MyAppConfig _myAppConfig;
        protected readonly ILogger _logger;
        protected readonly ApplicationDbContext _context;

        public BaseService(IOptionsSnapshot<MyAppConfig> myAppConfig, ILogger logger, ApplicationDbContext context)
        {
            _myAppConfig = myAppConfig.Value;
            _logger = logger;
            _context = context;
            _logger.LogDebug($"*** SERVICE Constructor of {this.GetType()} at {DateTime.Now}");
        }
    }
}
