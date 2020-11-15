using AutoFixture;
using NSubstitute;
using OnlineVideoLinks;
using OnlineVideoLinks.Models;
using OnlineVideoLinks.Utilities;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unbroken.LaunchBox.Plugins.Data;
using Xunit;

namespace LaunchboxPluginsTests.OnlineVideoLinks
{
    public class VideoSelectorFormTests
    {
        Fixture _fixture = new Fixture();
        IGameVideoUtility _gameVideoUtilitiesMock = Substitute.For<IGameVideoUtility>();
        IGamepadXinputProvider _gamepadXinputProviderMock = Substitute.For<IGamepadXinputProvider>();

        private bool _isPlaying = false;

        public VideoSelectorFormTests()
        {
            _gameVideoUtilitiesMock.When(x => x.Play(Arg.Any<GameVideo>()))
                .Do(x => _isPlaying = true);

            _gameVideoUtilitiesMock.When(x => x.StopPlaying())
                .Do(x => _isPlaying = false);

            _gameVideoUtilitiesMock.IsPlaying()
                .Returns(x =>
                {
                    return _isPlaying;
                });
        }

        [Theory]
        [InlineData(5, new GamepadButtonFlags[] { 
            GamepadButtonFlags.A 
            }, 0)]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadUp, GamepadButtonFlags.DPadUp,
            GamepadButtonFlags.A
            }, 0)]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown,
            GamepadButtonFlags.A
            }, 3)]
        [InlineData(2, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown,
            GamepadButtonFlags.A
            }, 1)]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadLeft, GamepadButtonFlags.X, GamepadButtonFlags.Y,
            GamepadButtonFlags.A
            }, 0)]
        public void Should_PlayVideo_AfterPressingButtons(int videoCount, GamepadButtonFlags[] buttons, int expectedVideoIndex)
        {
            var dummyGame = Substitute.For<IGame>();
            var dummyVideos = _fixture.CreateMany<GameVideo>(videoCount).ToArray();

            _gameVideoUtilitiesMock.GetGameVideos(dummyGame)
                .Returns(dummyVideos);

            var form = new VideoSelectorForm(dummyGame, _gameVideoUtilitiesMock, _gamepadXinputProviderMock);

            // Telling the IGamepadXinputProvider mock to raise events for the given buttons
            foreach (var button in buttons)
                _gamepadXinputProviderMock.ButtonPressed += Raise.EventWith(null, new XInputEventArgs(button));

            // Asserts
            _gamepadXinputProviderMock.Received(1).StartListening();
            _gameVideoUtilitiesMock.Received(1).Play(dummyVideos[expectedVideoIndex]);
        }

        [Theory]
        [InlineData(5, new GamepadButtonFlags[] {})]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadUp, GamepadButtonFlags.DPadUp
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown
            })]
        [InlineData(2, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadLeft, GamepadButtonFlags.X, GamepadButtonFlags.Y
            })]
        public void Should_NotPlayVideo_If_ButtonA_NotPressed(int videoCount, GamepadButtonFlags[] buttons)
        {
            var dummyGame = Substitute.For<IGame>();
            var dummyVideos = _fixture.CreateMany<GameVideo>(videoCount).ToArray();

            _gameVideoUtilitiesMock.GetGameVideos(dummyGame)
                .Returns(dummyVideos);

            var form = new VideoSelectorForm(dummyGame, _gameVideoUtilitiesMock, _gamepadXinputProviderMock);

            // Telling the IGamepadXinputProvider mock to raise events for the given buttons
            foreach (var button in buttons)
                _gamepadXinputProviderMock.ButtonPressed += Raise.EventWith(null, new XInputEventArgs(button));

            // Asserts
            _gamepadXinputProviderMock.Received(1).StartListening();
            _gameVideoUtilitiesMock.DidNotReceiveWithAnyArgs().Play(Arg.Any<GameVideo>());
        }

        [Theory]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.A, GamepadButtonFlags.B
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadUp, GamepadButtonFlags.DPadUp,
            GamepadButtonFlags.A, GamepadButtonFlags.B
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown,
            GamepadButtonFlags.A, GamepadButtonFlags.B
            })]
        [InlineData(2, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown,
            GamepadButtonFlags.A, GamepadButtonFlags.B
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadLeft, GamepadButtonFlags.X, GamepadButtonFlags.Y,
            GamepadButtonFlags.A, GamepadButtonFlags.B
            })]
        public void Should_PlayVideo_ThenStop(int videoCount, GamepadButtonFlags[] buttons)
        {
            var dummyGame = Substitute.For<IGame>();
            var dummyVideos = _fixture.CreateMany<GameVideo>(videoCount).ToArray();

            _gameVideoUtilitiesMock.GetGameVideos(dummyGame)
                .Returns(dummyVideos);

            var form = new VideoSelectorForm(dummyGame, _gameVideoUtilitiesMock, _gamepadXinputProviderMock);

            // Telling the IGamepadXinputProvider mock to raise events for the given buttons
            foreach (var button in buttons)
                _gamepadXinputProviderMock.ButtonPressed += Raise.EventWith(null, new XInputEventArgs(button));

            // Asserts
            _gamepadXinputProviderMock.Received(1).StartListening();
            _gameVideoUtilitiesMock.ReceivedWithAnyArgs(1).Play(null);
            _gameVideoUtilitiesMock.Received(1).StopPlaying();
        }

        [Theory]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.A, GamepadButtonFlags.A, GamepadButtonFlags.A
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown,
            GamepadButtonFlags.A, GamepadButtonFlags.A, GamepadButtonFlags.A
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.A, GamepadButtonFlags.DPadDown, GamepadButtonFlags.A
            })]
        public void Should_PlayVideo_OnlyOnce(int videoCount, GamepadButtonFlags[] buttons)
        {
            var dummyGame = Substitute.For<IGame>();
            var dummyVideos = _fixture.CreateMany<GameVideo>(videoCount).ToArray();

            _gameVideoUtilitiesMock.GetGameVideos(dummyGame)
                .Returns(dummyVideos);

            var form = new VideoSelectorForm(dummyGame, _gameVideoUtilitiesMock, _gamepadXinputProviderMock);

            // Telling the IGamepadXinputProvider mock to raise events for the given buttons
            foreach (var button in buttons)
                _gamepadXinputProviderMock.ButtonPressed += Raise.EventWith(null, new XInputEventArgs(button));

            // Asserts
            _gamepadXinputProviderMock.Received(1).StartListening();
            _gameVideoUtilitiesMock.ReceivedWithAnyArgs(1).Play(null);
        }

        [Fact]
        public void Should_PlayVideo0_Then_PlayVideo3()
        {
            var dummyGame = Substitute.For<IGame>();
            var dummyVideos = _fixture.CreateMany<GameVideo>(5).ToArray();

            _gameVideoUtilitiesMock.GetGameVideos(dummyGame)
                .Returns(dummyVideos);

            var form = new VideoSelectorForm(dummyGame, _gameVideoUtilitiesMock, _gamepadXinputProviderMock);

            // Telling the IGamepadXinputProvider mock to raise events for the given buttons
            var buttons = new GamepadButtonFlags[]
            {
                GamepadButtonFlags.A, GamepadButtonFlags.B,
                GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown,
                GamepadButtonFlags.A
            };
            foreach (var button in buttons)
                _gamepadXinputProviderMock.ButtonPressed += Raise.EventWith(null, new XInputEventArgs(button));

            // Asserts
            _gamepadXinputProviderMock.Received(1).StartListening();
            _gameVideoUtilitiesMock.Received(1).Play(dummyVideos[0]);
            _gameVideoUtilitiesMock.Received(1).StopPlaying();
            _gameVideoUtilitiesMock.Received(1).Play(dummyVideos[3]);
        }

        [Theory]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.B
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown,
            GamepadButtonFlags.B
            })]
        [InlineData(2, new GamepadButtonFlags[] {
            GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown, GamepadButtonFlags.DPadDown,
            GamepadButtonFlags.A, GamepadButtonFlags.B, GamepadButtonFlags.B
            })]
        [InlineData(5, new GamepadButtonFlags[] {
            GamepadButtonFlags.A, GamepadButtonFlags.B,
            GamepadButtonFlags.A, GamepadButtonFlags.B, GamepadButtonFlags.B
            })]
        public void Should_CloseForm_On_ButtonB(int videoCount, GamepadButtonFlags[] buttons)
        {
            var dummyGame = Substitute.For<IGame>();
            var dummyVideos = _fixture.CreateMany<GameVideo>(videoCount).ToArray();

            _gameVideoUtilitiesMock.GetGameVideos(dummyGame)
                .Returns(dummyVideos);

            var form = new VideoSelectorForm(dummyGame, _gameVideoUtilitiesMock, _gamepadXinputProviderMock);

            // Telling the IGamepadXinputProvider mock to raise events for the given buttons
            foreach (var button in buttons)
                _gamepadXinputProviderMock.ButtonPressed += Raise.EventWith(null, new XInputEventArgs(button));

            // Asserts
            Assert.False(_gameVideoUtilitiesMock.IsPlaying());
            Assert.True(form.IsDisposed);
        }
    }
}
