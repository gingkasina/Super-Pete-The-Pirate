﻿using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Super_Pete_The_Pirate.Managers
{
    public static class SoundManager
    {
        //--------------------------------------------------
        // Content Manager

        private static Dictionary<string, SoundEffect> _seCache = new Dictionary<string, SoundEffect>();
        private static ContentManager _contentManager = new ContentManager(SceneManager.Instance.Content.ServiceProvider, "Content");

        //--------------------------------------------------
        // SEs

        private static SoundEffect _cancelSe;
        private static SoundEffect _confirmSe;
        private static SoundEffect _selectSe;

        //----------------------//------------------------//

        public static void Initialize()
        {
            _cancelSe = LoadSe("Cancel");
            _confirmSe = LoadSe("Confirm");
            _selectSe = LoadSe("Select");
        }

        public static Song LoadBgm(string filename)
        {
            return _contentManager.Load<Song>("sounds/bgm/" + filename);
        }

        public static SoundEffect LoadSe(string filename)
        {
            if (!_seCache.ContainsKey(filename))
            {
                try
                {
                    _seCache[filename] = _contentManager.Load<SoundEffect>("sounds/se/" + filename);
                }
                catch (Exception ex)
                {
                    _seCache[filename] = null;
                    Debug.WriteLine(ex.ToString());
                }
            }
            return _seCache[filename];
        }

        public static void PlaySafe(this SoundEffect se)
        {
            if (se != null)
                se.Play();
        }

        public static void PlayCancelSe()
        {
            _cancelSe.PlaySafe();
        }

        public static void PlayConfirmSe()
        {
            _confirmSe.PlaySafe();
        }

        public static void PlaySelectSe()
        {
            _selectSe.PlaySafe();
        }
    }
}
