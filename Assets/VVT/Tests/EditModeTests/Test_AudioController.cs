using NUnit.Framework;
using NSubstitute;

namespace VVT.EditorTests {

    internal sealed class Test_AudioController {

        private IAudioService _audioService;

        [SetUp]
        public void SetUp() {
            _audioService = Substitute.For<IAudioService>();
        }

        [TestCase("sfx_sound")]
        public void PlaySound_WithValidString_PlaysSound(string sound) {
            _audioService.PlaySound(sound);
            _audioService.Received().PlaySound(sound);
        }

    }
}
