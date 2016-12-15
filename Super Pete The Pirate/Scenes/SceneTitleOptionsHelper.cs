﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using Super_Pete_The_Pirate.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super_Pete_The_Pirate.Scenes
{
    class SceneTitleOptionsHelper
    {
        //--------------------------------------------------
        // Menu

        private Color _menuItemColor;
        private int _menuY;

        private int _index;
        public int Index => _index;

        //--------------------------------------------------
        // Options

        private string[] _menuOptions;

        private const int BGM_Volume = 0;
        private const int SE_Volume = 1;
        private const int Display = 2;
        private const int Back = 3;

        //--------------------------------------------------
        // Active

        private bool _requestingExit;
        public bool RequestingExit => _requestingExit;

        //----------------------//------------------------//

        public SceneTitleOptionsHelper()
        {
            var viewportHeight = SceneManager.Instance.ViewportAdapter.VirtualHeight;
            
            _menuItemColor = new Color(68, 44, 45);

            _menuOptions = new string[]
            {
                "BGM Volume",
                "SE Volume",
                "Display",
                "Back"
            };
            _menuY = viewportHeight - (_menuOptions.Length * SceneManager.Instance.GameFont.LineHeight) - 7;
        }

        public void Activate()
        {
            _requestingExit = false;
        }

        public void Update(GameTime gameTime)
        {
            if (InputManager.Instace.Pressed(InputCommand.B))
                _requestingExit = true;

            if (InputManager.Instace.Pressed(InputCommand.Up))
            {
                _index = _index - 1 < 0 ? _menuOptions.Length - 1 : _index - 1;
                SoundManager.PlaySelectSe();
            }

            if (InputManager.Instace.Pressed(InputCommand.Down))
            {
                _index = _index + 1 > _menuOptions.Length - 1 ? 0 : _index + 1;
                SoundManager.PlaySelectSe();
            }

            if (InputManager.Instace.Pressed(InputCommand.Left))
                HandleLeft();

            if (InputManager.Instace.Pressed(InputCommand.Right))
                HandleRight();

            if (InputManager.Instace.Pressed(InputCommand.Confirm))
                HandleConfirm();
        }

        private void HandleLeft()
        {
            switch (_index)
            {
                case BGM_Volume:
                    SettingsManager.Instance.AddBGMVolume(-0.1f);
                    break;

                case SE_Volume:
                    SettingsManager.Instance.AddSEVolume(-0.1f);
                    break;

                case Display:
                    var windowedMode = SettingsManager.Instance.GameSettings.WindowedMode;
                    SettingsManager.Instance.SetWindowedMode(!windowedMode);
                    break;
            }
        }

        private void HandleRight()
        {
            switch (_index)
            {
                case BGM_Volume:
                    SettingsManager.Instance.AddBGMVolume(0.1f);
                    break;

                case SE_Volume:
                    SettingsManager.Instance.AddSEVolume(0.1f);
                    break;

                case Display:
                    var windowedMode = SettingsManager.Instance.GameSettings.WindowedMode;
                    SettingsManager.Instance.SetWindowedMode(!windowedMode);
                    break;
            }
        }

        private void HandleConfirm()
        {
            if (_index == Back)
                _requestingExit = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _menuOptions.Length; i++)
            {
                spriteBatch.DrawString(SceneManager.Instance.GameFont, _menuOptions[i],
                    new Vector2(25, _menuY + (i * SceneManager.Instance.GameFont.LineHeight)), _menuItemColor);

                spriteBatch.DrawString(SceneManager.Instance.GameFont, GetSettingsStringValue(i),
                    new Vector2(140, _menuY + (i * SceneManager.Instance.GameFont.LineHeight)), _menuItemColor);
            }
        }

        private string GetSettingsStringValue(int index)
        {
            switch (index)
            {
                case BGM_Volume:
                    var bgmVolume = SettingsManager.Instance.GameSettings.BGMVolume;
                    return Math.Ceiling(bgmVolume * 100).ToString() + "%";

                case SE_Volume:
                    var seVolume = SettingsManager.Instance.GameSettings.SEVolume;
                    return Math.Ceiling(seVolume * 100).ToString() + "%";

                case Display:
                    return SettingsManager.Instance.GameSettings.WindowedMode ? "Window" : "Fullscreen";
            }
            return "";
        }
    }
}
