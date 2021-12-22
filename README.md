# DarkSoulsSimpleThirdPersonPerspectiveDemo

Unity version:  2020.3.24f1c1



DevLog 2021.12.22

**解决了移动时主角剧烈抖动的问题。**之前是主角移动（更新位置）写在Update里，在FixedUpdate里更新摄像机的位置。后修改成主角移动写在Update里，摄像机位置更新写在LateUpdate里（别用插值运算，否则还是有小幅抖动）。