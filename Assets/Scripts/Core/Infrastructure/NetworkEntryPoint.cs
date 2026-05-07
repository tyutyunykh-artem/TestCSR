using Core.Network.Client;
using Core.Network.Demo;
using Core.Network.Hosting;
using Mirror;
using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure
{
    /// <summary>
    /// Точка входа.
    /// </summary>
    public class NetworkEntryPoint : IStartable, IDisposable
    {
        [Inject] private readonly ServerMessageHost _serverMessageHost;
        [Inject] private readonly HelloMessageDemo _helloMessageDemo;
        [Inject] private readonly HelloMessageHandler _helloMessageHandler;

        private readonly CompositeDisposable _disposables = new();

        public void Start()
        {
            Observable.EveryUpdate()
                .Where(_ => (NetworkServer.active || NetworkClient.isConnected) && NetworkManager.singleton != null)
                .Take(1)
                .Subscribe(_ =>
                {
                    InitializeNetworkSystems();
                })
                .AddTo(_disposables);
        }

        private void InitializeNetworkSystems()
        {
            Debug.Log("[NetworkEntryPoint.InitializeNetworkSystems] Initializing network systems");

            if (NetworkServer.active)
            {
                _serverMessageHost.Initialize();
                _helloMessageDemo.Start();
            }

            if (NetworkClient.isConnected)
            {
                _helloMessageHandler.Initialize();
            }

            Debug.Log("[NetworkEntryPoint.InitializeNetworkSystems] Network systems initialized");
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
