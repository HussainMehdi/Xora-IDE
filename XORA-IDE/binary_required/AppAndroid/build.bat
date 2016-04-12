@echo off
set ANDROID_HOME=D:\Android\android-sdk
set PATH=%PATH%;%ANDROID_HOME%\tools;%ANDROID_HOME%\platform-tools
call cordova build -d android
explorer platforms\android\build\outputs\apk


