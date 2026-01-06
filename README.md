# DotaSoundFix
A program for fixing broken and outdated sound files in Dota 2 mods.

<i>For automatic correction of audio codecs to work, Counter Strike 2 must be installed.</i>

## Buttons
<img src="https://github.com/Shiomari/DotaSoundFix/blob/master/FormScreen.png">

- `Select Folder` - To work, you need to select a folder containing the <b><i>sounds</i></b> and <b><i>soundevents</i></b> folders.
- `Fix Empty Sounds` - Replaces "0-byte files" with files containing empty sounds (prevents the game from crashing when sound timing is inaccurate). <b>Required for the following buttons to work!</b>
- `Check Not Including Sounds` - Outputs to a text file a list of sound files that are specified in <b><i>soundevents</i></b> but are not in the <b><i>sounds</i></b> folder.
- `Fix Not Including Sounds` - Substitutes empty sound files in place of those are specified in <b><i>soundevents</i></b> but are not in the <b><i>sounds</i></b> folder.
- `Set CS2 Path` <i>(Counter Strike 2 must be installed)</i> - Runs a program that converts <i>.wav</i> files to <i>.vsnd_c</i> files to set the path to Counter Strike 2.
- `Fix Sounds Codecs` <i>(Counter Strike 2 must be installed)</i>:
  - Converts <i>.vsnd_c</i> files to <i>.wav</i> files;
  - Converts <i>.wav</i> files with <b>ADPCM</b> codecs to <b>PCM</b> codecs;
  - Launches the program for converting <i>.wav</i> files to <i>.vsnd_c</i> files;
  - Opens the folder with the corrected <i>.wav</i> files;
  - Drag&Drop the corrected <i>.wav</i> files to the conversion program;
  - Wait for the conversion to complete (if the conversion is not completed, click `Fix Sounds Codecs` button again);
  - Replaces the <i>.vsnd_c</i> files with new ones.
- `Get Fixed Wav Files`:
  - Converts <i>.vsnd_c</i> files to <i>.wav</i> files;
  - Converts <i>.wav</i> files with <b>ADPCM</b> codecs to <b>PCM</b> codecs;
  - Opens the folder with the corrected <i>.wav</i> files.
- `Check Old VO Sounds Script` - Displays a list of obsolete <i>.vsndevts_c</i> scripts.
- `Create Fixed Vsndevts` - Creates a new <i>.vsndevts</i> scripts based on the deprecated <i>.vsndevts_c</i> scripts. The new <i>.vsndevts</i> script needs to be manually coded in <b>`Dota 2 - Tools`</b>.

## Used
- [InputSimulator](https://www.nuget.org/packages/InputSimulator/1.0.4)
- [Interop.UIAutomationClient](https://github.com/FlaUI/UIAutomation-Interop)
- [NAudio](https://github.com/naudio/NAudio)
- [ValveResourceFormat](https://github.com/ValveResourceFormat/ValveResourceFormat)
- [cs2_sound_converter](https://github.com/quad-damage/cs2_sound_converter)
- [Dota 2](https://store.steampowered.com/app/570/Dota_2/) <i>.vsndevts_c</i> files
