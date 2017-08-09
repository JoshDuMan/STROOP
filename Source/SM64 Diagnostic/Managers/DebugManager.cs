﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SM64_Diagnostic.Utilities;
using System.Windows.Forms;
using SM64_Diagnostic.Structs;
using SM64_Diagnostic.Structs.Configurations;

namespace SM64_Diagnostic.Managers
{
    public class DebugManager
    {
        CheckBox _spawnDebugCheckbox, _classicCheckbox, _resourceCheckbox, _stageSelectCheckbox, _freeMovementCheckbox;
        RadioButton[] _dbgSettingRadioButton;
        RadioButton _dbgSettingRadioButtonOff;

        public DebugManager(Control tabControl)
        {
            var panel = tabControl.Controls["NoTearFlowLayoutPanelDebugDisplayType"];

            _spawnDebugCheckbox = tabControl.Controls["checkBoxDbgSpawnDbg"] as CheckBox;
            _classicCheckbox = tabControl.Controls["checkBoxDbgClassicDbg"] as CheckBox;
            _resourceCheckbox = tabControl.Controls["checkBoxDbgResource"] as CheckBox;
            _stageSelectCheckbox = tabControl.Controls["checkBoxDbgStageSelect"] as CheckBox;
            _freeMovementCheckbox = tabControl.Controls["checkBoxDbgFreeMovement"] as CheckBox;

            _spawnDebugCheckbox.Click += SpawnDebugCheckbox_CheckedChanged;
            _classicCheckbox.Click += _classicCheckbox_CheckedChanged;
            _resourceCheckbox.Click += _resourceCheckbox_CheckedChanged;
            _stageSelectCheckbox.Click += _stageSelectCheckbox_CheckedChanged;
            _freeMovementCheckbox.Click += _freeMovementCheckbox_CheckedChanged;

            _dbgSettingRadioButtonOff = panel.Controls["radioButtonDbgOff"] as RadioButton;
            _dbgSettingRadioButton = new RadioButton[6];
            _dbgSettingRadioButton[0] = panel.Controls["radioButtonDbgObjCnt"] as RadioButton;
            _dbgSettingRadioButton[1] = panel.Controls["radioButtonDbgChkInfo"] as RadioButton;
            _dbgSettingRadioButton[2] = panel.Controls["radioButtonDbgMapInfo"] as RadioButton;
            _dbgSettingRadioButton[3] = panel.Controls["radioButtonDbgStgInfo"] as RadioButton;
            _dbgSettingRadioButton[4] = panel.Controls["radioButtonDbgFxInfo"] as RadioButton;
            _dbgSettingRadioButton[5] = panel.Controls["radioButtonDbgEnemyInfo"] as RadioButton;

            _dbgSettingRadioButtonOff.Click += radioButtonDbgOff_CheckedChanged;
            for (int i = 0; i < _dbgSettingRadioButton.Length; i++)
            {
                byte localI = (byte)i;
                _dbgSettingRadioButton[i].Click += (sender, e) => radioButtonDbgSetting_CheckChanged(sender, e, localI);
            }

        }

        private void radioButtonDbgSetting_CheckChanged(object sender, EventArgs e, byte settingValue)
        {
            // Turn debug on
            Config.Stream.SetValue((byte)1, Config.Debug.AdvancedModeAddress);

            // Set mode
            Config.Stream.SetValue(settingValue, Config.Debug.SettingAddress);
        }

        private void SpawnDebugCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Stream.SetValue(_spawnDebugCheckbox.Checked ? (byte)0x03 : (byte)0x00, Config.Debug.SettingAddress);
            Config.Stream.SetValue(_spawnDebugCheckbox.Checked ? (byte)0x01 : (byte)0x00, Config.Debug.SpawnModeAddress);
        }

        private void _classicCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Stream.SetValue(_classicCheckbox.Checked ? (byte)0x01 : (byte)0x00, Config.Debug.ClassicModeAddress);
        }

        private void _resourceCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Stream.SetValue(_resourceCheckbox.Checked ? (byte)0x01 : (byte)0x00, Config.Debug.ResourceModeAddress);
        }

        private void _stageSelectCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Stream.SetValue(_stageSelectCheckbox.Checked ? (byte)0x01 : (byte)0x00, Config.Debug.StageSelectAddress);
        }

        private void _freeMovementCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            Config.Stream.SetValue(_freeMovementCheckbox.Checked ? Config.Debug.FreeMovementOnValue : Config.Debug.FreeMovementOffValue, Config.Debug.FreeMovementAddress);
        }

        private void radioButtonDbgOff_CheckedChanged(object sender, EventArgs e)
        {
            // Turn debug off
            Config.Stream.SetValue((byte)0, Config.Debug.AdvancedModeAddress);

            // Set mode
            Config.Stream.SetValue((byte)0, Config.Debug.SettingAddress);
        }

        private void radioButtonDbgFxInfo_CheckedChanged(object sender, EventArgs e)
        {
            // Turn debug on
            Config.Stream.SetValue((byte)1, Config.Debug.AdvancedModeAddress);

            // Set mode
            Config.Stream.SetValue((byte)4, Config.Debug.SettingAddress);
        }

        private void radioButtonDbgEnemyInfo_CheckedChanged(object sender, EventArgs e)
        {
            // Turn debug on
            Config.Stream.SetValue((byte)1, Config.Debug.AdvancedModeAddress);

            // Set mode
            Config.Stream.SetValue((byte)5, Config.Debug.SettingAddress);
        }

        public void Update(bool updateView = false)
        {
            if (!updateView)
                return;

            _spawnDebugCheckbox.Checked = Config.Stream.GetByte(Config.Debug.SettingAddress) == 0x03
                 && Config.Stream.GetByte(Config.Debug.SpawnModeAddress) == 0x01;
            _classicCheckbox.Checked = Config.Stream.GetByte(Config.Debug.ClassicModeAddress) == 0x01;
            _resourceCheckbox.Checked = Config.Stream.GetByte(Config.Debug.ResourceModeAddress) == 0x01;
            _stageSelectCheckbox.Checked = Config.Stream.GetByte(Config.Debug.StageSelectAddress) == 0x01;
            _freeMovementCheckbox.Checked = Config.Stream.GetUInt16(Config.Debug.FreeMovementAddress) == Config.Debug.FreeMovementOnValue;

            var setting = Config.Stream.GetByte(Config.Debug.SettingAddress);
            var on = Config.Stream.GetByte(Config.Debug.AdvancedModeAddress);
            if (on == 0x01 && setting <= _dbgSettingRadioButton.Length)
                _dbgSettingRadioButton[setting].Checked = true;
            else
                _dbgSettingRadioButtonOff.Checked = true;
        }
    }
}
