﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AllReady.Models;
using AllReady.ViewModels.Campaign;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AllReady.Features.Campaigns
{
    public class AuthorizedCampaignsQueryHandler : IAsyncRequestHandler<AuthorizedCampaignsQuery, List<CampaignViewModel>>
    {
        private readonly AllReadyContext _context;

        public AuthorizedCampaignsQueryHandler(AllReadyContext context)
        {
            _context = context;
        }

        public async Task<List<CampaignViewModel>> Handle(AuthorizedCampaignsQuery message)
        {
            return await _context.CampaignManagers.Where(c => c.UserId == message.UserId)
                                                  .Select(c => c.Campaign)
                                                  .Include(x => x.ManagingOrganization)
                                                  .Include(x => x.Events)
                                                  .Include(x => x.ParticipatingOrganizations)
                                                  .Where(c => !c.Locked && c.Published)
                                                  .Select(campaign => campaign.ToViewModel()).ToListAsync();
        }
    }
}
