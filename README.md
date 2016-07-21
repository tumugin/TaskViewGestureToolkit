# TaskViewGestureToolkit
Windows10のタスクビューをタッチパッドを使いMac OS XのMission Control風に操作出来るようにするアプリケーション。

## 対応ドライバー
* Synaptics製タッチパッド(3本指以上でのタッチ対応のもの)
* Wacom製ペンタブレット(Wacom feel™ Multi-Touch対応のもので4本指以上のタッチ対応のもの)  
<http://wdnet.jp/library/feelmulti-touch/wacomfeelmulti-touchfaqi#1>

Thinkpad X1 Carbon 4th genの内蔵タッチパッドとWacom Intuos pro Mにて動作確認しました。

## 使い方
プログラムを起動するとpluginディレクトリ以下にある各ドライバーに対応したプラグインが読み込まれ初期化されます。  
(対応していないプラグインは自動的に除外され読み込まれません)  
Synapticsタッチパッドでは3本指での操作、Wacom製ペンタブレットでは4本指での操作で仮想デスクトップの切り替えが出来ます。  
※ドライバー側で何らかの操作を割り当てている場合は外してください
