﻿

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmbyStat.Common.Exceptions;
using EmbyStat.Repositories.Config;
using EmbyStat.Repositories.EmbyPlugin;
using EmbyStat.Repositories.EmbyServerInfo;
using EmbyStat.Services.Emby.Models;
using EmbyStat.Services.EmbyClient;
using MediaBrowser.Model.Plugins;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EmbyStat.Services.Emby
{
    public class PluginService : IPluginService
    {
	    private readonly ILogger<PluginService> _logger;
	    private readonly IEmbyClient _embyClient;
	    private readonly IEmbyPluginRepository _embyPluginRepository;
	    private readonly IEmbyServerInfoRepository _embyServerInfoRepository;
		private readonly IConfigurationRepository _configurationRepository;

		public PluginService(ILogger<PluginService> logger, IEmbyClient embyClient, IEmbyPluginRepository embyPluginRepository, IConfigurationRepository configurationRepository, IEmbyServerInfoRepository embyServerInfoRepository)
	    {
		    _logger = logger;
		    _embyClient = embyClient;
		    _embyPluginRepository = embyPluginRepository;
		    _configurationRepository = configurationRepository;
		    _embyServerInfoRepository = embyServerInfoRepository;
	    }

	    public EmbyUdpBroadcast SearchEmby()
	    {
		    using (var client = new UdpClient())
		    {
			    var requestData = Encoding.ASCII.GetBytes("who is EmbyServer?");
			    var serverEp = new IPEndPoint(IPAddress.Any, 7359);

			    client.EnableBroadcast = true;
			    client.Send(requestData, requestData.Length, new IPEndPoint(IPAddress.Broadcast, 7359));

			    var timeToWait = TimeSpan.FromSeconds(2);

			    var asyncResult = client.BeginReceive(null, null);
			    asyncResult.AsyncWaitHandle.WaitOne(timeToWait);
			    if (asyncResult.IsCompleted)
			    {
				    try
				    {
					    var receivedData = client.EndReceive(asyncResult, ref serverEp);
					    var serverResponse = Encoding.ASCII.GetString(receivedData);
					    return JsonConvert.DeserializeObject<EmbyUdpBroadcast>(serverResponse);
				    }
				    catch (Exception)
				    {
					    // No data recieved, swallow exception and return empty object
				    }
			    }

			    return new EmbyUdpBroadcast();
		    }
	    }

	    public async Task<EmbyToken> GetEmbyToken(EmbyLogin login)
		{
		    if (!string.IsNullOrEmpty(login?.Password) && !string.IsNullOrEmpty(login.UserName))
		    {
				try
				{
					var token = await _embyClient.AuthenticateUserAsync(login.UserName, login.Password, login.Address);
					return new EmbyToken
					{
						Token = token.AccessToken,
						Username = token.User.ConnectUserName,
						IsAdmin = token.User.Policy.IsAdministrator
					};
				}
				catch (Exception e)
				{
					_logger.LogError("Username or password are wrong, user should try again with other credentials!");
					_logger.LogError($"Message: {e.Message}");
					throw new BusinessException("WRONG_USERNAME_OR_PASSWORD");
				}
			}
			
			_logger.LogError("Username or password are empty, no use to try a login!");
			throw new BusinessException("WRONG_USERNAME_OR_PASSWORD");
	    }

	    public List<PluginInfo> GetInstalledPlugins()
	    {
		    return _embyPluginRepository.GetPlugins();
	    }

	    public ServerInfo GetServerInfo()
	    {
		    return _embyServerInfoRepository.GetSingle();
	    }

	    public async void FireSmallSyncEmbyServerInfo()
	    {
		    var settings = _configurationRepository.GetSingle();

			_embyClient.SetAddressAndUrl(settings.EmbyServerAddress, settings.AccessToken);
		    var systemInfoReponse = await _embyClient.GetServerInfo();
			var pluginsResponse = await _embyClient.GetInstalledPluginsAsync();

		    var systemInfo = Mapper.Map<ServerInfo>(systemInfoReponse);

		    _embyServerInfoRepository.UpdateOrAdd(systemInfo);
			_embyPluginRepository.RemoveAllAndInsertPluginRange(pluginsResponse);
		}
	}
}
