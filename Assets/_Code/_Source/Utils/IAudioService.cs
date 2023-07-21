using UnityEngine;
using FMODUnity;

namespace VamVam.Source.Utils {

    public interface IAudioService {

        // * Methods * //    
        void PlayMusic(EventReference music);
        void PlayMusic3D(EventReference music, Vector3 sourcePos);

        void PlaySfx(EventReference sound);
        void PlaySfx3D(EventReference sound, Vector3 sourcePos);

        void PlayAmbience(EventReference ambience);
        void PlayAmbience3D(EventReference ambience, Vector3 sourcePos);

        void ChangeBusVolume(FMOD.Studio.Bus bus, float newVolume);
        float GetBusVolume(FMOD.Studio.Bus bus);

        void StopMusic();

        void StopMasterChannel();
        void StopMusicChannel();
        void StopAmbienceChannel();
        void StopGameSfxChannel();
        void StopUISfxChannel();

        EventReference GetSound(string soundName);

    }
}