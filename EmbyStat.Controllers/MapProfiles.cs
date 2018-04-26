﻿using System;
using AutoMapper;
using EmbyStat.Api.EmbyClient.Model;
using EmbyStat.Common.Models;
using EmbyStat.Common.Tasks;
using EmbyStat.Controllers.ViewModels.Configuration;
using EmbyStat.Controllers.ViewModels.Emby;
using EmbyStat.Controllers.ViewModels.Movie;
using EmbyStat.Controllers.ViewModels.Server;
using EmbyStat.Controllers.ViewModels.Stat;
using EmbyStat.Controllers.ViewModels.Task;
using EmbyStat.Services.Models;
using EmbyStat.Services.Models.Stat;
using EmbyStat.Services.Models.Movie;
using EmbyStat.Services.Models.Emby;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.System;

namespace EmbyStat.Controllers.Helpers
{
    public class MapProfiles : Profile
    {
	    public MapProfiles()
	    { 
			//Controllers
		    CreateMap<Common.Models.Configuration, ConfigurationViewModel>().ReverseMap().ForMember(x => x.EmbyUserId, y => y.MapFrom(z => z.Id));
		    CreateMap<EmbyUdpBroadcast, EmbyUdpBroadcastViewModel>().ReverseMap();
		    CreateMap<EmbyLogin, EmbyLoginViewModel>().ReverseMap();
		    CreateMap<EmbyToken, EmbyTokenViewModel>().ReverseMap();
		    CreateMap<PluginInfo, EmbyPluginViewModel>().ReverseMap();
		    CreateMap<ServerInfo, ServerInfoViewModel>().ReverseMap();
		    CreateMap<Drives, DriveViewModel>()
				.ReverseMap()
			    .ForMember(x => x.Id, y => y.Ignore());

	        CreateMap<TaskInfo, TaskInfoViewModel>();
	        CreateMap<TaskResult, TaskResultViewModel>();
	        CreateMap<TaskTriggerInfo, TaskTriggerInfoViewModel>();
	        CreateMap<TimeSpanCard, TimeSpanCardViewModel>();
	        CreateMap<Card, CardViewModel>();
	        CreateMap<Poster, PosterViewModel>();
            CreateMap<MovieStats, MovieStatsViewModel>();
            CreateMap<Collection, CollectionViewModel>().ReverseMap();

			//EmbyResponses
			CreateMap<SystemInfo, ServerInfo>()
			    .ForMember(x => x.Id, y => Guid.NewGuid())
			    .ReverseMap()
			    .ForMember(x => x.CompletedInstallations, y => y.Ignore())
			    .ForMember(x => x.CompletedInstallations, y => y.Ignore())
			    .ForMember(x => x.FailedPluginAssemblies, y => y.Ignore());

		    CreateMap<Drive, Drives>()
			    .ForMember(x => x.Id, y => y.Ignore())
			    .ReverseMap();
	    }
    }
}