# LaunchBox Plugins
Various plugins for the LaunchBox frontend for PC games and emulated console platforms.

## Installation details

To install any available plugins, download its corresponding DLL file from the latest [release](https://github.com/SsjCosty/LaunchboxPlugins/releases), and add it to the Plugins subfolder from your main LaunchBox installation folder.

## Online Video Links

This plugin allows you to add online videos to your game, which will appear on the game's menu both in LaunchBox and BigBox. Mainly, it supports YouTube and Steam videos. The videos get played by LaunchBox's portable VLC distribution, so you don't have to worry about having the right video player installed.

#### LaunchBox Interface

There is a new **menu item** on the game's menu in LaunchBox, allowing you to manage your game's video links, which can be seen in the following screenshot, along with two sample video links. Once you open the video manager dialog, follow the on-screen instructions (and click "More info") to add, edit or remove your video links.

![LaunchBox Online Video Links interface](https://i.imgur.com/dININZs.png)

#### BigBox Interface

In BigBox, the video links can be played from the game's "Additional Apps/Versions" menu, as exemplified here:

![BigBox video links interface](https://i.imgur.com/9LRo3Op.png)

## Bulk Genre Editor

Currently, LaunchBox allows you to edit genres for games, however if use the default Bulk Edit Wizard to edit genres for multiple selected games then they will all be overwritten with the same exact set of genres. for 

This plugin allows you to add or remove genres to multiple games, while keeping each game's existing genres.

The new **menu item** is available for one or multiple selected games:

![Bulk Add/Edit Genres menu item](https://i.imgur.com/8ywzK9h.png)

The menu item opens the following dialog form, where you can select the genre(s) that you want to either add or remove from each game:

![Bulk Add/Edit Genres dialog form](https://i.imgur.com/LC1zj0G.png)

#### Example:

Let's say I have 3 games: Game 1, Game 2, and Game 3. To start, they have the following genres:
* Game 1 has genres: Action;
* Game 2 has genres: Role-Playing;
* Game 3 has genres: Action; Adventure;

After using this Bulk Genre Editor to add the genre "Beat 'em up", the result is this:
* Game 1 genres: Action; Beat 'em up;
* Game 2 genres: Action; Beat 'em up; Role-Playing;
* Game 3 genres: Action; Adventure; Beat 'em up;
