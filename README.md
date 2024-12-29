# BlockX

BlockX is a wide-spectrum content blocker for [Resonite](https://resonite.com), inspired by [uBlock Origin](https://github.com/gorhill/uBlock).

It is made to prevent meshes, textures and any other kind of content from loading.

> WARNING: THIS CAN AND WILL BREAK MAPS, SYSTEMS AND AVATARS DEPENDING ON WHAT IS BLOCKED

> WARNING: THIS MOD ISN'T READY YET AND IS MISSING IMPORTANT FEATURES, DO NOT USE

What this mod can and cannot do:
- It can block specific individual assets (textures, meshes, documents, ect)
- It CANNOT block a user (use the builtin block function)
- It CANNOT block a whole avatar (use the builtin hide function)
- It CANNOT completely block an object or world (unless every. single. asset. is in the blocklist but this would lead to more issues)

The goal of this project is mainly to block advertisements and tracking in worlds and not to replace the builtin blocking function of Resonite or moderation reports.  
BlockX is not a policing tool, more like a sanity protector.  
If you have any issues with anybody, [report them to moderation](https://moderation.resonite.com), do not try to get their avatar, world or creations added to this project.

## How does it work?

### Blocklists

Blocklists for BlockX are made in the `txt` (plaintext) format and goes as follows:

```
# This is comment
# Asset signature to block:
d8f42c9ee9af31a2671f6f00773d8e2bc7a808596d195725b61e0d8e4b349e48
```

The default blocklist is located at: https://i.j4.lc/resonite/bl.txt and will be downloaded at startup and written to a file named `blockXBL.txt` in the `rml_mods` folder (or wherever the mod DLL is located) for caching.

### Blocking

Right now, the mod will hijack the asset downloading phase and completely prevent the asset from being downloaded.

## Installation

BlockX relies on [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) which you will have to install first.

You will then need to download the latest release, and put the DLL file into the `rml_mods` folder.

## Acknowledgements

- Raymond Hill for making uBlock Origin, serving as inspiration for this project
