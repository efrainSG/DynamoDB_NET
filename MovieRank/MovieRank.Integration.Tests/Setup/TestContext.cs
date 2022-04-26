using Docker.DotNet;
using Docker.DotNet.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MovieRank.Integration.Tests.Setup {
    public class TestContext : IAsyncLifetime {
        private const string UriPipeDocker = "npipe://./pipe/docker_engine";
        private const string ContainerImageUri = "amazon/dynamodb-local";
        private const string PortNum = "8000";
        private string containerId;

        private readonly DockerClient dockerClient;

        public TestContext() {
            this.dockerClient = new DockerClientConfiguration(new Uri(UriPipeDocker)).CreateClient();
        }

        public async Task InitializeAsync() {
            await PullImage();
            await StartContainer();
            await new TestDataSetup().CreateTable();
        }

        private async Task PullImage() {
            await dockerClient.Images.CreateImageAsync(new ImagesCreateParameters {
                FromImage = ContainerImageUri,
                Tag = "latest"
            },
            new AuthConfig(),
            new Progress<JSONMessage>());
        }

        private async Task StartContainer() {
            var response = await dockerClient.Containers.CreateContainerAsync(new CreateContainerParameters {
                Image = ContainerImageUri,
                ExposedPorts = new Dictionary<string, EmptyStruct>
                {
                    {
                        PortNum, default(EmptyStruct)
                    }
                },
                HostConfig = new HostConfig {
                    PortBindings = new Dictionary<string, IList<PortBinding>>
                    {
                        {PortNum, new List<PortBinding>{new PortBinding { HostPort = PortNum} } }
                    },
                    PublishAllPorts = true
                }
            });
            containerId = response.ID;

            await dockerClient.Containers.StartContainerAsync(containerId, null);
        }

        public async Task DisposeAsync() {
            if (containerId != null) {
                await dockerClient.Containers.KillContainerAsync(containerId, new ContainerKillParameters());
            }
        }
    }
}
