// See https://aka.ms/new-console-template for more information
using GrapeCity.Documents.Drawing;
using GrapeCity.Documents.Layout;
using GrapeCity.Documents.Pdf;
using GrapeCity.Documents.Text;
using System.Drawing;

Console.WriteLine("PDFファイルにテーブルを追加する");

var doc = new GcPdfDocument();

float pageW = 595;
float pageH = 842;

var page = doc.Pages.Add(new SizeF(pageW, pageH));

// GcPdfGraphicsを初期化
var g = page.Graphics;

// LayoutHostを初期化
var host = new LayoutHost();

// LayoutViewを作成
var view = host.CreateView(pageW, pageH);

// LayoutRectを作成して、アンカーポイントを追加
var rt = view.CreateRect();
rt.AnchorTopLeftRight(null, 36, 36, 36);

// TableRenderer のインスタンスを作成
var ta = new TableRenderer(g,
    rt, FixedTableSides.TopLeftRight,
    rowCount: 5,
    columnCount: 4,
    gridLineColor: Color.Black,
    gridLineWidth: 1,
    rowMinHeight: 15);

// 列のスター幅（比例幅）を設定
var columns = ta.ColumnRects;
columns[0].SetStarWidth(1);
columns[1].SetStarWidth(5);
columns[2].SetStarWidth(2);
columns[3].SetStarWidth(3);

// テーブルのヘッダのセルスタイルを設定
var csHeader = new CellStyle()
{
    TextFormat = new TextFormat()
    {
        ForeColor = Color.Blue,
        FontBold = true,
        FontSize = 9,
        FontName = "Yu Mincho"
    },
    FillColor = Color.LightGray,
    TextAlignment = TextAlignment.Center,
    PaddingAll = 3
};

// データとセルスタイルを使用してセルをテーブルに追加
ta.AddCell(csHeader, 0, 0, "No.");
ta.AddCell(csHeader, 0, 1, "名前");
ta.AddCell(csHeader, 0, 2, "年齢");
ta.AddCell(csHeader, 0, 3, "国");

// テキストの配置を中央に設定
var csCenter = new CellStyle()
{
    TextFormat = new TextFormat()
    {
        ForeColor = Color.Black,
        FontSize = 9,
        FontName = "Yu Gothic"
    },
    TextAlignment = TextAlignment.Center,
    PaddingAll = 3
};

// テキストの配置を右寄せに設定
var csNormal = new CellStyle
{
    TextFormat = new TextFormat()
    {
        ForeColor = Color.Blue,
        FontSize = 9,
        FontName = "Yu Gothic"
    },
    TextAlignment = TextAlignment.Trailing,
    PaddingAll = 3
};

ta.AddCell(csNormal, 1, 0, "1.");
ta.AddCell(csNormal, 1, 1, "Alice");
ta.AddCell(csCenter, 1, 2, "25歳");
ta.AddCell(csNormal, 1, 3, "スペイン");

ta.AddCell(csNormal, 2, 0, "2.");
ta.AddCell(csNormal, 2, 1, "Bob");
ta.AddCell(csCenter, 2, 2, "36歳");
ta.AddCell(csNormal, 2, 3, "ドイツ");

ta.AddCell(csNormal, 3, 0, "3.");
ta.AddCell(csNormal, 3, 1, "Ken");
ta.AddCell(csCenter, 3, 2, "5歳");
ta.AddCell(csNormal, 3, 3, "ブラジル");

ta.AddCell(csNormal, 4, 0, "4.");
ta.AddCell(csNormal, 4, 1, "Teddy");
ta.AddCell(csCenter, 4, 2, "12歳");
ta.AddCell(csNormal, 4, 3, "メキシコ");

ta.ApplyCellConstraints();

// テーブルを描画
ta.Render();

// PDF ドキュメントを保存
doc.Save("result-add-table.pdf");