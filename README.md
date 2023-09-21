# Tosho_Kidsmall_Manager
東金商業高等学校　文化祭キッズモール用受付管理ソフトウェア

## 使い方
1. Tosho_Kidsmall_Manager.exeを起動します
2. 出し物データ(*.json)を選択し、起動します。
3. Step1→2→3の順に操作します。

## 出し物データの作成方法
作成方法には２種類存在します。
1. DashimonoData_Editorを使用する方法
2. jsonファイルを手打ちで作成する方法

基本的には1の方法で作成することをおすすめします。  
何らかの理由により、2の方法で作成する場合は、セクション2を参考に作成してください。

## 1.DashimonoData_Editorを使用して作成するパターン

[こちらのリポジトリのREADME.mdを参照してください](https://github.com/ToganeShogyo-IPC/DashimonoData_Editor)

## 2.出し物データをjsonファイル手打ちで作成する
> ファイル例
```json
[
  {"Class":"1A","Name":"お化け屋敷","OKNinzu":0},
  {"Class":"1B","Name":"お化け屋敷2","OKNinzu":0},
]
```
|キー|内容|データ型|
|-|-|-|
|Class|出し物をするクラス(同名が重複する場合は数字を付与してください)|String型|
|Name|出し物の名前|String型|
|OKNinzu|受け入れられる人数の上限|Int型|

> **Warning**  
> Jsonの形式は一次元配列、ルート要素が配列、その子要素としてディクショナリ型です。  
> データ型は厳守して作成してください。
