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
        [TestCase("sfx_sound_with_a_notorously_very_large_name")]
        public void PlaySound_WithValidString_PlaysSound(string sound) {
            _audioService.PlaySound(sound);
            _audioService.Received().PlaySound(sound);
        }

        [TestCase("invalid_name")]
        [TestCase("123")]
        [TestCase("|@#~€€¬ª!º$%&/()·")]
        public void PlaySound_WithInvalidString_ReturnsErrorLog(string sound) {
            _audioService.PlaySound(sound);
            _audioService.Received().PlaySound(sound);
        }

    }
}
