# CourseManager

[![Build Status](https://travis-ci.org/raedion/CourseManager.svg?branch=master)](https://travis-ci.org/raedion/CourseManager)

## 概要
大阪大学大学院情報科学研究科の授業を履修するにあたって必要となる単位数の計算などの実行を行えるようにした。

## 実行環境
Visual Studioを実行環境として用意した。
この時に用いるのはLivetというプロジェクトのテンプレートで、C#とWPFをMVVMのデザインパターンで拡張している。

ここで、Livetなどの環境を整えていても実行がうまくいかないという場合は以下の設定を試してみてほしい。

1. Visual Studioで本プロジェクトを読み込む
2. メニューより [ツール] - [オプション] - [NuGetパッケージマネージャー] - [パッケージソース]の画面に移動
3. [パッケージソース]欄に何も存在しない場合は、右上のほうにある緑色の[+]ボタンを押して新規項目を作成
4. [名前]のところに "Nuget.org", [ソース]のところに "https://api.nuget.org/v3/index.json" を入力して最後に更新ボタンを押して登録

この操作を行うことでNuGetからパッケージを得ることができ、Livetの情報を同期してくれる
