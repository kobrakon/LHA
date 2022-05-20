using Comfort.Common;
using EFT;
using System.IO;
using System.Media;
using UnityEngine;

namespace LHA // LHA sounds like an alternative to LUA or something
{
    public class LHAController : MonoBehaviour // controller script, the brains of the mod that runs in Mono
    {
        private static SoundPlayer soundPlayer;
        private bool isPlaying = false; // I have to use this bool, turns out attempting to load and play audio every frame doesn't end well

        public void Update() // base unity mono method that runs every frame
        {
            var r = Ready(); // check if stuff is loaded
            var t = Trigger(); // check if threshold is met

            if (r == false)
                return;

            if (t && !isPlaying) // if threshold is met and alert isn't already plating
            {
                isPlaying = true;
                soundPlayer.PlayLooping(); // play alert
            } else // threshold not met?
            {
                isPlaying = false;
                soundPlayer.Stop(); // end alert
            }
        }

        bool Ready() // checks if script is ready to run without vomiting errors
        {
            var gameWorld = Singleton<GameWorld>.Instance; // checks if gameworld is instantiated, use singleton so that there's only one instance per scene
            var sessionResultPanel = Singleton<SessionResultPanel>.Instance; // checks result panel, good end point because it's instantiated immediately after death or extract
            if (gameWorld.AllPlayers == null || gameWorld.AllPlayers.Count <= 0 || gameWorld.AllPlayers[0] is HideoutPlayer || sessionResultPanel != null) // if important stuff isn't loaded
            {
                return false; // return false
            }
            // if important stuff is loaded
            return true; // return true
        }

        bool Trigger() // checks player health to decide whether to play the alert or not
        {
            var gameWorld = Singleton<GameWorld>.Instance; // gaemwordl

            var current = gameWorld.AllPlayers[0].HealthController.GetBodyPartHealth(EBodyPart.Common).Current; // get player's current total health

            if (current > 220) // if health is above 220
            {
                return false; // return false, do not allow for alert
            } else // if player is below 220
            {
                return true; // return true, allow for alert alert
            }
        }

        public static void LoadAudio(string fullpath) // self explainatory
        {
            soundPlayer = new SoundPlayer
            {
                SoundLocation = Directory.GetCurrentDirectory() + fullpath // fetch audio file
            };

            soundPlayer.Load(); // load audio file
        }
    }
}
