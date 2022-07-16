Hello! Thanks for purchasing.

For using this package, you will need to:

1. Configure unity project to export for Oculus quest (Here's a good video on how to do it: https://www.youtube.com/watch?v=KLMf4yTCc0w)
2. Import Oculus Integration https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022
3. Import Photon Pun2 free version https://assetstore.unity.com/packages/tools/network/pun-2-free-119922
4. Import Text Mesh Pro (TMPro) from the unity package manager.
5. Go to https://www.photonengine.com/ and create an account, and create a new PUN app and you will get an appID. Copy it and paste it in the PhotonServerSettings config file in the project in App id Realtime field.
6. Add the 2 scenes to your build settings "Photon2Lobby" and "Photon2Room" in that order. You can build to your quest, unplug the device, and try the desktop version in your pc from the editor itself.
7. Enjoy :)

This template is a starting point for your project, it shows basic integration of Photon2 into Oculus integration for asymmetrical games, it is designed for oculus quest, but it is also compatible with oculus Rift/go.
I use mostly "RPC" for sending information and events to the network, so I strongly suggest that you read the documentation here: https://doc.photonengine.com/en-us/pun/v2/gameplay/rpcsandraiseevent
And of course, I also suggest reading through the the rest of Photon documentation here: https://doc.photonengine.com/en-us/pun/v2/getting-started/pun-intro

Troubleshooting.

- If when running the app it get stuck in "Connecting", check if your Oculus Quest have internet connection, and also check step 5, specially filling the appID in PhotonServerSettings.
- If you are getting connected, but can't see other players, try fixing your Region in PhotonServerSettings config file, you can find a list of regions in here: https://doc.photonengine.com/en-us/realtime/current/connection-and-authentication/regions

If you have any other trouble, don't hesitate to write: chiligamesco@gmail.com