using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiniLink.Shared.Messages;
using MiniLinkLogic.Libraries.MiniLink.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VisitProcessor.VisitProcessingService
{
    class VisitProcessorBackgroundService: IConsumer<LinkEntryVisitMessage>
    {
        private readonly ILogger<VisitProcessorBackgroundService> _logger;

      

        private readonly ILinkEntryService _linkEntryService;

        public VisitProcessorBackgroundService(ILogger<VisitProcessorBackgroundService> logger,  ILinkEntryService linkEntryService)
        {
            _logger = logger;
           
            _linkEntryService = linkEntryService;
        }

        public async Task Consume(ConsumeContext<LinkEntryVisitMessage> context)
        {
            var data = context.Message;
            _logger.LogDebug($"Adding visit for record {data.Id}");
            await _linkEntryService.AddVisit(data.Id, data.Ip);

        }
    }

}
