Overview

This project implements a popup-based leaderboard in Unity.
Entries are read from Assets/Resources/Leaderboard.json.
The popup opens and closes through a global IPopupManagerService bound with Zenject.
Player avatars load asynchronously and are cached in memory.
The popup prefab is an Addressable and is injected before initialization.

How it works

Core

ObjectPool<T> provides pooled items. New items are created with Zenject. Items return to the pool by raising OnRemoved.

Popup

IPopupManagerService and PopupManagerService. The manager instantiates the popup, injects it with InjectGameObject, calls every IPopupInitialization.Init, tracks instances, and closes them when requested. PopupInstaller binds the service as a singleton.

Leaderboard

LeaderboardJsonProvider reads JSON, supports a plain array or an object with the leaderboard field, and sorts by score in descending order.
LeaderboardView shows the Open button. Leaderboard coordinates opening and closing. LeaderboardPopup creates and fills items. LeaderboardItemsPool provides item instances. LeaderboardItem and LeaderboardItemView render data.
IAvatarLoader and AvatarLoader download textures with UnityWebRequestTexture, cache sprites by URL, and support cancellation.
PlayerTypeStyleSet maps the string type to a color. The optional Y scale is present in code and can be set to 1.

Flow

The user clicks the Open button. Leaderboard hides the button and calls OpenPopup(address, info, parent).

The manager instantiates the popup as an Addressable, injects it, calls Init on components that implement IPopupInitialization, then shows the popup.

The popup loads JSON, gets items from the pool, initializes each item, shows a loading label for the avatar, starts an avatar request with a cancellation token, and updates the image when ready.

The Close button triggers cleanup, returns items to the pool, and calls OnClose. Leaderboard then calls ClosePopup(address) and shows the Open button again.

Setup

Mark the popup prefab with the Addressables address LeaderboardPopup.

Create Assets/Resources/Leaderboard.json. You can use an array or an object with the leaderboard field.

Add installers to the scene.

PopupInstaller binds IPopupManagerService as a singleton.

LeaderboardInstaller binds the view, the pool, the style set, the JSON provider, and the avatar loader. It also calls Initialize.

Install TextMesh Pro.

Design notes

Access to the popup manager is through a DI interface.

Addressables instances are injected with InjectGameObject.

JSON reading and avatar loading are asynchronous and cancellable.

Memory cache only for avatars.

Minimal number of components and straightforward responsibilities.