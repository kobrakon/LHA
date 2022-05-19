using Comfort.Common;
using EFT;
using System.Collections;
using System.IO;
using System.Media;
using UnityEngine;

namespace LHA
{
    public class LHAController : MonoBehaviour
    {
        private SoundPlayer soundPlayer;
        private bool isLoaded = false;
        private bool trigger = false;
        private SessionResultPanel sessionResultPanel;

        public void Update()
        {
            var gameWorld = Singleton<GameWorld>.Instance;
            if (gameWorld.AllPlayers == null)
                return;

            if (gameWorld.AllPlayers.Count <= 0)
                return;
            

            if (gameWorld.AllPlayers[0] is HideoutPlayer)
                return;

            sessionResultPanel = Singleton<SessionResultPanel>.Instance;
            if (sessionResultPanel != null)
                return;

            if (!isLoaded)
                LoadAudio();

            var current = gameWorld.AllPlayers[0].HealthController.GetBodyPartHealth(EBodyPart.Common).Current;

            if (current > 220)
            {
                if (!trigger)
                    return;

                trigger = false;
            }
            else
                trigger = true;
        }

        public IEnumerator Start()
        {
            StartCoroutine(Check());
            yield break;
        }

        private IEnumerator Check()
        {
            yield return new WaitUntil(() => trigger == true);
            OnEvent();
            yield return new WaitUntil(() => trigger == false);
            OnEventEndOrNull();
            StartCoroutine(Check());
            yield break;
        }

        private void OnEvent()
        {
            if (!isLoaded)
                LoadAudio();

            soundPlayer.PlayLooping();
        }

        private void OnEventEndOrNull()
        {
            if (!isLoaded)
                LoadAudio();

            soundPlayer.Stop();
        }

        private void LoadAudio()
        {
            if (isLoaded)
                return;

            var fullPath = Directory.GetCurrentDirectory() + "\\BepInEx\\plugins\\LHA\\alert.wav";

            soundPlayer = new SoundPlayer
            {
                SoundLocation = fullPath
            };

            soundPlayer.Load();
            isLoaded = true;
        }
    }
}
